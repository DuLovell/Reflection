using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryVizualizer : MonoBehaviour
{
    [SerializeField] private float _originVerticalOffset;

    private LineRenderer _trajectoryRenderer;
    private HitIndicatorFactory _indicatorFactory;

    private const int DEFAULT_INDICATORS_CAPACITY = 5;
    
    private Vector3 _originPosition;

    private void Awake()
    {
        Cursor.visible = false;
        
        _trajectoryRenderer = GetComponent<LineRenderer>();
        _indicatorFactory = new HitIndicatorFactory(DEFAULT_INDICATORS_CAPACITY);
        
        SetOriginPosition();
    }
    
    private void Update()
    {
        Vector3[] trajectoryPositions = RayReflector.GetRayReflectionPositions(_originPosition, transform.forward);

        DrawTrajectory(trajectoryPositions);
        SetIndicators(trajectoryPositions);
    }

    private void SetOriginPosition()
    {
        _originPosition = transform.position;
        _originPosition.y += _originVerticalOffset;
    }

    private void DrawTrajectory(Vector3[] trajectoryPositions)
    {
        _trajectoryRenderer.positionCount = trajectoryPositions.Length;
        _trajectoryRenderer.SetPositions(trajectoryPositions);
    }

    private void SetIndicators(Vector3[] trajectoryPositions)
    {
        _indicatorFactory.ReleaseAllIndicators();
        for (int i = 1; i < trajectoryPositions.Length; i++)
        {
            _indicatorFactory.GetIndicator().position = trajectoryPositions[i];
        }
    }
}
