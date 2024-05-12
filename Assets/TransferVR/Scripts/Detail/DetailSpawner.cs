using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailSpawner : MonoBehaviour
{
    public float spawnDistnace = 1;
    // Start is called before the first frame update

    private void Awake()
    {
        
        
    }

    void Start()
    {
        //SpawnChilds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnChilds()
    {
        float angle = 360 / transform.childCount;
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            Vector3 spawnPos = new Vector3(
                transform.position.x + spawnDistnace * Mathf.Sin(angle), 
                transform.position.y + spawnDistnace * Mathf.Cos(angle),
                transform.position.z);
            child.GetComponent<Detail_BlueprintLogic>().CreateDetail(spawnPos);
        }
    }
/*

 * 
 */

}
