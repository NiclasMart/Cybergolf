//handels the putt mechanic of the ball

using UnityEngine;

public class Hit : MonoBehaviour 
{
    //influences how fast the value changes in hit Power controll with mouse movement
    const float POWER_INPUT_SENSITY = 0.1f;
    
    Rigidbody _rb;
    Ball _ball;
    GameManager _gameManager;
    HitCounter _hitCounter;
    PowerDisplay _display;
    
    bool _mouseIsPressed = false;
    public float hitPower { get; private set; } = 0;
    Vector3 _hitForce = Vector3.zero;

    //influences hit power
    public float powerMultiplier = 700;
    

    // Start is called before the first frame update
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        _ball = transform.GetComponent<Ball>();
        _gameManager = GameManager.instance;
        _hitCounter = LevelManager.instance.hitCounter;
        _display = LevelManager.instance.powerDisplay;
    }

    // Update is called once per frame
    void Update() 
    {
        
        //checks if the mousebutton is pressed
        if (_gameManager.gameState == GameState.IDLE_PHASE && Input.GetMouseButtonDown(0)) {

            //change game state
            _gameManager.gameState = GameState.HIT_PHASE;

            //save this as last position
            LevelManager.instance.SetLastPosition();

            //handle mouse button down for setting hit intensity
            CameraController.instance.LockCamera();
            _mouseIsPressed = true;

        }
        else if (_gameManager.gameState == GameState.HIT_PHASE && Input.GetMouseButtonUp(0)) {

            //handle mouse button up for hit release
            CameraController.instance.DeLockCamera();
            CalculateHitForce();
            _mouseIsPressed = false;  
        }
        else if (_gameManager.gameState == GameState.TUTORIAL) {
            //resets state if Tutorial UI is loaded
            _mouseIsPressed = false;
        }


        //if the mouse is pressed, calculate the hit power according to the vertical movement of th mouse
        if (_mouseIsPressed) {
            float input = Input.GetAxis("Mouse Y");
            hitPower = Mathf.Min(1, Mathf.Max(0, hitPower += input*POWER_INPUT_SENSITY));
            //show power within the power bar
            _display.SetPowerBar(hitPower);
        }
    }

    private void FixedUpdate() 
    {
        //apply hit force to ball
        if (_gameManager.gameState == GameState.HIT_PHASE && !_mouseIsPressed) {
            //check if power was applied or if power is zero vector
            if (_hitForce != Vector3.zero) {
                //play ball hit sound
                AudioManager.instance.SetVolume("ballHitSound", Mathf.Lerp(0.1f, 1f, hitPower));
                AudioManager.instance.Play("ballHitSound");
                hitPower = 0;

                //add force
                _rb.AddForce(_hitForce);
                _hitForce = Vector3.zero;

               
                //change game state
                _gameManager.gameState = GameState.ROLL_PHASE;

                //add hit to counter
                _hitCounter.addHit();
            }
            else {
                _gameManager.gameState = GameState.IDLE_PHASE;
            }
        }
    }


    //calculates the vector in which the ball will be hit
    void CalculateHitForce() 
    {
        Vector3 direction = Camera.main.transform.forward;
        direction.y = 0f;
        direction = Vector3.Normalize(direction);
        direction *= hitPower * powerMultiplier;
        _hitForce = direction;
    }

}
