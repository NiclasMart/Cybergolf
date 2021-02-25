using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    LevelManager _lvlManager;
    GameManager _gameManager;
    BallGenerator _generator;
    

    int _tutorialStages = 8;
    int _currentTutorialStage = 1;
    bool startedTutorialStage = false;

    public IntegerVariable tutorialStageCounter;
    public GameObject _panel;


    private void Awake()
    {
        _generator = GameObject.Find("BallGenerator").GetComponent<BallGenerator>();
        _gameManager = GameManager.instance;
        _lvlManager = LevelManager.instance;

        //set gameState
        _gameManager.gameState = GameState.TUTORIAL;
        _gameManager.isTutorialLevel = true;

        //search for current tutorial stage
        _currentTutorialStage = tutorialStageCounter.value;

        TutorialInformer informer;

        //starts tutorial for each stage and sets corresponding tutorial informer
        switch (_currentTutorialStage) {
            case 1:
                _generator.SetProperties(BallAbility.NONE, BallBounciness.NORMAL, WorldGravity.NORMAL);

                //set tutorial informer
                informer = _lvlManager.ball.GetComponent<TutorialInformer>();
                informer.enabled = true;
                informer.Initialize(InformType.ROLL_END, this);
                ShowTutorialUI("First");
                break;
            case 2:
                _generator.SetProperties(BallAbility.NONE, BallBounciness.NORMAL, WorldGravity.NORMAL);

                //set tutorial informer
                informer = GameObject.Find("Goal").GetComponent<TutorialInformer>();
                informer.enabled = true;
                informer.Initialize(InformType.TEST_CAM, this);
                ShowTutorialUI("Second");
                break;
            case 3:
                _generator.SetProperties(BallAbility.JUMP, BallBounciness.NORMAL, WorldGravity.NORMAL);

                //set tutorial informer
                informer = _lvlManager.cooldownManager.gameObject.GetComponent<TutorialInformer>();
                informer.enabled = true;
                informer.Initialize(InformType.TRIGGER_ABILITY, this);
                ShowTutorialUI("Third");
                break;
            case 4:
                _generator.SetProperties(BallAbility.STOP, BallBounciness.NORMAL, WorldGravity.NORMAL);

                //set tutorial informer
                informer = _lvlManager.cooldownManager.gameObject.GetComponent<TutorialInformer>();
                informer.enabled = true;
                informer.Initialize(InformType.TRIGGER_ABILITY, this);
                ShowTutorialUI("Fourth");
                break;
            case 5:
                _generator.SetProperties(BallAbility.REDIRECT, BallBounciness.NORMAL, WorldGravity.NORMAL);

                //set tutorial informer
                informer = _lvlManager.cooldownManager.gameObject.GetComponent<TutorialInformer>();
                informer.enabled = true;
                informer.Initialize(InformType.TRIGGER_ABILITY, this);
                ShowTutorialUI("Fifth");
                break;
            case 6:
                _generator.SetProperties(BallAbility.BREAK, BallBounciness.NORMAL, WorldGravity.NORMAL);

                //set tutorial informer
                informer = _lvlManager.cooldownManager.gameObject.GetComponent<TutorialInformer>();
                informer.enabled = true;
                informer.Initialize(InformType.TRIGGER_ABILITY, this);
                ShowTutorialUI("Sixth");
                break;
            case 7:
                _generator.SetProperties(BallAbility.JUMP, BallBounciness.HARD, WorldGravity.LOW);

                //set tutorial informer
                informer = _lvlManager.ball.GetComponent<TutorialInformer>();
                informer.enabled = true;
                informer.Initialize(InformType.REACH_GOAL, this);
                ShowTutorialUI("Seventh");
                break;
            case 8:
                _generator.SetProperties(BallAbility.NONE, BallBounciness.NORMAL, WorldGravity.NORMAL);
                ShowTutorialUI("Eighth");
                break;

        }
        
    }

    private void Start()
    {
        //Set Cursor for menu handling
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowCompleteUI()
    {
        _gameManager.gameState = GameState.TUTORIAL;
        ShowTutorialUI("Complete");
    }

    public void LoadNewStage()
    {
        if (tutorialStageCounter.value < _tutorialStages) {
            tutorialStageCounter.ApplyChange(1);
        }
        _gameManager.LoadNewScene("Tutorial");
    }

    //set UI and cursor to visible
    void ShowTutorialUI(string stage)
    {
        //handle cursor display
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //handle display of first tutorial step
        _panel.transform.Find(stage).gameObject.SetActive(true);
        _panel.SetActive(true);
        Time.timeScale = 0f;
    }

    //hides UI and Cursor
    void HideTutorialUI(string stage)
    {
        //handle cursor display
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //handle UI display
        _panel.transform.Find(stage).gameObject.SetActive(false);
        _panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void StartTutorialPractice(string stage)
    {
        _gameManager.gameState = GameState.IDLE_PHASE;
        HideTutorialUI(stage);
    }





}
