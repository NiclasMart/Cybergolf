using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }


    public GameState gameState;
    public bool isTutorialLevel = false;
    CameraController _camController;

    public IntegerVariable tutorialData;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) {
            Destroy(instance.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
        gameState = GameState.MENU;
        _camController = null;

        //reset Tutorial
        tutorialData.value = 1;

    }

    private void Update()
    {
        //if starting a new level, search for the camera controller
        if (_camController == null && gameState != GameState.MENU && gameState != GameState.GENERATE_PHASE) {
            _camController = GameObject.Find("CameraController").GetComponent<CameraController>();
        }

        //switch between ball view and spec view
        if (Input.GetKeyDown(KeyCode.C)) {
            if (gameState == GameState.IDLE_PHASE) {
                gameState = GameState.SPEC_PHASE;
                _camController.SwitchToSpecView();
            }
            else if (gameState == GameState.SPEC_PHASE) {
                gameState = GameState.IDLE_PHASE;
                _camController.SwitchToBallView();
            }
        }
    }


    //loads the scene according to the buildIndex
    public void LoadNewScene(int buildIndex) 
    {
        gameState = GameState.IDLE_PHASE;
        _camController = null;
        SceneManager.LoadScene(buildIndex);
    }

    //loads the scene according to the levelName
    public void LoadNewScene(string levelName)
    {
        gameState = GameState.IDLE_PHASE;
        _camController = null;
        SceneManager.LoadScene(levelName);
    }

    //loads the menu screen
    public void LoadMenu()
    {
        gameState = GameState.MENU;
        isTutorialLevel = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _camController = null;
        LoadNewScene(0);
    }

    //is called from menu and loads BallGeneration scene
    public void LoadBallGenerator()
    {
        gameState = GameState.GENERATE_PHASE;
        SceneManager.LoadScene("BallGenerator");
    }

}
