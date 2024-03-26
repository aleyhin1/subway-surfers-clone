using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnPositionZ;
    [SerializeField] private FloatVariableSO _movementDistance;
    public uint UnwalkablePathCount { get; set; }
    private List<SpawnPoint> _spawnPoints;
    private List<PooledObject> _walkableObstacles;
    private ObjectPool _objectPool;

    private void Awake()
    {
        _objectPool = GetComponent<ObjectPool>();
    }

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
                    PooledObject randomObstacle =
                        _objectPool.ObjectsToPool[Random.Range(0, _objectPool.ObjectsToPool.Count)];
                    PooledObject pooledObject = _objectPool.GetPooledObject(randomObstacle);

                    pooledObject.transform.position = spawnPoint.Position;

                    Obstacle obstacle = pooledObject.GetComponent<Obstacle>();
                    spawnPoint.Track(obstacle);

                    if (!obstacle.IsWalkable) UnwalkablePathCount++;
                }
                else
                {
                    PooledObject randomWalkableObstacle = _walkableObstacles[Random.Range(0, _walkableObstacles.Count)];
                    PooledObject pooledObject = _objectPool.GetPooledObject(randomWalkableObstacle);
                    pooledObject.transform.position = spawnPoint.Position;

                    Obstacle obstacle = pooledObject.GetComponent<Obstacle>();
                    spawnPoint.Track(obstacle);
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
        _walkableObstacles = new List<PooledObject>();

        foreach(PooledObject obstacle in _objectPool.ObjectsToPool)
        {
            if (obstacle.GetComponent<Obstacle>().IsWalkable)
            {
                _walkableObstacles.Add(obstacle);
            }
        }
    }
}
