using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TutorialInformer : MonoBehaviour
{   
    [HideInInspector]
    public InformType informType;
    TutorialHandler _tutorialHandler = null;
    GameManager _gameManager;

    bool _preconditionFulfilled = false;

    Ball _ball;
    BoxCollider _goal;
    AbilityManager _abilityManager;

    delegate void ExecFunction();


    public void Initialize(InformType type, TutorialHandler handler)
    {
        informType = type;
        _tutorialHandler = handler;
        _gameManager = GameManager.instance;

        switch (type) {
            case InformType.ROLL_END:
                _ball = gameObject.GetComponent<Ball>();
                break;
            case InformType.TEST_CAM:
                _goal = gameObject.GetComponent<BoxCollider>();
                break;
            case InformType.TRIGGER_ABILITY:
                _abilityManager = gameObject.GetComponent<AbilityManager>();
                _ball = LevelManager.instance.ball.GetComponent<Ball>();
                break;
        }
    }

    private void Update()
    {
        if (_tutorialHandler != null) {
            switch (informType) {
                case InformType.ROLL_END:
                    TestIfBallHasStopedRolling();
                    break;
                case InformType.TEST_CAM:
                    TestIfCameraHasSpottedGoal();
                    break;
                case InformType.TRIGGER_ABILITY:
                    TestIfAbilityWasTriggerd();
                    break;
                case InformType.REACH_GOAL:
                    TestIfBallReachedGoal();
                    break;
                    
            }
        }
    }

    //calls the given function after an amount of time
    IEnumerator WaitForExecution(ExecFunction methode, float time)
    {
        yield return new WaitForSeconds(time);
        methode();
    }

    //Tutorial Stage 1
    //ball need to roll and then stop to fulfill this test and trigger the next tutorial
    void TestIfBallHasStopedRolling()
    {
        if (_preconditionFulfilled) {
            if (!_ball.Rolling) {
            _tutorialHandler.ShowCompleteUI();
            }
        }
        else {
            _preconditionFulfilled = _ball.Rolling;
        }
    }

    //Tutorial Stage 2
    //the camera mode need to be switched to spec cam and back to fulfilling this test and trigger the next tutorial
    void TestIfCameraHasSpottedGoal()
    {
        if (_preconditionFulfilled) {
            if (_gameManager.gameState != GameState.SPEC_PHASE) {
                //if condition is fulfilled, load next tutorial stage after 3sec
                ExecFunction m_function = _tutorialHandler.ShowCompleteUI;
                StartCoroutine(WaitForExecution(m_function, 3f));
            }
        }
        else {
            _preconditionFulfilled = (_gameManager.gameState == GameState.SPEC_PHASE);
        }
    }

    //Tutorial Stage 3 - 6
    //the ability need to be triggerd and the ball need to stop to fulfilling this test and trigger the next tutorial
    void TestIfAbilityWasTriggerd()
    {
        if (_preconditionFulfilled) {
            if (!_ball.Rolling) {
                ExecFunction m_function = _tutorialHandler.ShowCompleteUI;
                StartCoroutine(WaitForExecution(m_function, 2f));
            }
        }
        else {
            if (_gameManager.gameState == GameState.ROLL_PHASE) {
                _preconditionFulfilled = Input.GetKeyDown(KeyCode.Space);
            }
        }
    }

    //Tutorial Stage 7
    //the ball need to reach the goal to fulfill this test and trigger the next tutorial
    void TestIfBallReachedGoal()
    {
        if (_preconditionFulfilled) {
            if (_gameManager.gameState == GameState.POINT_VIEW) {
                _tutorialHandler.ShowCompleteUI();
            }
        }
        else {
            //in this stage is no precondition
            _preconditionFulfilled = true;
        }
    }

    //check if ball reached goal for the "REACH_GOAL" lvl
    private void OnTriggerStay(Collider other)
    {
        if (enabled && other.tag == "goal" && _gameManager.gameState == GameState.IDLE_PHASE) {
            _gameManager.gameState = GameState.POINT_VIEW;
        }
    }

}
