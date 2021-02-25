using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/StopAbility")]
public class StopAbility : ChargeAbility
{
    private Stop _stop;

    public override void Initialize(GameObject obj)
    {
        _stop = obj.GetComponent<Stop>();
        _stop.Initialize();

    }

    public override void TriggerAbility()
    {
        AudioManager.instance.Play("abilityActivationSound");
        _stop.TriggerStop();
    }
}
