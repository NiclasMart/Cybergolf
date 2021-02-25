using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RedirectAbility")]
public class RedirectAbility : ChargeAbility
{
    public float slowDown = 1f;

    private Redirect _redirect;

    public override void Initialize(GameObject obj)
    {
        _redirect = obj.GetComponent<Redirect>();
        _redirect.Initialize();

        _redirect.slowDown = slowDown;
    }

    public override void TriggerAbility()
    {
        AudioManager.instance.Play("abilityActivationSound");
        _redirect.TriggerRedirect(triggerButton);
    }
}
