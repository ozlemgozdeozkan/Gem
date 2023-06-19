using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data's/Gem")]

public class ObjectsDataBase : ScriptableObject
{
    [SerializeField] private List<GemData> _gems;

    public PlacableGem GetGem(int _index) => _gems[_index].Gem;
    public GemData GetData(int _index) => _gems[_index];

    public PlacableGem GetRandomGem() => GetGem(Random.Range(0, _gems.Count));
}