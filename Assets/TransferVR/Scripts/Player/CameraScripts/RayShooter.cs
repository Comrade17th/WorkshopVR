using UnityEngine;
using System.Collections;
public class RayShooter : MonoBehaviour
{

    public float TargetDistance = 1;
    private Transform objectToFollow;
    private Transform holdObject;
    private Camera _camera;

    private void Awake()
    {
        objectToFollow = gameObject.transform.GetChild(0);
    }

    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked; // ? �������� ��������� ����
        Cursor.visible = false; // ? � ������ ������
    }
    void Update()
    { //� ��� ������� �� ������� ����� �������� �������� ��� ��� �������� ���� �� �������� 3.1.

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(
            _camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);          
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;// � �������� ������, � ������� ����� ���.
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                { //� ��������� ������� � ����� ������� ���������� ReactiveTarget.
                    holdObject = target.GetComponent<Transform>();
                    Debug.Log("Target hit");
                    target.ReactToHit(objectToFollow);
                }
                //else
                //{
                //    StartCoroutine(SphereIndicator(hit.point));
                //}
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (holdObject != null)
            {
                ReactiveTarget target = holdObject.GetComponent<ReactiveTarget>();
                if (target != null)
                    target.Drop();
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {   //� ����������� ���������� ��������� IEnumerator.
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1); //� �������� ����� yield ��������� �����������, 
        //����� ������� ������������.
        Destroy(sphere);// � ������� ���� GameObject � ������� ������.
    }

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");// � ������� GUI.Label() ���������� �� ������ ������.
    }
}