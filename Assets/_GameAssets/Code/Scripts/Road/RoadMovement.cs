using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private RoadSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<RoadSpawner>();
    }

    private void Update()
    {
        if (transform.position.z < -_spawner.TileCount * _spawner.TileSideLength * .5f)
        {
            transform.position = Vector3.zero;
        }

        transform.Translate(Vector3.back * _movementSpeed * Time.deltaTime);
    }
}
