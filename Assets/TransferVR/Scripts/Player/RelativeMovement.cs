using UnityEngine;
using System.Collections;
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;// ¬ 
    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    private CharacterController _charController;
    

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _charController.detectCollisions = false;
 }

    void Update()
    {

        Vector3 movement = Vector3.zero;// ¬ Начинаем с вектора(0, 0, 0), непрерывно добавляя компоненты движения.
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0)
        {// ¬ 
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            Quaternion tmp = target.rotation;// ¬ 
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);// ¬ 
            target.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation,
            direction, rotSpeed * Time.deltaTime);
        }
        movement *= Time.deltaTime;
        _charController.Move(movement);
    }
}