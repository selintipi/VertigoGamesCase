using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Wheel/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public int count;
    private UnityEvent updated;
    public UnityEvent Updated => updated;

    private void OnEnable()
    {
        updated ??= new UnityEvent();
    }

    private void OnValidate()
    {
        updated.Invoke();
    }
}
