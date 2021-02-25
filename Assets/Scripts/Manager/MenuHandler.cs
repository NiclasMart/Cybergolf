using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour {

    public SaveData data;
    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

        //start music
        AudioManager.instance.StopAll();
        AudioManager.instance.Play("mainMenuMusic");

        Destroy(MusicController.instance);
    }

    //toggle display of high score display according to current state
    public void ToggleHighScore()
    {
        GameObject highScoreDisplay = transform.Find("ScoreBoardDisplay").Find("Holder").gameObject;
        if (highScoreDisplay.activeSelf) {
            HideHighScore(highScoreDisplay);
        }
        else {
            ShowHighScore(highScoreDisplay);
        }
    }

    //hides buttons and shows high score panel
    void ShowHighScore(GameObject highScoreDisplay)
    {
        highScoreDisplay.gameObject.SetActive(true);
        transform.Find("Title").gameObject.SetActive(false);
        transform.Find("Buttons").gameObject.SetActive(false);
    }

    //hides high score panel and shows buttons
    void HideHighScore(GameObject highScoreDisplay)
    {
        highScoreDisplay.gameObject.SetActive(false);
        transform.Find("Title").gameObject.SetActive(true);
        transform.Find("Buttons").gameObject.SetActive(true);
    }

    //start animation to show difficulty selection buttons
    public void ShowDifficultyMenu()
    {
        _anim.SetTrigger("SwitchMenu");
    }

    //delets the high score save file and reloads display
    public void ResetHighScoreData()
    {
        SaveSystem.DeleteData();
        HighScoreHandler highScoreDisplay = transform.Find("ScoreBoardDisplay").GetComponent<HighScoreHandler>();
        highScoreDisplay.LoadHighScore();

    }

    //loads the ball generator with the choosen difficulty
    public void LoadNewGame(int mode)
    {
        SetDificulty((Difficulty)mode);
        GameManager.instance.LoadBallGenerator();
    }

    //Loads the TutorialMap
    public void LoadTutorial()
    {
        GameManager.instance.LoadNewScene("Tutorial");
    }

    //Exits the game
    public void CloseGame()
    {
        Application.Quit();
    }

    void SetDificulty(Difficulty mode)
    {
        data.difficultyMode = mode;
    }

    
}
