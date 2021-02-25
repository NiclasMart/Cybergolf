using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    Rigidbody _rb;

    //Keep
    public void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void TriggerStop()
    {
        Vector3 tmpV = Vector3.Normalize(_rb.velocity);
        tmpV *= 0.01f;
        _rb.velocity = tmpV;
        _rb.angularVelocity = Vector3.zero;
    }
}
