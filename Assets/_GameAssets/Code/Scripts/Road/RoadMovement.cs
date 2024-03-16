using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : EndlessMover
{
    private RoadSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<RoadSpawner>();
    }

    protected override void Update()
    {
        if (transform.position.z < -_spawner.TileCount * _spawner.TileSideLength * .5f)
        {
            transform.position = Vector3.zero;
        }

        base.Update();
    }
}
