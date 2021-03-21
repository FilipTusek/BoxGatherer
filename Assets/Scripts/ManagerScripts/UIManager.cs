using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Events;

namespace ManagerScripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberOfBoxesText;
        [SerializeField] private Button _spawnBoxesButton;
        [SerializeField] private Button _startGatheringButton;

        private void OnEnable()
        {
            EventManager.OnAllBoxesCollected.OnEventRaised += EnableBoxSpawningButtonInteraction;
            EventManager.OnAllBoxesCollected.OnEventRaised += DisableStartGatheringButtonInteraction;
            EventManager.OnAllBoxesLanded.OnEventRaised += EnableStartGatheringButtonInteraction;
        }

        private void OnDisable()
        {
            EventManager.OnAllBoxesCollected.OnEventRaised -= EnableBoxSpawningButtonInteraction;
            EventManager.OnAllBoxesCollected.OnEventRaised -= DisableStartGatheringButtonInteraction;
            EventManager.OnAllBoxesLanded.OnEventRaised -= EnableStartGatheringButtonInteraction;
        }

        private void Start()
        {
            AddNumberOfBoxesToSpawn(5);
            DisableStartGatheringButtonInteraction();
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

        private void DisableStartGatheringButtonInteraction()
        {
            _startGatheringButton.interactable = false;
        }

        private void EnableStartGatheringButtonInteraction()
        {
            _startGatheringButton.interactable = true;
        }
    }
}
