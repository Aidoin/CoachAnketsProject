using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HeroValues : MonoBehaviour
{
    public Text HeroName;
    public InputField HeroHours;
    public ClassHeroy ClassHeroy;
    public Image Background;
    public Image Icon;

    public int Hours
    {
        get
        {
            if (HeroHours.text == "")
            {
                return 0;
            }
            else
            {
                return System.Convert.ToInt32(HeroHours.text);
            }
        }
        private set { }
    }
}