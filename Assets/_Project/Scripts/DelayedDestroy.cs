using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    [SerializeField] private float _delay = 1.5f;

    private void Start()
    {
        Destroy(gameObject, _delay);
    }
}
