using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMainValue : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image bar;
    [SerializeField] private Image barValue;
    [SerializeField] private Text hours;

    public Image Icon => icon;
    public Image Bar => bar;
    public Image BarValue => barValue;
    public Text Hours => hours;
}
