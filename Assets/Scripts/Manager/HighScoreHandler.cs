using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[System.Serializable]
public struct HighScore
{
    public BallAbility ability;
    public BallBounciness bounciness;
    public WorldGravity gravity;
    public int score;
}

[System.Serializable]
public class HighScoreHandler : MonoBehaviour
{
    TextMeshProUGUI[][] ScoreBoard = new TextMeshProUGUI[3][];
    public TextMeshProUGUI[] firstPlace;
    public TextMeshProUGUI[] secondPlace;
    public TextMeshProUGUI[] thirdPlace;
    public TextMeshProUGUI newIcon;

    List<HighScore> highScoreData;

    private void Awake()
    {
        // get text fields in appropriate form
        ScoreBoard[0] = firstPlace;
        ScoreBoard[1] = secondPlace;
        ScoreBoard[2] = thirdPlace;
        
        LoadHighScore();
    }

    //trys to load data from file and displays it
    public void LoadHighScore()
    {
        highScoreData = SaveSystem.LoadData();

        //if no file was found call function with parameter, that all fields should be empty
        if (highScoreData == null) {   
            FillDisplay(true);
        }
        else {
            //sort all elements and display them
            highScoreData.Sort((x, y) => x.score.CompareTo(y.score));
            FillDisplay(false);
        }
    }

    public bool FillNewData(HighScore newData)
    {

        if (CheckIfNewHighScore(newData.score)) {
            if (highScoreData == null) {
                highScoreData = new List<HighScore>();
                highScoreData.Add(newData);
                DisplayNewIcon(0);
            }
            else {
                highScoreData.Add(newData);
                highScoreData.Sort((x, y) => x.score.CompareTo(y.score));
                if (highScoreData.Count == 4) {
                    highScoreData.RemoveAt(3);
                }
                DisplayNewIcon(highScoreData.IndexOf(newData));
            }
            

            FillDisplay(false);
            SaveSystem.SaveData(highScoreData);
            return true;
        }

        return false;
    }

    bool CheckIfNewHighScore(int score)
    {
        if (highScoreData == null || highScoreData.Count < 3) {
            return true;
        }
        for (int i = 0; i < highScoreData.Count; i++) {
            if (score < highScoreData[i].score) {
                return true;
            }
        }
        return false;
    }

    void DisplayNewIcon(int position)
    {
        Vector3 icon = newIcon.GetComponent<RectTransform>().localPosition;
        icon.y = icon.y + position * -110f;
        newIcon.GetComponent<RectTransform>().localPosition = icon;

       
        newIcon.gameObject.SetActive(true);
        Debug.Log("PlayAnimatiiiioon");
        newIcon.gameObject.GetComponent<Animator>().Play("NewIconPulsAnimation");

    }
    

    void FillDisplay(bool empty)
    {
        //extracts each row from the display and call row filler function
        if (empty) {
            //if no data is available, call function for each row with empty parameter
            for (int i = 0; i < 3; i++) {
                FillRow(ScoreBoard[i], i, true);
            }
        }
        else {
            //check if data for row exists and fill this row accordingly
            for (int i = 0; i < 3; i++) {
                if (highScoreData.Count >= i + 1) {
                    FillRow(ScoreBoard[i], i, false);
                }
                else {
                    FillRow(ScoreBoard[i], i, true);
                }
            }
        }
    }

    //fills each row according to existing data
    void FillRow(TextMeshProUGUI[] highScoreField, int row, bool empty)
    {
        if (empty) {
            //fills row with empty symbols
            for (int i = 0; i < 4; i++) {
                highScoreField[i].SetText("---");
            }
        }
        else {
            //fills data in row
            highScoreField[0].SetText(StringGenerator.Generate(highScoreData[row].ability));
            highScoreField[1].SetText(StringGenerator.Generate(highScoreData[row].bounciness));
            highScoreField[2].SetText(StringGenerator.Generate(highScoreData[row].gravity));
            highScoreField[3].SetText(StringGenerator.Generate(highScoreData[row].score));
        }

    }
}
