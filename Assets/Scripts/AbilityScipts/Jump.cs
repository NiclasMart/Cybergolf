using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [HideInInspector] public float jumpPower = 2f;
    Rigidbody _rb;

    public void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void TriggerJump()
    {
        _rb.AddForce(new Vector3(0, jumpPower, 0) * 1000);
    }

   


}
