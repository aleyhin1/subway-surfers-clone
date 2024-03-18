using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint
{
    public Vector3 Position { get; private set; }
    public bool IsSpawnable {  get; private set; }
    public Obstacle SpawnedObstacle { get; private set; }
    private ObstacleSpawner _spawner;

    public void UpdateTrack()
    {
        if (SpawnedObstacle != null && IsSpawnComplete())
        {
            if (!SpawnedObstacle.IsWalkable) _spawner.UnwalkablePathCount--;

            SpawnedObstacle = null;
            IsSpawnable = true;
        }
    }

    public SpawnPoint(ObstacleSpawner spawner, Vector3 position)
    {
        Position = position;
        IsSpawnable = true;
        _spawner = spawner;
    }

    public void Track(Obstacle spawnedObstacle)
    {
        IsSpawnable = false;
        SpawnedObstacle = spawnedObstacle;
    }

    private bool IsSpawnComplete()
    {
        return SpawnedObstacle.transform.position.z < Position.z - SpawnedObstacle.SpawnInterval;
    }
}
