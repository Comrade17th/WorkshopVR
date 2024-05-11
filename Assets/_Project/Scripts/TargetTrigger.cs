using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    [SerializeField] private string _triggerTag = "Bullet";
    [SerializeField] private ParticleSystem _effect;
    
    public event Action TargetShooted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_triggerTag))
        {
            TargetShooted?.Invoke();
            Instantiate(_effect, transform.position, transform.rotation);
        }
    }
}
