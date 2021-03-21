using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Events;

public class Box : MonoBehaviour
{
    public enum BoxType
    {
        Red,
        Blue
    }
    
    public BoxType TypeOfBox { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    
    private SpriteRenderer _spriteRenderer;
    private bool _hasLanded = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InvokeRepeating("CheckIfLanded", 0.2f, 0.1f);
    }

    private void OnDisable()
    {
        _hasLanded = false;
    }

    private void CheckIfLanded()
    {
        if (_hasLanded) return;
        if (Rigidbody.velocity.magnitude < 0.1f) {
            _hasLanded = true;
            EventManager.OnBoxLanded.OnEventRaised?.Invoke();
            CancelInvoke("CheckIfLanded");
        }
    }
    
    public void SetType(BoxType boxType, Color color)
    {
        gameObject.layer = LayerMask.NameToLayer("Box");
        TypeOfBox = boxType;
        _spriteRenderer.color = color;
    }
}
