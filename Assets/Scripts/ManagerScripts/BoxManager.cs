using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Events;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnBounds;
    [SerializeField] private Transform _boxParent;
    [SerializeField] private GameObject _boxPrefab;

    [Space(10.0f)] [SerializeField] private Color _redColor;
    [SerializeField] private Color _blueColor;

    private Queue<Box> _boxPool = new Queue<Box>();

    private int _maxNumberOfBoxes = 10;
    private int _numBoxesToSpawn;

    private void OnEnable()
    {
        EventManager.OnBoxNumberSet.OnEventRaised += AddBoxesToSpawn;
        EventManager.OnSpawnBoxes.OnEventRaised += SpawnBoxes;
        EventManager.OnCollectBox.OnEventRaised += CollectBox;
    }

    private void OnDisable()
    {
        EventManager.OnBoxNumberSet.OnEventRaised -= AddBoxesToSpawn;
        EventManager.OnSpawnBoxes.OnEventRaised -= SpawnBoxes;
        EventManager.OnCollectBox.OnEventRaised -= CollectBox;
    }

    private void Start()
    {
        _spawnBounds.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void AddBoxesToSpawn(int numBoxes, TMP_Text text)
    {
        if (_numBoxesToSpawn + numBoxes <= _maxNumberOfBoxes) {
            _numBoxesToSpawn += numBoxes;
            text.text = "" + _numBoxesToSpawn;
        }
    }

    private void SpawnBoxes()
    {
        for (int i = 0; i < _numBoxesToSpawn; i++)
            SpawnBox();
    }

    private void SpawnBox()
    {
        var boxTypeIndex = Random.Range(0, Enum.GetValues(typeof(Box.BoxType)).Length);
        Box.BoxType boxType = (Box.BoxType) boxTypeIndex;
        var box = GetBox();
        box.SetType(boxType, boxType == Box.BoxType.Blue ? _blueColor : _redColor);
        box.transform.position = GetSpawnPosition();
    }

    private void CollectBox(Box box)
    {
        box.gameObject.SetActive(false);
        _boxPool.Enqueue(box);
    }

    private Box GetBox()
    {
        return _boxPool.Count == 0 ? Instantiate(_boxPrefab, _boxParent).GetComponent<Box>() : _boxPool.Dequeue();
    }

    private Vector2 GetSpawnPosition()
    {
        var position = _spawnBounds.position;
        var localScale = _spawnBounds.localScale;
        var minMaxPosX = new Vector2(position.x - localScale.x / 2, position.x + localScale.x / 2);
        var minMaxPosY = new Vector2(position.y - localScale.y / 2, position.y + localScale.y / 2);
        return new Vector2(Random.Range(minMaxPosX.x, minMaxPosX.y), Random.Range(minMaxPosY.x, minMaxPosY.y));
    }
}