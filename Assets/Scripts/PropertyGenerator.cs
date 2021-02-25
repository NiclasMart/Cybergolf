using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PropertyGenerator : MonoBehaviour
{
    public SaveData data;
    public WheelSpin abWheel;
    public WheelSpin boWheel;
    public WheelSpin grWheel;

    Properties newProperties;

    BallAbility[] _abilities = { BallAbility.JUMP, BallAbility.STOP, BallAbility.BREAK, BallAbility.REDIRECT };
    BallBounciness[] _bounce = { BallBounciness.SOFT, BallBounciness.NORMAL, BallBounciness.HARD };
    WorldGravity[] _gravity = { WorldGravity.LOW, WorldGravity.NORMAL, WorldGravity.HIGH };

    //tabels with rotation forces for wheels
    float[] abilityTable = new float[] { 5.4f, 10.3f, 11.7f, 8.7f }; //Jump, Stop, Break, Redirect
    float[] bounceTable = new float[] { 11.8f, 7f, 9.3f };   //soft, normal, hard
    float[] gravityTable = new float[] { 9.9f, 7.3f, 10.5f }; //low, normal, high

    //tabel with very hard combinations
    Properties[] hardCombinations = new Properties[] {
        new Properties(BallAbility.JUMP, BallBounciness.SOFT, WorldGravity.LOW),
        new Properties(BallAbility.JUMP, BallBounciness.SOFT, WorldGravity.NORMAL),
        new Properties(BallAbility.JUMP, BallBounciness.SOFT, WorldGravity.HIGH),
        new Properties(BallAbility.BREAK, BallBounciness.SOFT, WorldGravity.LOW),
        new Properties(BallAbility.STOP, BallBounciness.HARD, WorldGravity.HIGH),
        new Properties(BallAbility.STOP, BallBounciness.HARD, WorldGravity.NORMAL),
        
    };

    //table with easy combinations
    Properties[] easyCombinations = new Properties[] {
        new Properties(BallAbility.REDIRECT, BallBounciness.NORMAL, WorldGravity.NORMAL),
        new Properties(BallAbility.REDIRECT, BallBounciness.SOFT, WorldGravity.HIGH),
        new Properties(BallAbility.STOP, BallBounciness.NORMAL, WorldGravity.NORMAL),
        new Properties(BallAbility.JUMP, BallBounciness.NORMAL, WorldGravity.NORMAL),
        new Properties(BallAbility.STOP, BallBounciness.SOFT, WorldGravity.HIGH),
    };

    private void Update()
    {
        //check if all wheels are ready
        if (abWheel.state == WheelState.STOPED && grWheel.state == WheelState.STOPED && boWheel.state == WheelState.STOPED) {
            BtnHandler.instance.ActivateStartButton();
        }
    }

    public void GenerateBallProperties()
    {
        newProperties = new Properties();

        //generate new Properties until the combination is allowed
        bool combinationIsAllowed = false;
        while (!combinationIsAllowed) {
            //generate properties according to difficulty mode
            switch (data.difficultyMode) {
                case Difficulty.EASYMODE:
                    newProperties = easyCombinations[UnityEngine.Random.Range(0, easyCombinations.Length)];
                    combinationIsAllowed = true;
                    break;
                case Difficulty.HARDMODE:
                    newProperties = hardCombinations[UnityEngine.Random.Range(0, hardCombinations.Length)];
                    combinationIsAllowed = true;
                    break;
                case Difficulty.NORMAL:
                    newProperties.ballBounciness = GenerateBounciness();
                    newProperties.ballAbility = GenerateAbility();
                    newProperties.gravity = GenerateGravity();
                    combinationIsAllowed = CheckCombination();
                    break;
            }
        }
        //spin each wheel according to properties
        SpinAllWheels();

        //save new Properties
        data.ballProperties = newProperties;
    }

    //returns random ability
    private BallAbility GenerateAbility()
    {
        return _abilities[UnityEngine.Random.Range(0, _abilities.Length)];
    }

    //returns random Bounce Value
    private BallBounciness GenerateBounciness()
    {
        return _bounce[UnityEngine.Random.Range(0, _bounce.Length)];
    }

    //returns random Gravity Value
    private WorldGravity GenerateGravity()
    {
        return _gravity[UnityEngine.Random.Range(0, _gravity.Length)];
    }


    private void SpinAllWheels()
    {
        //search for spin power for the Ability wheel and start spinning
        int index = Array.IndexOf(_abilities, newProperties.ballAbility);
        abWheel.StartSpin(abilityTable[index]);

        //search for spin power for the Bounce wheel and start spinning
        index = Array.IndexOf(_bounce, newProperties.ballBounciness);
        boWheel.StartSpin(bounceTable[index]);

        //search for spin power for the Gravity wheel and start spinning
        index = Array.IndexOf(_gravity, newProperties.gravity);
        grWheel.StartSpin(gravityTable[index]);
    }

    //check if combination is allowed
    bool CheckCombination()
    {
        //check if combination is hard
        foreach (Properties p in hardCombinations) {
            if (newProperties.Equals(p)) {
                return false;
            };
        }

        //check if combination is easy
        foreach (Properties p in easyCombinations) {
            if (newProperties.Equals(p)) {
                return false;
            };
        }
        return true;
    }
}
