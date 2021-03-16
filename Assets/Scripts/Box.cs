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
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetType(BoxType boxType, Color color)
    {
        TypeOfBox = boxType;
        _spriteRenderer.color = color;
    }
}
