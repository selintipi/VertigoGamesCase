using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliceController : MonoBehaviour
{
    
    [SerializeField] private Item item;
    [SerializeField] private TextMeshProUGUI ui_text_count;
    
    // Start is called before the first frame update
    void Start()
    {
        if(item.name!="Bomb") ui_text_count.text = $"x{item.count}";
    }

    private void OnEnable()
    {
        item.Updated.AddListener(UpdateUI);
    }

    private void UpdateUI()
    {
        ui_text_count.text = $"x{item.count}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
