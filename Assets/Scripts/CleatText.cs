using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleatText : MonoBehaviour
{
    public List<InputField> Texts = new List<InputField>();

    public void Clear()
    {
        for (int i = 0; i < Texts.Count; i++)
        {
            Texts[i].text = "";
        }
    }

}
