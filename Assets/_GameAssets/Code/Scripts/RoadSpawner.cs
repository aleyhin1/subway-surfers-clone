using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public uint TileCount;
    [HideInInspector] public float TileSideLength;
    [SerializeField] private GameObject _roadTile;
    private const float _offset = 2f;
    private Vector3 _tileSize;

    private void Start()
    {
        CreateRoad();
        CreateCollider();
    }

    private void CreateRoad()
    {
        _tileSize = _roadTile.GetComponent<MeshRenderer>().bounds.size;
        TileSideLength = _tileSize.z - _offset;

        for(int i = 0; i < TileCount; i++)
        {
            Vector3 tilePosition = new Vector3(0, 0, i * TileSideLength);

            Instantiate(_roadTile, tilePosition, Quaternion.identity, transform);
        }
    }

    private void CreateCollider()
    {
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        Vector3 size = new Vector3(_tileSize.x, _tileSize.y, TileSideLength * TileCount);
        Vector3 center = new Vector3(0, 0, TileSideLength * TileCount * .5f);
        collider.size = size;
        collider.center = center;
    }
}
