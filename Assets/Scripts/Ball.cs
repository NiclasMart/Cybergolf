using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody _rb;
    GameManager _gameManager;
    AbilityManager _abilityManager;
    PowerDisplay _powerDisplay;
    MeshRenderer _ballGraphic;

    public bool Rolling => _rb.velocity != Vector3.zero;
    public bool Grounded { get; private set;  }
    float _angDrag;

    // Start is called before the first frame update
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        _angDrag = _rb.angularDrag;
        _gameManager = GameManager.instance;
        _abilityManager = LevelManager.instance.cooldownManager;
        _powerDisplay = LevelManager.instance.powerDisplay;
        _ballGraphic = GetComponentInChildren<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        //if ball stops, change game State to Idle phase
        if (_gameManager.gameState == GameState.ROLL_PHASE && !Rolling) {
            _gameManager.gameState = GameState.IDLE_PHASE;
            //add Charge to ability
            _abilityManager.AddCharge();
            //reset power display
            _powerDisplay.SetPowerBar(0);
        }

        //handle slow down of ball after the hit, if its on floor
        if (Grounded && _rb.velocity.magnitude < 3f) {
            _rb.angularDrag = 7f;
        }
        else {
            _rb.angularDrag = _angDrag;
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        //check if ball reached goal and show points if level is no tutorial level
        if (!_gameManager.isTutorialLevel && other.tag == "goal" && _gameManager.gameState == GameState.IDLE_PHASE) {
            _gameManager.gameState = GameState.POINT_VIEW;
            LevelManager.instance.ShowScore();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //handle reset of ball, if its outside the map
        if (other.tag == "resetCube") {
            LevelManager.instance.ResetBall();
        }
        //if Camera gets close to ball, set ball material transparent
        if (other.tag == "MainCamera") {
            transform.Find("Graphik").gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if camera gets away from the wall, the material from the ball is set to non Transparent
        if (other.tag == "MainCamera") {
            transform.Find("Graphik").gameObject.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //sets ball to grounded if its colliding with the floor
        if (collision.transform.tag == "floor") {
            Grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //sets ball to not grounded if its exiting the collider
        if (collision.transform.tag == "floor") {
            Grounded = false;
        }
    }
}
