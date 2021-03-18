using Filip.Utils;
using TMPro;
using UnityEngine;
using Event = Filip.Utils.Event;

namespace Scripts.Events
{
    public class EventManager : MonoBehaviour
    {
        public static readonly Event OnSpawnBoxes = new Event();
        public static readonly Event OnSpawnBox = new Event();
        public static readonly Event OnGathererTargetReached = new Event();
        
        public static readonly EventSingle<bool> OnGathererActiveStatusChanged = new EventSingle<bool>();
        public static readonly EventSingle<Box> OnCollectBox = new EventSingle<Box>();
        
        public static readonly EventDouble<int, TMP_Text> OnBoxNumberSet = new EventDouble<int, TMP_Text>();
    }
}
