using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSync : MonoBehaviour
{
    public string targetObjName;
    // Update is called once per frame
    void Update()
    {
        GameObject.Find(targetObjName).transform.position = this.transform.position;
    }
}
