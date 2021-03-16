using Filip.Utils;
using UnityEngine;
using Event = Filip.Utils.Event;

namespace Scripts.Events
{
    public class EventManager : MonoBehaviour
    {
        public static readonly Event OnSpawnBox = new Event();

        public static readonly EventSingle<Box> OnCollectBox = new EventSingle<Box>();
    }
}
