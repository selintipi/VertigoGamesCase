using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Wheel/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Image image;
    [SerializeField] private int count;
    public int ItemCount => count;
}
