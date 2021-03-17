using Scripts.Events;
using UnityEngine;

namespace ManagerScripts
{
    public class UIManager : MonoBehaviour
    {
        public void StartGathering()
        {
            EventManager.OnGathererActiveStatusChanged.OnEventRaised?.Invoke(true);
        }

        public void StopGathering()
        {
            EventManager.OnGathererActiveStatusChanged.OnEventRaised?.Invoke(false);
        }
    }
}
