using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlacementSystem : MonoBehaviour
{
    public static PlacementSystem current;

    [SerializeField] private Grid grid;

    [SerializeField] private Material _gridMaterial = null;

    public GridLayout _gridLayout;


    [SerializeField] private ObjectsDataBase dataBase;

    public Vector3Int Size { get; private set; }
    private Vector3[] Vertices;

    private Vector2 _gridScale = default;


    private void Awake()
    {
        current = this;
        grid = _gridLayout.gameObject.GetComponent<Grid>();

        Vector4 _scale = _gridMaterial.GetVector("_Size");
        _gridScale = new(_scale.x, _scale.y);
    }
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = _gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }


    private Vector3 GetFirstGrid()
    {
        return SnapCoordinateToGrid(Vector3.zero);
    }

}