using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName = "New Ability";
    public KeyCode triggerButton = KeyCode.Space;
    public Sprite sprite;
    public Material symbolMaterial;
    

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();

}
