﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer),typeof(Renderer))]
public class PlayerBehaviour : Bolt.EntityEventListener<ICubeState>
{
    private float _resetColorTime; //ダメージを受けた時に使う
    private Renderer _renderer;
    private GameObject Center;
    [SerializeField] private float power = 5;
    private Rigidbody rb;

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform grappleTip, rayOrigin, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private Transform cameraTransform;

    private float distanceFromPoint;
    [SerializeField]
    private float reelSpeed = 20.0f;

    [SerializeField]
    private float drowSpeed = 8;

    public override void Attached()
    {
        _renderer = GetComponent<Renderer>(); //ダメージを受けた時にマテリアルの色を変えるために使う
        rb = GetComponent<Rigidbody>();
        state.SetTransforms(state.CubeTransform, transform);

        if (entity.IsOwner)
        {
            state.CubeColor = new Color(Random.value, Random.value, Random.value);
            Center = (GameObject)Resources.Load("Center");
            Center = Instantiate(Center, this.transform.position, Quaternion.identity);
        cameraTransform = Center.transform;
        }
        //Center = BoltNetwork.Instantiate(BoltPrefabs.Center, this.transform.position, Quaternion.identity);
        Debug.Log("hello");
        state.AddCallback("CubeColor", ColorChanged);

        lr = GetComponent<LineRenderer>();
    }

    public override void SimulateOwner()
    {
        var speed = 4f;
        var movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-cameraTransform.right * power);
            //obj.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(cameraTransform.right * power);
            //obj.Translate(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(cameraTransform.forward * power);
            //obj.Translate(0, 0.1f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-cameraTransform.forward * power);
            //obj.Translate(0, -0.1f, 0);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            var flash = FlashColorEvent.Create(entity);
            flash.FlashColor = Color.red; //ダメージ色
            flash.Send();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGrapple();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopGrapple();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Shot shotCommand = Center.transform.GetChild(0).GetComponent<Shot>();
            shotCommand.BulletShot();
        }

        if (Input.GetKey(KeyCode.E))
        {
            reelin();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            reelout();
        }

        this.Center.transform.position = this.transform.position; // CenterのポジションをPlayerのポジションと同期させる
        RotationSync();
    }

    private void Update()
    {
        if (_resetColorTime < Time.time)
        {
            _renderer.material.color = state.CubeColor;
        }
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, maxDistance, whatIsGrappleable))
        {
            Debug.Log(hit.transform.gameObject);

            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            //joint.connectedBody = hit.transform.gameObject.GetComponent<Rigidbody>();

            distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = 0.6f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = grappleTip.position;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);

        Destroy(this.GetComponent<Joint>());
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * drowSpeed);

        lr.SetPosition(0, grappleTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    void ColorChanged()
    {
        GetComponent<Renderer>().material.color = state.CubeColor;
    }

    void OnGUI()
    {
        if (entity.IsOwner)
        {
            GUI.color = state.CubeColor;
            GUILayout.Label("@@@");
            GUI.color = Color.white;
        }
    }

    public override void OnEvent(FlashColorEvent evnt)
    {
        _resetColorTime = Time.time + 0.2f;
        _renderer.material.color = evnt.FlashColor;
    }

    void RotationSync()
    {
        transform.GetChild(0).transform.rotation = Center.transform.rotation;
    }

    void reelin ()
    {
        if(joint)
        joint.maxDistance -= reelSpeed * Time.deltaTime;
    }

    void reelout ()
    {
        if (joint)
            joint.maxDistance += reelSpeed * Time.deltaTime;
    }
}