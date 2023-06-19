using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, ISelectHandler, IPointerClickHandler, ISubmitHandler
{
    [SerializeField] private TMP_Text _itemName;

    [SerializeField] private ItemButtonEvent _onSelectEvent;
    [SerializeField] private ItemButtonEvent _onSubmitEvent;
    [SerializeField] private ItemButtonEvent _onClickEvent;

    public ItemButtonEvent OnSelectEvent { get => _onSelectEvent; set => _onSelectEvent = value; }
    public ItemButtonEvent OnSubmitEvent { get => _onSubmitEvent; set => _onSubmitEvent = value; }
    public ItemButtonEvent OnClickEvent { get => _onClickEvent; set => _onClickEvent = value; }
    public string ItemNameValue { get => _itemName.text; set => _itemName.text = value; }

    [SerializeField] private TextMeshProUGUI _countText = null;
    [SerializeField] private TextMeshProUGUI _itemTypeText = null;

    [SerializeField] private Image _itemImage = null;

    public GemData Data { get; private set; }

    private int _count = 0;

    public void Initialize(GemData _data)
    {
        Data = _data;

        _itemImage.sprite = _data.Sprite;
        SetItemTypeText(_data.Type);
        SetCountText();
    }
    public void SetCountText() => _countText.SetText($"Collected Count: {_count}");
    public void SetItemTypeText(GemType _type) => _itemTypeText.SetText($"Gem Type: {_type}");
    public void IncreaseCount()
    {
          _count++;
        Debug.Log("My Type: " + Data.Type);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClickEvent.Invoke(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onSelectEvent.Invoke(this);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        _onSubmitEvent.Invoke(this);
    }
    public void ObtainSelectionFocus()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        _onSelectEvent.Invoke(this);
    }
}
[System.Serializable]
public class ItemButtonEvent : UnityEvent<ItemButton> { }
