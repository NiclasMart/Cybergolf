using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public AbilityManager cooldownManager;
    public PowerDisplay powerDisplay;
    public GameObject scoreDisplay;
    public HitCounter hitCounter;
    public HighScoreHandler highScoreHandler;
    public SaveData data;
    [HideInInspector]
    public GameObject ball;
    GameManager _gameManager;
    BallGenerator _generator;
    ScoreHandler _scoreHandler;
    Vector3 _lastPosition;

    public GameObject pauseMenu;

    GameState _stateBeforeEnteringPauseMenu;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("ball");
        _generator = GameObject.Find("BallGenerator").GetComponent<BallGenerator>();
        _gameManager = GameManager.instance;
        _scoreHandler = ScoreHandler.instance;

        //handle cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //generate ball with according properties
        _generator.SetBallProperties(ball, cooldownManager);
    }

    private void Update()
    {
        //handle Pause menu
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameState state = _gameManager.gameState;
            if ((state == GameState.IDLE_PHASE) || (state == GameState.ROLL_PHASE) || (state == GameState.SPEC_PHASE)) {
                PauseGame();
            }
        }
    }

    //pauses game and show pause menu
    void PauseGame()
    {
        // play menu open sound and pause music
        AudioManager.instance.Play("openMenuSound");
        MusicController.instance.DisableMusic();

        _stateBeforeEnteringPauseMenu = _gameManager.gameState;
        _gameManager.gameState = GameState.PAUSE_MENU;
        pauseMenu.SetActive(true);

        UnlockCursor();
    }

    //resume from pause menu back to the game
    public void ResumeGame()
    {
        //resume music
        MusicController.instance.EnableMusic();

        _gameManager.gameState = _stateBeforeEnteringPauseMenu;
        pauseMenu.SetActive(false);

        //handle Cursor
        LockCursor();
    }

    //reload level
    public void ReloadLevel()
    {
        ResumeGame();
        _gameManager.LoadNewScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowHighscore()
    {
        //create new high score object 
        HighScore newScore = new HighScore();
        newScore.ability = data.ballProperties.ballAbility;
        newScore.bounciness = data.ballProperties.ballBounciness;
        newScore.gravity = data.ballProperties.gravity;
        newScore.score = _scoreHandler.totalScore;

        //check if new score is a high score and fill it in if so
        highScoreHandler.FillNewData(newScore);

        //handle display of UI
        GameObject.Find("Scoorboard").SetActive(false);
        highScoreHandler.transform.Find("Holder").gameObject.SetActive(true);
    }

    //return to menu
    public void ReturnToMenu()
    {
        ResumeGame();
        _gameManager.LoadMenu();
    }

    //save score and display score Panel
    public void ShowScore()
    {
        scoreDisplay.SetActive(true);
        _scoreHandler.AddScore(hitCounter.score);
        _scoreHandler.DisplayScore();
        UnlockCursor();
        
    }

    //LoadNextLevel
    public void LoadNextLevel()
    {
        GameManager.instance.LoadNewScene(SceneManager.GetActiveScene().buildIndex + 1);
        LockCursor();
    }

    //saves the last ball position
    public void SetLastPosition()
    {
        _lastPosition = ball.transform.position;
    }

    //resets ball to last position
    public void ResetBall()
    {
        AudioManager.instance.Play("ballResetSound");

        StartCoroutine(SetBallPosition());

    }

    public IEnumerator SetBallPosition()
    {
        yield return new WaitForSeconds(0.3f);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ball.transform.position = _lastPosition;
    }

    void UnlockCursor()
    {
        //handle Cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
}
