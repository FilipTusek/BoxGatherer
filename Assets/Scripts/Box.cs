using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetType(BoxType boxType, Color color)
    {
        gameObject.layer = LayerMask.NameToLayer("Box");
        TypeOfBox = boxType;
        _spriteRenderer.color = color;
    }
}
