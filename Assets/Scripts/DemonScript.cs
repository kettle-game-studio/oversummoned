using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{
    public string Name;
    public Rigidbody DemonBody;

    public void StartBeHeld()
    {
        DemonBody.isKinematic = true;
    }

    public void StopBeHeld()
    {
        DemonBody.isKinematic = false;
    }
}
