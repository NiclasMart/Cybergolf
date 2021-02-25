using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecCamMovement : MonoBehaviour
{
    Vector3 _direction;
    Vector3 _move;
    Rigidbody _rb;
    Transform cam;

   public float speed;

    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetChild(0);
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _direction = Vector3.Normalize(Camera.main.transform.forward);
        _move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            _move += _direction * speed;
        }
        if (Input.GetKey(KeyCode.S)) {
            _move += -_direction * speed;
        }
        if (Input.GetKey(KeyCode.D)) {
            _move += Vector3.Normalize(Camera.main.transform.right) * speed;
        }
        if (Input.GetKey(KeyCode.A)) {
            _move += -Vector3.Normalize(Camera.main.transform.right) * speed;
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            _move += Vector3.up * speed;
        }
        if (Input.GetKey(KeyCode.LeftControl)) {
            _move += -Vector3.up * speed;
        }
    }

    private void FixedUpdate()
    {
        //transform.position += _move * Time.fixedDeltaTime;
        Ray ray = new Ray(transform.position, _move);
        if (!Physics.Raycast(ray, 1f)) { 
            _rb.MovePosition(transform.position + _move * Time.fixedDeltaTime);
        }
    }

    
}
