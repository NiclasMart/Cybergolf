using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour
{
    Quaternion _startRotation;
    // Start is called before the first frame update
    void Start()
    {
        _startRotation = transform.rotation;
    }


    private void Update()
    {
        transform.rotation = _startRotation;
    }
}
