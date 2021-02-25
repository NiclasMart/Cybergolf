using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public Image darkMask;

    private Ability _ability;
    private GameObject _ball;
    private Image _iconImage;
    private GameManager _gameManager;

    private bool abilityIsAvailable;
    //for charge abilities
    private int chargesNeeded;
    private int currentCharges = 0;
    //for nonCharge Abilities
    private int startAmountUses;
    private int remainingUses;
    private bool isActive = false;
    

    public void Initialize(Ability selectedAbility, GameObject ball)
    {
        _gameManager = GameManager.instance;
        _ability = selectedAbility;
        _ball = ball;
        _ball.transform.Find("Symbol").GetComponentInChildren<MeshRenderer>().material = _ability.symbolMaterial;
        _iconImage = GetComponent<Image>();
        _iconImage.sprite = _ability.sprite;
        darkMask.sprite = _ability.sprite;
        _ability.Initialize(ball);

        //differentiate between the two ability types
        if (_ability is ChargeAbility) {
            chargesNeeded = ((ChargeAbility)_ability).chargesNeeded;
            //if the level is a tutorial level, set abilitys ready to use without charges
            if (_gameManager.gameState == GameState.TUTORIAL) {
                SetAbilityReady();
            }
            AddCharge();
        }
        else {
            startAmountUses = ((NonChargeAbility)_ability).startAmount;
            remainingUses = startAmountUses;
        }

        SetAbilityState();
    }

    void Update()
    {
        //check if button for ability trigger is pressed and ability can be used
        if (abilityIsAvailable && _gameManager.gameState == GameState.ROLL_PHASE && !isActive) {
            if (Input.GetKeyDown(_ability.triggerButton)) {
                ButtonTriggerd();
            }
        }
    }
  
    //adds one charge to the ability
    public void AddCharge()
    {
        //only add charge if ability is a charge ability
        if (_ability is ChargeAbility) {
            currentCharges = Mathf.Min(currentCharges + 1, chargesNeeded);
        }
        //reset active state after each hit
        else {
            isActive = false;
        }

        SetAbilityState();
    }

    //fills all charges to an ability
    void SetAbilityReady()
    {
        if (_ability is ChargeAbility) {
            currentCharges = chargesNeeded;
        }
    }

    private void SetAbilityState()
    {
        //handle charges for charge abilities
        if (_ability is ChargeAbility) {
            if (currentCharges == chargesNeeded) {
                abilityIsAvailable = true;
                darkMask.enabled = false;
            }
            else {
                darkMask.enabled = true;
                abilityIsAvailable = false;
                darkMask.fillAmount = (-1 * ((float)currentCharges / chargesNeeded)) + 1;
            }
        }
        //handle remaining uses if ability is a non charge ability
        else {
            abilityIsAvailable = remainingUses > 0;
            darkMask.fillAmount = (-1 * ((float)remainingUses / startAmountUses)) + 1;
        }
    }

    //handle trigger of the ability button
    private void ButtonTriggerd()
    {
        //handle different ability types
        if (_ability is ChargeAbility) {
            currentCharges = 0;
        }
        else {
            remainingUses--;
            isActive = true;
        }
        
        SetAbilityState();
        _ability.TriggerAbility();
    }

}
