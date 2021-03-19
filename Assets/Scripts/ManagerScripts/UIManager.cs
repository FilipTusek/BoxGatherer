using System;
using Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ManagerScripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberOfBoxesText;
        [SerializeField] private Button _spawnBoxesButton;

        private void OnEnable()
        {
            EventManager.OnAllBoxesCollected.OnEventRaised += EnableBoxSpawningButtonInteraction;
        }

        private void OnDisable()
        {
            EventManager.OnAllBoxesCollected.OnEventRaised -= EnableBoxSpawningButtonInteraction;
        }

        private void Start()
        {
            AddNumberOfBoxesToSpawn(5);
        }

        public void StartGathering()
        {
            EventManager.OnGathererActiveStatusChanged.OnEventRaised?.Invoke(true);
        }

        public void StopGathering()
        {
            EventManager.OnGathererActiveStatusChanged.OnEventRaised?.Invoke(false);
        }

        public void AddNumberOfBoxesToSpawn(int numBoxes)
        {
            EventManager.OnBoxNumberSet.OnEventRaised?.Invoke(numBoxes, _numberOfBoxesText);
        }
        
        public void SpawnBoxes()
        {
            _spawnBoxesButton.interactable = false;
            EventManager.OnSpawnBoxes.OnEventRaised?.Invoke();
        }

        private void EnableBoxSpawningButtonInteraction()
        {
            _spawnBoxesButton.interactable = true;
        }
    }
}
