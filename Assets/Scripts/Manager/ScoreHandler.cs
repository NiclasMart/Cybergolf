using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    const int MAP_COUNT = 3;

    public static ScoreHandler instance;

    //array structure: first map, second map, third map, total
    public TextMeshProUGUI[] pairDisplayTable;
    public TextMeshProUGUI pairTotalDisplay;
    public TextMeshProUGUI[] scoreDisplayTable;
    public TextMeshProUGUI scoreTotalDisplay;

    int[] _pairTable = { 5, 4, 6 };
    List<int> _scoreTable = new List<int>();
    public int totalScore = 0;

    private void Awake()
    {
        //handle Singelton
        if (instance != null) {
            Destroy(instance);
        }
        instance = this;

        DontDestroyOnLoad(this);
    }


    public void AddScore(int score)
    {
        _scoreTable.Add(score);
        DisplayScore();
    }

    public void DisplayScore()
    {
        int totalPairCount = 0, totalScoreCount = 0;
        for (int i = 0; i < MAP_COUNT; i++) {
            if (_scoreTable.Count >= i + 1) {
                scoreDisplayTable[i].SetText(Convert.ToString(_scoreTable[i]));
                totalScoreCount += _scoreTable[i];

                pairDisplayTable[i].SetText(Convert.ToString(_pairTable[i]));
                totalPairCount += _pairTable[i];
            }

        }

        pairTotalDisplay.SetText(Convert.ToString(totalPairCount));
        scoreTotalDisplay.SetText(Convert.ToString(totalScoreCount));

        totalScore = totalScoreCount;
    }

    
}
