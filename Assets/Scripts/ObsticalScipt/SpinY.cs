using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinY : MonoBehaviour
{

    Rigidbody _rb;
    Vector3 _rotationVector;
    public float speed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rotationVector = new Vector3(0, speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Quaternion newRotation = Quaternion.Euler(_rotationVector * Time.deltaTime);
        _rb.MoveRotation(_rb.rotation * newRotation);
    }
}
