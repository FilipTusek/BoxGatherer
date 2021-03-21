using UnityEngine;
using Utils.Events;

public class Container : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
            EventManager.OnCollectBox.OnEventRaised?.Invoke(other.GetComponent<Box>());
    }
}
