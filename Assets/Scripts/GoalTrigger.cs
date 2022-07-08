using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalTrigger : MonoBehaviour
{
    public UnityEvent onGoalReached;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        onGoalReached.Invoke();
    }
}
