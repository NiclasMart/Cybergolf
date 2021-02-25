using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    GameObject _ball;
    MeshRenderer _ballGrafikLayer;

    public SaveData data;

    [Header("Physik Materialien")]
    public PhysicMaterial[] bounce;
    [Header("Grafik Materialien")]
    public Material[] material;
    [Header("Fähigkeiten")]
    public Ability[] abilitys;
    float[] _gravity = { -8f, -15, -25 };

    //DO DELETE
    //------------
    [Space(10)]
    [Header("Test Einstellungen")]
    public bool UseTestValues = false;
    public BallAbility tAbility;
    public BallBounciness tBounce;
    public WorldGravity tGravity;

    //-----------
    
    public void SetBallProperties(GameObject ball, AbilityManager abilityManager)
    {
        //DO DELETE:
        if (UseTestValues) {
            data.ballProperties.ballAbility = tAbility;
            data.ballProperties.ballBounciness = tBounce;
            data.ballProperties.gravity = tGravity;
        }
        //---

        _ball = ball;
        _ballGrafikLayer = _ball.transform.Find("Graphik").GetComponent<MeshRenderer>();

        SetAbility(abilityManager);
        SetBounce();

        Physics.gravity = new Vector3(0, _gravity[(int)data.ballProperties.gravity], 0);

        //DO DELETE
        Debug.Log(data.ballProperties.ballAbility);
        Debug.Log(data.ballProperties.ballBounciness);
        Debug.Log(Physics.gravity);
        //---
    }

    public void SetProperties(BallAbility ability, BallBounciness bounce, WorldGravity gravity)
    {
        data.ballProperties.ballAbility = ability;
        data.ballProperties.ballBounciness = bounce;
        data.ballProperties.gravity = gravity;
    }

    void SetAbility(AbilityManager manager)
    {
        switch (data.ballProperties.ballAbility) {
            case BallAbility.JUMP:
                manager.Initialize(abilitys[0], _ball);
                break;
            case BallAbility.REDIRECT:
                manager.Initialize(abilitys[1], _ball);
                break;
            case BallAbility.STOP:
                manager.Initialize(abilitys[2], _ball);
                break;
            case BallAbility.BREAK:
                manager.Initialize(abilitys[3], _ball);
                break;
            case BallAbility.NONE:
                manager.Initialize(abilitys[4], _ball);
                break;
        }
    }

    void SetBounce()
    {
        switch (data.ballProperties.ballBounciness) {
            case BallBounciness.SOFT:
                _ball.GetComponent<SphereCollider>().material = bounce[0];
                _ballGrafikLayer.material = material[0];
                break;
            case BallBounciness.NORMAL:
                _ball.GetComponent<SphereCollider>().material = bounce[1];
                _ballGrafikLayer.material = material[1];
                break;
            case BallBounciness.HARD:
                _ball.GetComponent<SphereCollider>().material = bounce[2];
                _ballGrafikLayer.material = material[2];
                break;
        }
    }

}
