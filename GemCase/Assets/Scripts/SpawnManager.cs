using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Material _gridMaterial = null;
    [SerializeField] private GameObject _gridObject = null;

    [SerializeField] private GameObject _tempObject = null;

    [SerializeField] private Vector3 _firstLocation = Vector3.zero;


    private Vector2Int _gridScale = Vector2Int.zero;

    private Vector3[,] _gridLocations = default;

    private void Awake()
    {
        Vector4 _scale = _gridMaterial.GetVector("_Size");

        _gridScale.x = (int)_scale.x;
        _gridScale.y = (int)_scale.y;


        _gridLocations = new Vector3[_gridScale.y, _gridScale.x];

        BoxCollider _box = _gridObject.GetComponent<BoxCollider>();

        Vector3 _size = new(-_box.bounds.size.x / 2, 0, _box.bounds.size.z / 2);

        _firstLocation = (_gridObject.transform.position + _size) + Vector3.right / 2 + Vector3.back / 2;
    }

    private void Start()
    {
        for (int i = 0; i < _gridScale.y; i++)
        {
            for (int j = 0; j < _gridScale.x; j++)
            {
                float _addedX = 10.00f / _gridScale.x;
                float _addedY = 10.00f / _gridScale.y;

                float _gridX = _firstLocation.x + (_addedX * j);
                float _gridZ = _firstLocation.z - (_addedY * i);

                _gridLocations[i, j] = new(_gridX, 0.50f, _gridZ);

                Instantiate(_tempObject, new(_gridLocations[i, j].x, 0.50f, _gridLocations[i, j].z), Quaternion.identity);
            }
        }
    }

}