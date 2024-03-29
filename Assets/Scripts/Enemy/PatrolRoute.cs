using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    public enum PatrolType
    {
        Loop = 0,
        PingPong = 1
    }

    [SerializeField] private Color _patrolRouteColor = Color.green;
    public PatrolType patrolType;
    public List<Transform> route;

    [Button("Add Patrol Point")]
    private void AddPatrolPoint()
    {
        GameObject newPoint = new GameObject();
        newPoint.transform.position = transform.position;
        newPoint.transform.parent = transform;
        newPoint.name = "Point" + (route.Count + 1);
        route.Add(newPoint.transform);
        
        #if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(newPoint, "Add Patrol Point");
        #endif
    }

    [Button("Reverse Patrol Route")]
    private void ReversePatrolRoute()
    {
        route.Reverse();
    }

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
            Handles.Label(transform.position, gameObject.name);
        #endif
        
        Gizmos.color = _patrolRouteColor;

        for (int i = 0; i < route.Count - 1; i++)
        {
            Gizmos.DrawLine(route[i].position, route[i + 1].position);
        }

        if (patrolType == PatrolType.Loop)
        {
            Gizmos.DrawLine(route[route.Count - 1].position, route[0].position);
        }        
    }
}
