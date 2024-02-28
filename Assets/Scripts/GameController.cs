using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private List<GameObject> prizeList;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform wheel;
    
    public List<AnimationCurve> animationCurves;
    private UnityEvent spinButtonClicked;
    private int index;
    private bool spinning;    
    private float anglePerItem;    
    private int randomTime;
    private int itemNumber;
    private List<GameObject> wonPrizes;
    
    // Start is called before the first frame update
    void Start()
    {
        wonPrizes = new List<GameObject>();
        spinning = false;
        anglePerItem = 360 / prizeList.Count;
        spinButton.onClick.AddListener(OnSpinButtonClicked);
        CreateSlices();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSpinButtonClicked()
    {
        if(spinning) return;
        randomTime = Random.Range (1, 4);
        itemNumber = Random.Range (0, prizeList.Count);
        float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);
        StartCoroutine (SpinTheWheel (5 * randomTime, maxAngle));
    }
    
    IEnumerator SpinTheWheel (float time, float maxAngle)
    {
        spinning = true;
        
        float timer = 0.0f;        
        float startAngle = wheel.eulerAngles.z;        
        maxAngle = maxAngle - startAngle;
        
        int animationCurveNumber = Random.Range (0, animationCurves.Count);
        Debug.Log ("Animation Curve No. : " + animationCurveNumber);
        
        while (timer < time) {
            //to calculate rotation
            float angle = maxAngle * animationCurves [animationCurveNumber].Evaluate (timer / time) ;
            wheel.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }
        
        wheel.eulerAngles = new Vector3 (0.0f, 0.0f, maxAngle + startAngle);
        spinning = false;
            
        Debug.Log ("Prize: " + prizeList[itemNumber]);
        wonPrizes.Add(prizeList[itemNumber]);
        Debug.Log("Won Prizes: " );
        for (int i = 0; i < wonPrizes.Count; i++)
        {
            Debug.Log(wonPrizes[i] + " ");
        }
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
