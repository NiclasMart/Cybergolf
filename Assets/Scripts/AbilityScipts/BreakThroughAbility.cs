using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BreakThroughAbility")]
public class BreakThroughAbility : NonChargeAbility
{
    public int abilityLayer;
    public Material activeMaterial;

    private BreakThrough _breakThrough;

    public override void Initialize(GameObject obj)
    {
        _breakThrough = obj.GetComponent<BreakThrough>();
        _breakThrough.Initialize();

        _breakThrough.abilityLayer = abilityLayer;
        _breakThrough.activeMaterial = activeMaterial;
        _breakThrough.defaultMaterial = symbolMaterial;
    }

    public override void TriggerAbility()
    {
        AudioManager.instance.Play("abilityActivationSound");
        _breakThrough.TriggerBreakThrough();
    }
}
