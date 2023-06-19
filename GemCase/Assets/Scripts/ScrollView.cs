using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrollView : MonoBehaviour
{
    public static ScrollView Instance { get; private set; } = null;

    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _prefabListItem;

    [Space(10)]
    [Header("Sctoll View Events")]
    [SerializeField] private ItemButtonEvent _eventItemClicked;
    [SerializeField] private ItemButtonEvent _eventItemOnSelect;
    [SerializeField] private ItemButtonEvent _eventItemOnSubmit;

    [Space(10)]
    [Header("Default Selected Index")]
    [SerializeField] private int _defaultSelectedIndex = 0;

    [Space(10)]
    [Header("For Testing Only")]
    [SerializeField] private int _testButtonCount = 1;



    public List<ItemButton> ItemButtons { get; private set; } = null;
    public ItemButton GetItemButtonByGemType(GemType _type)
    {
        foreach (ItemButton item in ItemButtons)
        {
            if (!item.Data.Type.Equals(_type))
                continue;

            return item;
        }

        return null;
    }

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (_testButtonCount > 0)
        {
            TestCreateItems(_testButtonCount);
            UpdateAllButtonNavigationalReferences();
        }
        StartCoroutine(DelayedSelectChild(_defaultSelectedIndex));
    }


    public void SelectChild(int index)
    {
        int childCount = _content.transform.childCount;

        if (index >= childCount)
            return;

        GameObject childObject = _content.transform.GetChild(index).gameObject;
        ItemButton item = childObject.GetComponent<ItemButton>();
        item.ObtainSelectionFocus();
    }
    public IEnumerator DelayedSelectChild(int index)
    {
        yield return new WaitForSeconds(1f);

        SelectChild(index);
    }

    private void UpdateAllButtonNavigationalReferences()
    {
        ItemButton[] children = _content.transform.GetComponentsInChildren<ItemButton>();
        if (children.Length < 2)
            return;

        ItemButton item;
        Navigation navigation;

        for (int i = 0; i < children.Length; i++)
        {
            item = children[i];

            navigation = item.gameObject.GetComponent<Button>().navigation;

            navigation.selectOnUp = GetNavigationUp(i, children.Length);
            navigation.selectOnUp = GetNavigationDown(i, children.Length);

            item.gameObject.GetComponent<Button>().navigation = navigation;

        }
    }

    private Selectable GetNavigationDown(int _indexCurrent, int _totalEntries)
    {
        ItemButton item;
        if (_indexCurrent == _totalEntries - 1)//last item
        {
            item = _content.transform.GetChild(0).GetComponent<ItemButton>();
        }
        else
        {
            item = _content.transform.GetChild(_indexCurrent + 1).GetComponent<ItemButton>();
        }
        return item.GetComponent<Selectable>();
    }

    private Selectable GetNavigationUp(int _indexCurrent, int _totalEntries)
    {
        ItemButton item;
        if (_indexCurrent == 0)
        {
            item = _content.transform.GetChild(_totalEntries - 1).GetComponent<ItemButton>();
        }
        else
        {
            item = _content.transform.GetChild(_indexCurrent - 1).GetComponent<ItemButton>();
        }
        return item.GetComponent<Selectable>();
    }

    private void TestCreateItems(int count)
    {
        ItemButtons = new();

        for (int i = 0; i < _testButtonCount; i++)
        {
            CreateItem("Gem Type: " + i , i);
        }
    }
    private ItemButton CreateItem(string strName , int _index)
    {
        GameObject gObj;
        ItemButton item;

        gObj = Instantiate(_prefabListItem, Vector3.zero, Quaternion.identity);
        gObj.transform.SetParent(_content.transform);
        gObj.transform.localScale = new Vector3(1f, 1f, 1f);
        gObj.transform.localPosition = new Vector3();
        gObj.transform.localRotation = Quaternion.Euler(new Vector3());
        gObj.name = strName;

        item = gObj.GetComponent<ItemButton>();
        item.ItemNameValue = strName;

        item.OnSelectEvent.AddListener((ItemButton) => { HandleEventItemOnSelect(item); });
        item.OnClickEvent.AddListener((ItemButton) => { HandleEventItemOnClick(item); });
        item.OnSubmitEvent.AddListener((ItemButton) => { HandleEventItemOnSubmit(item); });

        ItemButton _itemButton = gObj.GetComponent<ItemButton>();
        ItemButtons.Add(_itemButton);
        _itemButton.Initialize(GridParent.DataBase.GetData(_index));

        return item;
    }

    private void HandleEventItemOnSubmit(ItemButton item)
    {
        _eventItemOnSubmit.Invoke(item);
    }

    private void HandleEventItemOnClick(ItemButton item)
    {
        _eventItemClicked.Invoke(item);
    }

    private void HandleEventItemOnSelect(ItemButton item)
    {
        //ScrollViewAutoScroll scrollViewAutoScroll = GetComponent<ScrollViewAutoScroll>();
        //scrollViewAutoScroll.HandleOnSelectChange(item.gameObject);

        _eventItemOnSelect.Invoke(item);
    }
}
