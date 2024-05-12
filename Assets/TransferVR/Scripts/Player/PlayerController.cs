using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] Transform target;
    private Rigidbody rb;
    public float force;
    public float jumpfForce;
    public float jumpDelay = 1f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if(horInput != 0 || vertInput != 0)
        {
            movement.x = horInput;
            movement.z = vertInput;

            Quaternion tmp = target.rotation;

            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            movement = transform.TransformDirection(movement);
            _characterController.Move(movement * force * Time.deltaTime);
            //rb.AddForce(movement * force * Time.fixedDeltaTime);
        }

        //if (Input.GetKey(KeyCode.W))
        //    rb.AddForce(Vector3.forward * force * Time.fixedDeltaTime);
        //if (Input.GetKey(KeyCode.S))
        //    rb.AddForce(Vector3.forward * -force * Time.fixedDeltaTime);
        //if (Input.GetKey(KeyCode.A))
        //    rb.AddForce(Vector3.left * force * Time.fixedDeltaTime);
        //if (Input.GetKey(KeyCode.D))
        //    rb.AddForce(Vector3.right * force * Time.fixedDeltaTime);
        if(jumpDelay < 0) jumpDelay = 0;
        if(jumpDelay > 0) jumpDelay -= Time.fixedDeltaTime;

        if (Input.GetAxis("Jump") != 0 && jumpDelay == 0)
        {
            jumpDelay = 1;
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpfForce * Time.fixedDeltaTime);
        }
            
    }
}
