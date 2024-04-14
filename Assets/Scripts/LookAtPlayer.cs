using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(1, Mathf.Sqrt(2), 1);
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.up, Camera.main.transform.rotation.eulerAngles.y);
    }
}
