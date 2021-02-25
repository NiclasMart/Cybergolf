using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Movement
{
    DOWN,
    UP,
    DOWN_WAIT,
    UP_WAIT
}

public class guillotine : MonoBehaviour
{
    Vector3 _startPosition, _downVec, _upVec;
    Rigidbody _rb;
    Movement _state = Movement.DOWN;
    float _waitStartTime;

    public float downSpeed;
    public float upSpeed;
    public float downStayDuration;
    public float upStayDuration;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startPosition = transform.position;

        _downVec = new Vector3(0, -downSpeed, 0);
        _upVec = new Vector3(0, upSpeed, 0);
    }

    private void FixedUpdate()
    {
        if (transform.position.y > _startPosition.y && _state == Movement.UP) {
            _state = Movement.UP_WAIT;
            _waitStartTime = Time.time;
        }


        if (_state == Movement.DOWN) {
            _rb.MovePosition(transform.position + _downVec * Time.fixedDeltaTime);
        }
        else if (_state == Movement.UP) {
            _rb.MovePosition(transform.position + _upVec * Time.fixedDeltaTime);
        }
        else if (_state == Movement.DOWN_WAIT && Time.time - _waitStartTime >= downStayDuration) {
            _state = Movement.UP;
        }
        else if (_state == Movement.UP_WAIT && Time.time - _waitStartTime >= downStayDuration) {
            _state = Movement.DOWN;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        _state = Movement.DOWN_WAIT;
        _waitStartTime = Time.time;
        Debug.Log("Trigger");
    }

}
