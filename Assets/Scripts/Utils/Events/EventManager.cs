using TMPro;
using UnityEngine;
using Event = Utils.Events;

namespace Utils.Events
{
    public class EventManager
    {
        public static readonly Event OnSpawnBoxes = new Event();
        public static readonly Event OnGathererTargetReached = new Event();
        public static readonly Event OnAllBoxesCollected = new Event();
        public static readonly Event OnBoxLanded = new Event();
        public static readonly Event OnAllBoxesLanded = new Event();
        
        public static readonly EventSingle<bool> OnGathererActiveStatusChanged = new EventSingle<bool>();
        public static readonly EventSingle<Box> OnCollectBox = new EventSingle<Box>();
        
        public static readonly EventDouble<int, TMP_Text> OnBoxNumberSet = new EventDouble<int, TMP_Text>();
    }
}
