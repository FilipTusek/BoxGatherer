using System.Collections.Generic;
using UnityEngine.Events;

namespace Filip.Utils
{
    public class Event
    {
        public UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }

    public class EventSingle<T>
    {
        public UnityAction<T> OnEventRaised;

        public void RaiseEvent(T item1)
        {
            OnEventRaised?.Invoke(item1);
        }
    }

    public class EventDouble<T, TU>
    {
        public UnityAction<T, TU> OnEventRaised;
        
        public void RaiseEvent(T item1, TU item2)
        {
            OnEventRaised?.Invoke(item1, item2);
        }
    }

    public class EventTriple<T, TU, TV>
    {
        public UnityAction<T, TU, TV> OnEventRaised;
        
        public void RaiseEvent(T item1, TU item2, TV item3)
        {
            OnEventRaised?.Invoke(item1, item2, item3);
        }
    }
}