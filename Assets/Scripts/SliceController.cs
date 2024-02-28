using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliceController : MonoBehaviour
{
    [SerializeField] private Item item;

    [SerializeField] private TextMeshProUGUI countText;
    // Start is called before the first frame update
    void Start()
    {
        //countText.text = item.ItemCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
