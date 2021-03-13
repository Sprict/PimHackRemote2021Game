using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : Bolt.EntityBehaviour<ICenterState>
{
    public GameObject shotPoint;
    public GameObject bullet;

    public float speed = 1;

    public void BulletShot(Vector3 addForce)
    {
        // 弾丸の複製
        GameObject bullets = Instantiate(bullet);

        Vector3 force;

        force = this.gameObject.transform.forward.normalized * speed;
        force += addForce * Time.deltaTime;

        // Rigidbodyに力を加えて発射
        bullets.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        // 弾丸の位置を調整
        bullets.transform.position = shotPoint.transform.position;
    }
}
