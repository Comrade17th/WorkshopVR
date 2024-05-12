using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Target : MonoBehaviour
{
    [SerializeField] private TargetTrigger _trigger;

    private void Awake()
    {
        Assert.IsNotNull(_trigger);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void LayDown()
    {
        Debug.Log($"layed down");
    }
}
