using TMPro;
using UnityEngine;

public class ScoreTextAssigner : MonoBehaviour
{
    public TextMeshProUGUI[] pairDisplayTable;
    public TextMeshProUGUI pairTotalDisplay;
    public TextMeshProUGUI[] scoreDisplayTable;
    public TextMeshProUGUI scoreTotalDisplay;

    private void Awake()
    {
        ScoreHandler scoreHandler = ScoreHandler.instance;

        scoreHandler.pairDisplayTable = pairDisplayTable;
        scoreHandler.pairTotalDisplay = pairTotalDisplay;
        scoreHandler.scoreDisplayTable = scoreDisplayTable;
        scoreHandler.scoreTotalDisplay = scoreTotalDisplay;
    }
}
