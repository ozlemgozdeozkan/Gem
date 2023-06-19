using UnityEngine;

[CreateAssetMenu(menuName = "GemsData", order = 1)]
public class GemData : ScriptableObject
{
    [SerializeField] private PlacableGem _gemPrefab = null;

    [SerializeField] private GemType _gemType = GemType.Yellow;

    [SerializeField] private Sprite _gemSprite = null;


    public PlacableGem Gem => _gemPrefab;
    public Sprite Sprite => _gemSprite;
    public GemType Type => _gemType;


    public bool IsType(GemType _ref) => _gemType.Equals(_ref);
}
public enum GemType
{
    Yellow,
    Green,
    Pink
}