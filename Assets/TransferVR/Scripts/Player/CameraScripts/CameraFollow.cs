using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    public Transform objectToFollow;
    private Vector3 deltaPos;
    
    // Start is called before the first frame update
    void Start()
    {
        deltaPos = transform.position - objectToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.position + deltaPos;
    }
}
