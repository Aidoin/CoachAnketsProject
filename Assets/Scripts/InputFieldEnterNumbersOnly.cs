using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldEnterNumbersOnly : MonoBehaviour
{

    public int LimitingNumbers;
    public InputField text;

    public void ChangingTheField() // Отсекается ввод значений "-", ".", " " и ","
    {
        text.text = text.text.Trim('-', '.', ',', ' ');
        
        // Ограничивает длину строки
        if (LimitingNumbers != 0) // Если значение ограничения заданно
        {
            if (text.text.Length > LimitingNumbers)
            {
                text.text = text.text.Remove(LimitingNumbers); 
            }
        }
    }
}