using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleReleaser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            PooledObject pooledObject = other.GetComponent<PooledObject>();
            pooledObject.Release();
        }
    }
}
