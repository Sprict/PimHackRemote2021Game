using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject shotPoint;
    public GameObject bullet;

    public float speed = 1;

    public void BulletShot()
    {
        // 弾丸の複製
        GameObject bullets = Instantiate(bullet);

        Vector3 force;

        force = this.gameObject.transform.forward * speed;

        // Rigidbodyに力を加えて発射
        bullets.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        // 弾丸の位置を調整
        bullets.transform.position = shotPoint.transform.position;
    }
}
