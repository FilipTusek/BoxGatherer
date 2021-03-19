using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Events;
using UnityEngine;

public class Container : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
            EventManager.OnCollectBox.OnEventRaised?.Invoke(other.GetComponent<Box>());
    }
}
