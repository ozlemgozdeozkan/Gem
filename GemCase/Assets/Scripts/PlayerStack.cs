using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStack : MonoBehaviour
{
    [SerializeField] private List<Transform> _collectedItems = new();

    [SerializeField] private float _followSpeed = 0.00f;

    public static int coinCount = 0;
    [SerializeField]
    Text coinText;

    private List<PlacableGem> _pinkList = new();
    private List<PlacableGem> _greenList = new();
    private List<PlacableGem> _yellowList = new();

    private void Start()
    {
        coinText.text = coinCount.ToString();
    }

    private void Update()
    {
        foreach (Transform _item in _collectedItems)
        {
            int _index = _collectedItems.IndexOf(_item);

            if (_index.Equals(0))
                _item.position = Vector3.Lerp(_item.position, transform.position + Vector3.up * 0.75f, _followSpeed * Time.deltaTime);
            else
            {
                _item.position = Vector3.Lerp(_item.position, _collectedItems[_index - 1].position + Vector3.back * 0.50f + Vector3.up * 0.50f, _followSpeed * Time.deltaTime);

                _item.position = new(_item.position.x, _item.position.y, _collectedItems[_index - 1].position.z);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlacableGem _gem))
        {
            if (_gem.IsCollectable && !_collectedItems.Contains(other.transform))
            {
                _collectedItems.Add(other.transform);

                switch (_gem.GemData.Type)
                {
                    case GemType.Yellow:
                        _yellowList.Add(_gem);
                        break;
                    case GemType.Green:
                        _greenList.Add(_gem);
                        break;
                    case GemType.Pink:
                        _pinkList.Add(_gem);
                        break;

                }
            }
        }
        if (other.tag == "SalesArea")
        {

            StartCoroutine(CO_DestroyItemsWithDelay());

        }
    }
    private IEnumerator CO_DestroyItemsWithDelay()
    {
        int itemCount = _collectedItems.Count;

        for (int i = itemCount - 1; i >= 0; i--)
        {
            Transform item = _collectedItems[i];


            ItemButton _button = ScrollView.Instance.GetItemButtonByGemType(item.GetComponent<PlacableGem>().GemData.Type);
            PlacableGem _gem = item.GetComponent<PlacableGem>();

            switch (_gem.GemData.Type)
            {
                case GemType.Yellow:
                    _yellowList.Remove(_gem);

                    _button.IncreaseCount();
                    break;
                case GemType.Green:
                    _greenList.Remove(_gem);

                    _button.IncreaseCount();
                    break;
                case GemType.Pink:
                    _pinkList.Remove(_gem);

                    _button.IncreaseCount();
                    break;
            }
            _button.SetCountText();


            _collectedItems.Remove(item);

            //ScrollView.Instance.ItemButtons[ScrollView.Instance.ItemButtons.IndexOf(item.GetComponent<PlacableGem>().GemData)];

            Destroy(item.gameObject);
            float scale = item.localScale.x;
            int gemValue = Mathf.RoundToInt(100 + scale * 100);
            coinCount += gemValue;
            coinText.text = coinCount.ToString();

            yield return new WaitForSeconds(0.1f);

        }
    }

    private void IncreaseGemCount(GemType _type, ItemButton _button)
    {
        foreach (Transform item in _collectedItems)
        {
            PlacableGem _gem = item.GetComponent<PlacableGem>();

            if (_gem.GemData.IsType(_type))
                _button.IncreaseCount();
        }
    }
}