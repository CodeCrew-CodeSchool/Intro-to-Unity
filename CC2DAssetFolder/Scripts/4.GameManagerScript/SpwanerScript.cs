using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanerScript : MonoBehaviour
{
    [Tooltip("Where the object will spawn from")]
    public Transform[] spawnPositions;

    [Tooltip("The gameObject that will spawn")]
    public GameObject[] spawnObjects;
    private Vector2 spawnOffsetX;
    private Vector2 spawnOffsetY;

    public bool spawnAtAllTime = true;

    [Range(1, 100)]
    private float spawnDist = 10.00f;
    private float dist;

    [Range(0.1f, 10f)]
    [Tooltip("The amount of time it takes to spawn on object")]
    public float spawnTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    

    IEnumerator SpawnTimer()
    {
        bool go = true;
        if (spawnAtAllTime)
        {
            go = true;
        }
        else
        {
            if (dist < spawnDist)
            {
                go = true;
            }
            else if ((dist > spawnDist))
            {
                go = false;
            }
        }
        yield return new WaitForSeconds(spawnTime);
        if (go)
        {
            
            spawnLauncher();
            
        }
        StartCoroutine(SpawnTimer());
    }

    public void spawnLauncher()
    {
        Transform spawnSpot = spawnPositions[Random.Range(0, spawnPositions.Length)];
        GameObject Object = spawnObjects[Random.Range(0, spawnObjects.Length)];

        Instantiate(Object, spawnSpot.position, Quaternion.identity);
    }

    public Vector3 GetPosition(Transform p)
    {
        return p.position;
    }
    
}
