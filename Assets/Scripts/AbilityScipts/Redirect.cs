using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Redirect : MonoBehaviour
{
    [HideInInspector] public float slowDown = 1f;
    Rigidbody _rb;
    Ball _ball;
    Vector3 _newDirection;
    Vector3 _oldDirection;

    //Keep
    public void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        _ball = GetComponent<Ball>();
    }

    void PrepareAbility()
    {
        //stop ball
        Time.timeScale = 0f;

        //display direction line
        GetComponent<CalculateDirectionLine>().redirectIsActive = true;

        CameraController.instance.SetCameraUpdateSettings(CinemachineBrain.UpdateMethod.LateUpdate);
    }

    void CalculateRedirect()
    {
        //save direction
        _oldDirection = _rb.velocity;
        _oldDirection.y = 0f;

        //search new direction
        _newDirection = Camera.main.transform.forward;
        _newDirection.y = 0f;
        _newDirection = Vector3.Normalize(_newDirection);

        //delete old roll direction
        _rb.angularVelocity = Vector3.zero;
    }

    void ApplyRedirection()
    {
        float oldSpeed = _oldDirection.magnitude * slowDown;
        Vector3 tmpVel = _rb.velocity;
        tmpVel.x = _newDirection.x * oldSpeed;
        tmpVel.z = _newDirection.z * oldSpeed;
        _rb.velocity = tmpVel;

        //hide direction line
        GetComponent<CalculateDirectionLine>().redirectIsActive = false;
        Time.timeScale = 1f;
    }

    IEnumerator WaitUntilButtonIsReleased(KeyCode key)
    {
        //wait until spae is released
        while (Input.GetKey(key)){
            yield return new WaitForSecondsRealtime(0.1f);
        }

        CalculateRedirect();
        ApplyRedirection();
        CameraController.instance.SetCameraUpdateSettings(CinemachineBrain.UpdateMethod.SmartUpdate);

        AudioManager.instance.Play("abilityActivationSound");
    }

    public void TriggerRedirect(KeyCode key)
    {
        PrepareAbility();

        StartCoroutine(WaitUntilButtonIsReleased(key));
    }
}
