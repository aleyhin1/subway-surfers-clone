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
        float colliderRadiusZ = GetComponentInChildren<Collider>().bounds.extents.z;
        float spawnInterval = colliderRadiusZ + _safeInterval;

        SpawnInterval = spawnInterval;
    }
}
