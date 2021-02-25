using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/JumpAbility")]
public class JumpAbility : ChargeAbility
{
    public float jumpPower = 2f;
   
    private Jump _jump;

    public override void Initialize(GameObject obj)
    {
        _jump = obj.GetComponent<Jump>();
        _jump.Initialize();

        _jump.jumpPower = jumpPower;
    }

    public override void TriggerAbility()
    {
        AudioManager.instance.Play("abilityActivationSound");
        _jump.TriggerJump();
    }
}
