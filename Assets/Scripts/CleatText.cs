using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleatText : MonoBehaviour
{
    public InputField[] Text;

    public void Clear()
    {
        for (int i = 0; i < Text.Length; i++)
        {
            Text[i].text = "";
        }
    }

}
