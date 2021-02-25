using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakThrough : MonoBehaviour
{
    [HideInInspector] public int abilityLayer;
    [HideInInspector] public Material activeMaterial;
    [HideInInspector] public Material defaultMaterial;

    GameManager _gameManager;
    MeshRenderer _graphicLayer;
    int defaultLayer;
    float startTime;

    public void Initialize()
    {
        defaultLayer = gameObject.layer;
        _gameManager = GameManager.instance;
        _graphicLayer = transform.Find("Symbol").GetComponentInChildren<MeshRenderer>();
    }

    public void TriggerBreakThrough()
    {
        startTime = Time.time;
        gameObject.layer = abilityLayer;
        _graphicLayer.material = activeMaterial;

        StartCoroutine(ResetLayer());
    }

    IEnumerator ResetLayer()
    {
        while (_gameManager.gameState == GameState.ROLL_PHASE) {
            yield return new WaitForSeconds(1f);
        }
        gameObject.layer = defaultLayer;
        _graphicLayer.material = defaultMaterial;
    }
}
