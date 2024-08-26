using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class ProgressBarWestern : MonoBehaviour
{

    public Color BarColor;   
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;
   
    private Image bar, barBackground;
    private float barValue;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            barValue = value;
            UpdateValue(barValue);

        }
    }

        

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        barBackground = GetComponent<Image>();
        barBackground = transform.Find("BarBackground").GetComponent<Image>();
    }

    private void Start()
    {

        bar.color = BarColor;
        barBackground.color = BarBackGroundColor; 
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(barValue);


    }

    void UpdateValue(float val)
    {
        bar.fillAmount = val / 100;
        bar.color = BarColor;

    }


    private void Update()
    {
        if (!Application.isPlaying)
        {           
            UpdateValue(50);

            bar.color = BarColor;
            barBackground.color = BarBackGroundColor;

            barBackground.sprite = BarBackGroundSprite;           
        }
    }

}
