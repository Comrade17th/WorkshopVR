using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Random = UnityEngine.Random;

public class Detail_BlueprintLogic : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public List<Transform> nextStages = new List<Transform>();
    [SerializeField] public List<Transform> previousStages = new List<Transform>();

    public Material origin;
    public Material baseBlueprint;
    public Material activeBlueprint;
    public Material invisibleBlueprint;
    
    private Renderer renderer;
    private Collider _collider;
    private Throwable _throwable;
    private Interactable _interactable;
    
    private int condition;
    private bool needChangeMat = false;

    public Transform copyChild;
    public Transform originalParent;

    public float maxSpawnRadius = 2;
    public float minSpawnRadius = 1;

    public float CollisionAccuracy = 0.05f;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
        _throwable = GetComponent<Throwable>();
        _interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        renderer.enabled = true;
        origin = renderer.material;
        

        if(originalParent == null) // if its original
        {
            ChangeCondition(0);
            CreateDetail(transform.position + new Vector3(1, 1, 0));
            SetMeNotInterectable();
        }
        else // if its copied
        {
            ChangeCondition(2);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (needChangeMat)
        {
            switch (condition)
            {
                case 0: 
                    renderer.material = baseBlueprint;
                    needChangeMat = false;
                    break;
                case 1:
                    renderer.material = activeBlueprint;
                    needChangeMat = false;
                    break;
                case 2:
                    renderer.material = origin;
                    needChangeMat = false;
                    break;
                case 3:
                    renderer.material = invisibleBlueprint;
                    needChangeMat = false;
                    break;
            }
                
        }

        if(copyChild != null)
        {
            if (copyChild.GetComponent<ReactiveTarget>().isFollow == true)
            {
                ChangeCondition(1);
            }
            else if (isPreviousStagesDone())
            {
                ChangeCondition(0);
            }
            else
            {
                ChangeCondition(3);
            }
            
        }
    }

    private void SetMeNotInterectable()
    {
        _collider.isTrigger = true;
        Destroy(_throwable);
        Destroy(_interactable);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"In Trigger entered {gameObject.name}\n{other.gameObject.name}");
        
        if (other.transform == copyChild)
        {
            Debug.Log($"with child triggered");
            Destroy(copyChild.gameObject);
            ChangeCondition(2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision entered {gameObject.name}\n{collision.gameObject.name}");
        
        if (collision.transform == copyChild)
        {
            //GetComponent<Rigidbody>().
            Debug.Log($"with child collided");
            Destroy(copyChild.gameObject);
            ChangeCondition(2);
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        Debug.Log($"Collision stay {gameObject.name}\n{collision.gameObject.name}");            
        if (collision.transform == copyChild)
        {
            Destroy(copyChild.gameObject);
            ChangeCondition(2);
        }
    }

    private bool IsOriginal()
    {
        if (originalParent == null)
            return true;
        else
            return false;
    }

    private bool isPreviousStagesDone()
    {
        bool isDone = true;
        //if(previousStages.Count == 0)
        foreach(Transform prev in previousStages)
        {
            if(prev.GetComponent<Detail_BlueprintLogic>().condition != 2)
                isDone = false;
        }
        return isDone;
    }

    private Vector3 randomPosAround(Vector3 centerPos)
    {
        if(Random.Range(0, 100) < 50)
        return new Vector3(
            centerPos.x + Random.Range(minSpawnRadius * 10, maxSpawnRadius * 10)/10f,
            0.5f,
            centerPos.z + Random.Range(minSpawnRadius * 10, maxSpawnRadius * 10)/10f
            );
        else
            return new Vector3(
            centerPos.x - Random.Range(minSpawnRadius * 10, maxSpawnRadius * 10) / 10f,
            0.5f,
            centerPos.z - Random.Range(minSpawnRadius * 10, maxSpawnRadius * 10)/10f
            );
    }

    public void CreateDetail(Vector3 position)
    {
        position = randomPosAround(position);
        GameObject newCopy = Instantiate(gameObject, position ,this.transform.rotation);
        newCopy.GetComponent<Detail_BlueprintLogic>().ChangeCondition(2);
        newCopy.GetComponent<Detail_BlueprintLogic>().SetParent(transform);
        copyChild = newCopy.GetComponent<Transform>();
        
        //newCopy.GetComponent<ReactiveTarget>().isReactive = true;
                //newCopy.GetComponent<BoxCollider>().isTrigger = false;

        //newCopy.GetComponent<ReactiveTarget>().link1 = gameObject.GetComponent<BoxCollider>();
        //newCopy.GetComponent<ReactiveTarget>().link2 = newCopy.GetComponent<BoxCollider>();

        //gameObject.GetComponent<ReactiveTarget>().link1 = gameObject.GetComponent<BoxCollider>();
        //gameObject.GetComponent<ReactiveTarget>().link2 = newCopy.GetComponent<BoxCollider>();

    }

    public void SetParent(Transform parent)
    {
        originalParent = parent;
    }

    

    public void ChangeCondition(int condition)
    {
        needChangeMat = true;
        this.condition = condition;
    }

}
