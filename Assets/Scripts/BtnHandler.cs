using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnHandler : MonoBehaviour
{
    public Button spinBtn;
    public Button startBtn;

    public static BtnHandler instance;

    private void Start()
    {
        instance = this;
    }

    public void DeactivateBtn(Button b)
    {
        b.interactable = false;
    }

    public void ActivateStartButton()
    {
        startBtn.interactable = true;
    }

    public void LoadFirstLevel()
    {
        GameManager.instance.LoadNewScene(1);
    }
}
