using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Obstacle> _obstacles;
    [SerializeField] private float _spawnPositionZ;
    [SerializeField] private FloatVariableSO _movementDistance;
    public uint UnwalkablePathCount { get; set; }
    private List<SpawnPoint> _spawnPoints;
    private List<Obstacle> _walkableObstacles;

    private void Start()
    {
        CreateSpawnPoints();
        ListWalkableObstacles();
    }

    private void Update()
    {
        foreach(SpawnPoint spawnPoint in _spawnPoints)
        {
            spawnPoint.UpdateTrack();
        }

        foreach(SpawnPoint spawnPoint in _spawnPoints)
        {
            if (spawnPoint.IsSpawnable)
            {
                if (UnwalkablePathCount < 2)
                {
                    Obstacle randomObstacle = _obstacles[Random.Range(0, _obstacles.Count)];
                    Obstacle spawnedObstacle = Instantiate<Obstacle>
                        (randomObstacle, spawnPoint.Position, Quaternion.identity);

                    spawnPoint.Track(spawnedObstacle);

                    if (!spawnedObstacle.IsWalkable) UnwalkablePathCount++;
                }
                else
                {
                    Obstacle randomWalkableObstacle = _walkableObstacles[Random.Range(0, _walkableObstacles.Count)];
                    Obstacle spawnedObstacle = Instantiate<Obstacle>
                        (randomWalkableObstacle, spawnPoint.Position, Quaternion.identity);

                    spawnPoint.Track(spawnedObstacle);
                }
                
            }
        }
    }

    private void CreateSpawnPoints()
    {
        _spawnPoints = new List<SpawnPoint>();

        Vector3 spawnPosition = new Vector3(-_movementDistance.Value, 0, _spawnPositionZ);

        for (int i = 0; i < 3; i++)
        {
            SpawnPoint spawnPoint = new SpawnPoint(this, spawnPosition);
            _spawnPoints.Add(spawnPoint);

            spawnPosition.x += _movementDistance.Value;
        }
    }

    private void ListWalkableObstacles()
    {
        _walkableObstacles = new List<Obstacle>();

        foreach(Obstacle obstacle in _obstacles)
        {
            if (obstacle.IsWalkable)
            {
                _walkableObstacles.Add(obstacle);
            }
        }
    }
}
