using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move :  Bolt.EntityBehaviour{
    public float speed = 3.0f;
    // Update is called once per frame
    void Update ( ) {
        if (Input.GetKey ("up")) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey ("down")) {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
    }
}