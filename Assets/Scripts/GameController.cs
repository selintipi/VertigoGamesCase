using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button ui_button_spin;
    [SerializeField] private List<GameObject> prizeList;
    [SerializeField] private GameObject prefab_bomb;
    [SerializeField] private Transform ui_transform_wheel;
    [SerializeField] private Image ui_image_wheel;
    [SerializeField] private Sprite ui_sprite_wheel_gold;
    [SerializeField] private Sprite ui_sprite_wheel_silver;
    [SerializeField] private Image ui_image_arrow;
    [SerializeField] private Sprite ui_sprite_arrow_gold;
    [SerializeField] private Sprite ui_sprite_arrow_silver;
    [SerializeField] private Button ui_button_leave;
    [SerializeField] private Sprite ui_sprite_wheel_bronze;
    [SerializeField] private Sprite ui_sprite_arrow_bronze;
    
    public List<AnimationCurve> animationCurves;
    private UnityEvent spinButtonClicked;
    private int index;
    private bool spinning;    
    private float anglePerItem;    
    private int randomTime;
    private int itemNumber;
    private int spinCount = 0;
    private List<GameObject> wonPrizes;
    private List<GameObject> slices;
    
    // Start is called before the first frame update
    void Start()
    {
        wonPrizes = new List<GameObject>();
        spinning = false;
        ui_button_spin.onClick.AddListener(OnSpinButtonClicked);
        ui_button_leave.onClick.AddListener(OnLeaveButtonClicked);
        CreateSlices();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLeaveButtonClicked()
    {
        if (!spinning)
        {
            Application.Quit();
            Debug.Log("Quit Application");
        } 
    }

    private void OnSpinButtonClicked()
    {
        if(spinning) return;
        anglePerItem = 360 / slices.Count;
        randomTime = Random.Range (1, 4);
        itemNumber = Random.Range (0, slices.Count);
        spinCount++;
        float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);
        StartCoroutine (SpinTheWheel (5 * randomTime, maxAngle));
    }
    
    IEnumerator SpinTheWheel (float time, float maxAngle)
    {
        spinning = true;
        
        float timer = 0.0f;        
        float startAngle = ui_transform_wheel.eulerAngles.z;        
        maxAngle = maxAngle - startAngle;
        
        int animationCurveNumber = Random.Range (0, animationCurves.Count);
        
        while (timer < time) {
            //to calculate rotation
            float angle = maxAngle * animationCurves [animationCurveNumber].Evaluate (timer / time) ;
            ui_transform_wheel.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }
        
        ui_transform_wheel.eulerAngles = new Vector3 (0.0f, 0.0f, maxAngle + startAngle);
        spinning = false;
        CalculatePrize();
        SetUI();
    }

    private void CalculatePrize()
    {
        if (slices[itemNumber].name == "Bomb")
        {
            spinCount = 0;
            wonPrizes.Clear();
            Application.Quit();
        }
        else
        {
            wonPrizes.Add(slices[itemNumber]);
        }
        Debug.Log("Won Prizes: " );
        for (int i = 0; i < wonPrizes.Count; i++)
        {
            Debug.Log(wonPrizes[i] + " ");
        }
    }

    private void SetUI()
    {
        foreach (Transform child in ui_transform_wheel)
        {
            Destroy(child.GetChild(0).gameObject);
            slices.Clear();
        }
        if (spinCount % 30 == 0)
        {
            ui_image_wheel.sprite = ui_sprite_wheel_gold;
            ui_image_arrow.sprite = ui_sprite_arrow_gold;
            CreateSlicesForSpecialZone();
        }
        else if(spinCount % 5 == 0)
        {
            ui_image_wheel.sprite = ui_sprite_wheel_silver;
            ui_image_arrow.sprite = ui_sprite_arrow_silver;
            CreateSlicesForSpecialZone();
        }
        else
        {
            ui_image_wheel.sprite = ui_sprite_wheel_bronze;
            ui_image_arrow.sprite = ui_sprite_arrow_bronze;
            CreateSlices();
        }
    }

    private void CreateSlices()
    {
        int sliceCount = ui_transform_wheel.childCount;
        slices = new List<GameObject>();
        Instantiate(prefab_bomb, ui_transform_wheel.GetChild(0));
        slices.Add(prefab_bomb);
        for (int sliceIndex = 1; sliceIndex < sliceCount; sliceIndex++)
        {
            index = Random.Range (0, prizeList.Count);
            Transform sliceTransform = ui_transform_wheel.GetChild(sliceIndex).gameObject.transform;
            Instantiate(prizeList[index], sliceTransform);
            slices.Add(prizeList[index]);
        }
    }

    private void CreateSlicesForSpecialZone()
    {
        foreach (Transform child in ui_transform_wheel)
        {
            index = Random.Range (0, prizeList.Count);
            Transform sliceTransform = child.gameObject.transform;
            Instantiate(prizeList[index], sliceTransform);
            slices.Add(prizeList[index]);
        }
    }
    
}
