using Scripts.Events;
using TMPro;
using UnityEngine;

namespace ManagerScripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberOfBoxesText;
        
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
            EventManager.OnSpawnBoxes.OnEventRaised?.Invoke();
        }
    }
}
