using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    //public float rotSpeed = 1.5f;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    public float minimumVert = 0.0f;
    public float maximumVert = 30.0f;

    private float _rotX = 0;
    private float _rotY;
    private Vector3 _offset;

    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
        }
    void LateUpdate()
    {
        //float horInput = Input.GetAxis("Horizontal");

        _rotX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotX = Mathf.Clamp(_rotX, minimumVert, maximumVert);
        float delta = Input.GetAxis("Mouse X") * sensitivityHor;

        _rotY = transform.localEulerAngles.y + delta;
        _rotY += Input.GetAxis("Mouse X") * sensitivityHor;
        

        Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);
    }
}
