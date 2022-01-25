using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class RayReflector
{
    public static Vector3[] GetRayReflectionPositions(Vector3 position, Vector3 direction, int maxReflectionCount = 2, 
        float maxStepDistance = 500f)
    {
        List<Vector3> trajectoryPositions = new List<Vector3> {position};

        for (int i = 0; i <= maxReflectionCount; i++)
        {
            Ray ray = new Ray(position, direction);
            
            if (Physics.Raycast(ray, out RaycastHit hit, maxStepDistance))
            {
                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
            }
            else
            {
                position += direction * maxStepDistance;
            }

            trajectoryPositions.Add(position);
        }

        return trajectoryPositions.ToArray();
    }
}
