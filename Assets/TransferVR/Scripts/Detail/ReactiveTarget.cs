using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public bool isFollow = false; // true if it holded by player
    public Transform objectToFollow;
    private Vector3 deltaPos;
    public bool isReactive = false;

    public BoxCollider link1;
    public BoxCollider link2;
    
    private void Update()
    {
        if (isFollow)
        {
            FollowTarget();
            if(link1 != null)
            link1.isTrigger = false;
            if (link2 != null)
            link2.isTrigger = false;

        }
        else
        {
            if (link1 != null)
                link1.isTrigger = true;
            if (link2 != null)
                link2.isTrigger = true;
        }
    }

    private void FollowTarget()
    {
        transform.position = objectToFollow.position + deltaPos;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        isFollow = false;
        objectToFollow = null;
        deltaPos = Vector3.zero;
    }

    public void ReactToHit(Transform objectToFollow)
    {// � �����, ��������� ��������� ��������.
        if (isReactive)
        {
            transform.position = objectToFollow.position;
            deltaPos = transform.position - objectToFollow.position;
            this.objectToFollow = objectToFollow;
            isFollow = true;
        }
        
        //StartCoroutine(Die());
    }

}