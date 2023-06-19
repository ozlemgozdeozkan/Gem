using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class PlacableGem : MonoBehaviour
{
    [SerializeField] private GemData _gemData = null;
    public GemData GemData => _gemData;

    private Vector3 _startPos = Vector3.zero;

    private GridParent _firstParent = null;

    private bool _isCollactable = false;

    public bool IsCollectable => _isCollactable;
    private bool _isGemSpawned = false;


    private void Awake()
    {
        _startPos = transform.position;
        StartCoroutine(nameof(CO_SetCollectable));
    }

    public void Initialize(GridParent _parent)
    {
        _firstParent = _parent;
    }


    private IEnumerator CO_SetCollectable()
    {
        yield return new WaitForSeconds(0.25f);

        _isCollactable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && IsCollectable && !_isGemSpawned)
        {
            transform.DOKill();
            _firstParent.SpawnRandomGemAtPoint(_startPos);
            _isGemSpawned = true;

        }
    }
}