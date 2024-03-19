using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [field: SerializeField] public bool IsWalkable { get; private set; }
    public float SpawnInterval { get; set; }
    [SerializeField] private float _safeInterval;

    private void Start()
    {
        float colliderRadiusZ = GetColliderRadiusZ();
        float spawnInterval = colliderRadiusZ + _safeInterval;

        SpawnInterval = spawnInterval;
    }

    private float GetColliderRadiusZ()
    {
        float radiusZ = 0;

        foreach(Collider collider in GetComponentsInChildren<Collider>())
        {
            radiusZ += collider.bounds.extents.z;
        }

        return radiusZ;
    }
}
