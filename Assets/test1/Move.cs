using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private float power = 5;

    private Transform obj;
    private Rigidbody rig;

    void Start()
    {
        obj = this.GetComponent<Transform>();
        rig = this.GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rig.AddForce( -camera.right * power);
            //obj.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rig.AddForce(camera.right * power);
            //obj.Translate(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rig.AddForce(camera.forward * power);
            //obj.Translate(0, 0.1f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rig.AddForce( -camera.forward * power);
            //obj.Translate(0, -0.1f, 0);
        }
    }
}
