using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 20;
    [SerializeField] float actualTime = 0;
    [SerializeField] float RandomRangePosition;
    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        if(actualTime >= timeToSpawn)
        {
        }
    }
    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-RandomRangePosition, RandomRangePosition), 0.5f, Random.Range(-RandomRangePosition, RandomRangePosition));
    }
}
