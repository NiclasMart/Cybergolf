using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PropertyDisplay : MonoBehaviour
{
     public TextMeshProUGUI textElement;
     public SaveData data;

    // Start is called before the first frame update
    void Start()
    {
        textElement.SetText(
            "Talent: " + StringGenerator.Generate(data.ballProperties.ballAbility) + "\n" + 
            "Federung: " + StringGenerator.Generate(data.ballProperties.ballBounciness) + "\n" + 
            "Gravitation: " + StringGenerator.Generate(data.ballProperties.gravity));
    }

}
