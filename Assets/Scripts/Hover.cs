using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum HoverState
{
    UP,
    DOWN
}

public class Hover : MonoBehaviour
{
    Vector3 _pos;
    HoverState _state;
    float _min, _max, speedUpMuliplier, slowDownMultiplier;
    float _startSpeed;

    public float speed;
    public float amplitude;
    [Range(0f, 0.05f)]
    public float slowDownSpeed;
    public float minSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _pos = transform.position;
        _min = _pos.y - amplitude;
        _max = _pos.y + amplitude;
        slowDownMultiplier = 1f - slowDownSpeed;
        speedUpMuliplier = 1f + slowDownSpeed;
 

        _startSpeed = speed;

        if (Random.Range(0f, 1f) < 0.5) {
            _state = HoverState.DOWN;
        }
        else {
            _state = HoverState.UP;
        }
    }

    private void FixedUpdate()
    {
        if (_state == HoverState.DOWN) {
            if (_pos.y < _min) {
                speed *= slowDownMultiplier;
                if (speed <= minSpeed) {
                    _state = HoverState.UP;
                }
            }
            else if (_pos.y > _max && speed <= _startSpeed) {
                speed *= speedUpMuliplier;
            }
            _pos.y -= speed * Time.fixedDeltaTime;
            
        }
        else {
            if (_pos.y > _max) {
                speed *= slowDownMultiplier;
                if (speed < minSpeed) {
                    _state = HoverState.DOWN;
                }
            }
            else if (_pos.y < _min && speed <= _startSpeed) {
                speed *= speedUpMuliplier;
            }
            _pos.y += speed * Time.fixedDeltaTime;
           
        }

        transform.position = _pos;
    }
}
