using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //[SerializeField] private Button spinButton;
    [SerializeField] private List<GameObject> prizeList;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform wheel;
    private UnityEvent spinButtonClicked;
    private int index;
    
    // Start is called before the first frame update
    void Start()
    {
        //spinButtonClicked = new UnityEvent();
        //spinButton.onClick.AddListener(OnSpinButtonClicked);
        CreateSlices();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSpinButtonClicked()
    {
        
    }

    void CreateSlices()
    {
        index = 0;
        foreach (Transform child in wheel)
        {
            Transform sliceTransform = child.gameObject.transform;
            Instantiate(prizeList[index], sliceTransform);
            index++;
        }
    }
    
}
