using UnityEngine;
using DG.Tweening;

public class GridParent : MonoBehaviour
{
    private GameObject _gridObject = null;
    private Material _gridMaterial = null;

    private Vector3 _firstLocation = Vector3.zero;

    private Vector2Int _gridScale = Vector2Int.zero;

    private Vector3[,] _gridLocations = default;

    public static ObjectsDataBase DataBase = null;


    private void Awake()
    {
        DataBase = Resources.Load<ObjectsDataBase>("Database");

        _gridObject = transform.GetComponentInChildren<BoxCollider>().gameObject;
        _gridMaterial = _gridObject.GetComponent<MeshRenderer>().material;


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
            }
        }
        Spawn();
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        for (int i = 0; i < _gridScale.y; i++)
        {
            for (int j = 0; j < _gridScale.x; j++)
            {
                SpawnRandomGemAtPoint(new(_gridLocations[i, j].x, 0.50f, _gridLocations[i, j].z));
            }
        }
    }

    public void SpawnRandomGemAtPoint(Vector3 _point)
    {
        GameObject _gemObject = Instantiate(DataBase.GetRandomGem().gameObject, _point, Quaternion.identity);

        _gemObject.GetComponent<PlacableGem>().Initialize(this);

        _gemObject.transform.localScale = Vector3.zero;

        _gemObject.transform.DOScale(Vector3.one, 5.00f).SetEase(Ease.Linear);
    }
}