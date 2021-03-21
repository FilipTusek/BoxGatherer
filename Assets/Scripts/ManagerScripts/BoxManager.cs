using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils.Events;
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
    private int _numBoxesToCollect;
    private int _numGatheredBoxes = 0;
    private int _numBoxesLanded;

    private void OnEnable()
    {
        EventManager.OnBoxNumberSet.OnEventRaised += AddBoxesToSpawn;
        EventManager.OnSpawnBoxes.OnEventRaised += SpawnBoxes;
        EventManager.OnCollectBox.OnEventRaised += CollectBox;
        EventManager.OnBoxLanded.OnEventRaised += AddToLandedBoxes;
    }

    private void OnDisable()
    {
        EventManager.OnBoxNumberSet.OnEventRaised -= AddBoxesToSpawn;
        EventManager.OnSpawnBoxes.OnEventRaised -= SpawnBoxes;
        EventManager.OnCollectBox.OnEventRaised -= CollectBox;
        EventManager.OnBoxLanded.OnEventRaised -= AddToLandedBoxes;
    }

    private void Start()
    {
        _spawnBounds.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void AddBoxesToSpawn(int numBoxes, TMP_Text text)
    {
        if (_numBoxesToSpawn + numBoxes <= _maxNumberOfBoxes && _numBoxesToSpawn + numBoxes > 0) {
            _numBoxesToSpawn += numBoxes;
            text.text = "" + _numBoxesToSpawn;
        }
        else {
            Debug.Log("Number of boxes must be between 1 and " + _maxNumberOfBoxes);
        }
    }

    private void SpawnBoxes()
    {
        _numBoxesToCollect = _numBoxesToSpawn;
        for (int i = 0; i < _numBoxesToSpawn; i++)
            SpawnBox();
    }

    private void SpawnBox()
    {
        var boxTypeIndex = Random.Range(0, Enum.GetValues(typeof(Box.BoxType)).Length);
        Box.BoxType boxType = (Box.BoxType) boxTypeIndex;
        var box = GetBox();
        box.SetType(boxType, boxType == Box.BoxType.Blue ? _blueColor : _redColor);
        box.transform.SetParent(_boxParent);
        box.transform.position = GetSpawnPosition();
        box.gameObject.SetActive(true);
    }

    private void CollectBox(Box box)
    {
        box.gameObject.SetActive(false);
        _boxPool.Enqueue(box);
        _numGatheredBoxes++;
        CheckIfAllBoxesCollected();
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

    private void CheckIfAllBoxesCollected()
    {
        if (_numGatheredBoxes == _numBoxesToCollect) {
            EventManager.OnAllBoxesCollected.OnEventRaised?.Invoke();
            _numGatheredBoxes = 0;
            _numBoxesLanded = 0;
        }
    }

    private void AddToLandedBoxes()
    {
        _numBoxesLanded++;
        CheckIfAllBoxesLanded();
    }

    private void CheckIfAllBoxesLanded()
    {
        if (_numBoxesLanded == _numBoxesToCollect) EventManager.OnAllBoxesLanded.OnEventRaised?.Invoke();
    }
}