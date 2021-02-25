using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    public WheelState state = WheelState.PRE_SPINNING;

    Quaternion _rotation;
    Rigidbody _rb;
    Vector3 _spinForce;
  
    void Start()
    {
        state = WheelState.PRE_SPINNING;
        _rotation = transform.rotation;
        _rb = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //handles state of the wheel
        if (state == WheelState.PRE_SPINNING && _rb.angularVelocity.y != 0) {
            state = WheelState.SPINNING;
        }
        else if (_rb.angularVelocity.y < 0.2 && state == WheelState.SPINNING) {
            //stop rotation
            _rb.angularVelocity = Vector3.zero;
            state = WheelState.STOPED;
        }
    }

    //start spinning wheel with specified power
    public void StartSpin(float power)
    {
        _spinForce = new Vector3(0, power, 0);
        _rb.AddTorque(_spinForce, ForceMode.Impulse);
    }


}
