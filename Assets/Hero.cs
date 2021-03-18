using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hero : MonoBehaviour
{
    public ClassHeroy ClassHero;
    public int NumberOfHours;
    public int HeroID;
    public Image Icon;
    public Image Bar;
    public Text HoursText;
    public Text NameText;

    private float maxValue = 100;

    public void SetInfo(int hours, string name, Sprite icon, ClassHeroy classHeroy, int id, Color color)
    {
        NumberOfHours = hours;
        HoursText.text = NumberOfHours.ToString() + " Ч.";
        Bar.fillAmount = (NumberOfHours / maxValue);
        Bar.color = color;
        NameText.text = name;
        Icon.sprite = icon;
        ClassHero = classHeroy;
        HeroID = id;
    }

    public void SetMaxHours(int hours)
    {
        maxValue = hours;
        Bar.fillAmount = (NumberOfHours / maxValue);
    }
}
