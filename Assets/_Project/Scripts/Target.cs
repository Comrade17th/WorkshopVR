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
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    private void LayDown()
    {
        
    }
}
