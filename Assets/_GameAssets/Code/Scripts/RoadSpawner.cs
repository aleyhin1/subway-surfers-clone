using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public uint TileCount;
    [HideInInspector] public float TileSideLength;
    [SerializeField] private GameObject _roadTile;
    private const float _offset = 2f;

    private void Start()
    {
        CreateRoad();
    }

    private void CreateRoad()
    {
        Vector3 tileSize = _roadTile.GetComponent<MeshRenderer>().bounds.size;
        TileSideLength = tileSize.z - _offset;

        for(int i = 0; i < TileCount; i++)
        {
            Vector3 tilePosition = new Vector3(0, 0, i * TileSideLength);

            Instantiate(_roadTile, tilePosition, Quaternion.identity, transform);
        }
    }
}
