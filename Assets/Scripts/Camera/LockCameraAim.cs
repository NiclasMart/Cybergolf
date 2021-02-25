using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraAim: MonoBehaviour
{
    Vector3 _offsetPosition;
    Transform _ballPTransform;

    // Start is called before the first frame update
    void Start()
    {
        _ballPTransform = transform.parent;
        _offsetPosition = transform.position - _ballPTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _ballPTransform.position + _offsetPosition;
    }
}
