using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject settings;
    public GameObject HeroesListSetting;
    public GameObject HeroesListWiew;

    public List<int> hoursHeroes = new List<int>();

    private void UpdateList()
    {
        int maxHour = 0;

        hoursHeroes.Clear();

        for (int i = 0; i < HeroesListSetting.transform.childCount; i++)
        {
            int hour;

            string str = HeroesListSetting.transform.GetChild(i).GetChild(3).GetComponent<InputField>().text;

            if (str != "") // Если значение присутствует
            {
                hour = System.Convert.ToInt32(str);
            }
            else // Если значения нет
            {
                hour = 0;
            }

            hoursHeroes.Add(hour); // Добовляем час в список
            if (hour > maxHour) maxHour = hour;
        }

        


    }


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settings.activeSelf == true)
            {



                UpdateList();

                settings.SetActive(false);
            }
            else
            {
                
                settings.SetActive(true);
            }
        }
    }
}
