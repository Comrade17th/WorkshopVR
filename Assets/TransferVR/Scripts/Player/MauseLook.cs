using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensivityHor = 9.0f;
    public float sensivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotatoinX = 0;

    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if(body != null)
            body.freezeRotation = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
        }   
        else if(axes == RotationAxes.MouseY)
        {
            _rotatoinX -= Input.GetAxis("Mouse Y") * sensivityVert;

            _rotatoinX = Mathf.Clamp(_rotatoinX, minimumVert, maximumVert);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotatoinX, rotationY, 0);

        }
        else
        {
            _rotatoinX -= Input.GetAxis("Mouse Y") * sensivityHor;
            _rotatoinX = Mathf.Clamp(_rotatoinX, minimumVert, maximumVert);
            float delta = Input.GetAxis("Mouse X") * sensivityVert;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotatoinX, rotationY, 0);

        }
    }
}
