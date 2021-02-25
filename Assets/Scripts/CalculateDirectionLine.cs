using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CalculateDirectionLine : MonoBehaviour
{
    const float DOWN_OFFSET = 0.35f;
    const float LINE_LENGTH = 3.5f;

    GameManager _gameManager;
    LineRenderer _lineRenderer;
    CinemachineFreeLook _cam;
    Hit _hit;

    Vector3 _startPosition;
    Vector3 _endPosition;
    AnimationCurve _lineThickness;
    bool ballIsTransparent = false;
    public bool redirectIsActive = false;

    
    
    void Start()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _gameManager = GameManager.instance;
        _hit = GetComponent<Hit>();
        _cam = GameObject.Find("BallCamera").GetComponent<CinemachineFreeLook>();

        ResetLinePoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.gameState == GameState.HIT_PHASE || redirectIsActive) {
            _lineRenderer.enabled = true;
            CalculateEndPoint();
            CalculateLineThickness();
            SetLineRenderer();
        }
        else {
            _lineRenderer.enabled = false;
            ResetLinePoints();
        }
    }

    void CalculateEndPoint()
    {
        //calculate direction of line
        Vector3 direction = Camera.main.transform.forward;
        direction.y = 0f;
        direction = Vector3.Normalize(direction) * LINE_LENGTH;
        
        //if ball is transparent, display line on the floor
        if (!ballIsTransparent) {
            direction.y = Mathf.Lerp(2.5f, DOWN_OFFSET, _cam.m_YAxis.Value);
            Debug.Log(_cam.m_YAxis.Value);
        }

        //search for Collission
        RaycastHit hit;
        Ray ray = new Ray(_startPosition, direction);

        if (Physics.Raycast(ray, out hit, direction.magnitude)) {
            _endPosition = hit.point;
        }
        else {
            _endPosition = _startPosition + direction;
        }
    }

    //sets thickness of direction line proportional to hit power
    void CalculateLineThickness()
    {
        float thickness = Mathf.Lerp(0.1f, 0.4f, _hit.hitPower);
        _lineThickness = new AnimationCurve(new Keyframe(0, thickness), new Keyframe(1.3f, 0.05f));
        _lineRenderer.widthCurve = _lineThickness;
    }

    //set points in line renderer
    void SetLineRenderer()
    {
        _lineRenderer.SetPosition(0, _startPosition);
        _lineRenderer.SetPosition(1, _endPosition);
    }

    //reset points to ball position
    void ResetLinePoints()
    {
        Vector3 tmpPos = transform.position;
        tmpPos.y -= DOWN_OFFSET;
        _startPosition = tmpPos;

        _endPosition = _startPosition;

        SetLineRenderer();
    }

    //check if ball is set to transparent for a different displayment of the direction line
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera") {
            ballIsTransparent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera") {
            ballIsTransparent = false;
        }
    }


}
