using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitCounter : MonoBehaviour
{
    Text _text;
    public int score { get; private set; }

    void Start()
    {
        _text = GetComponent<Text>();
    }


    public void addHit()
    {
        score++;
        _text.text = score.ToString();
    }
}
