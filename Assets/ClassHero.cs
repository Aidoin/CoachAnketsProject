using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassHero : MonoBehaviour
{
    public ClassHeroy classHeroy;
    public int NumberOfHours;
    public int Id;

    private Image bar;
    private Text text;
    private float maxValue = 100;

    void Start()
    {
        bar = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        text = transform.GetChild(2).GetChild(1).GetComponent<Text>();
    }

    public void SetHours(int hours)
    {
        NumberOfHours = hours;
        text.text = NumberOfHours.ToString() + " Ч.";
        bar.fillAmount = (NumberOfHours / maxValue);
    }

    public void SetMaxHours(int hours)
    {
        maxValue = hours;
        SetHours(NumberOfHours);
    }
}
