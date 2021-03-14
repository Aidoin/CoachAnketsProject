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

    public List<GameObject> activeHeroesList = new List<GameObject>();

    private void UpdateList()
    {
        int maxHour = 0;

        hoursHeroes.Clear();
        activeHeroesList.Clear();

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

            if (hour != 0)
            {
                HeroesListWiew.transform.GetChild(i).gameObject.SetActive(true);
                HeroesListWiew.transform.GetChild(i).GetComponent<ClassHero>().SetHours(hour);
                activeHeroesList.Add(HeroesListWiew.transform.GetChild(i).gameObject);
            }
            else
            {
                HeroesListWiew.transform.GetChild(i).gameObject.SetActive(false);
            }

            hoursHeroes.Add(hour); // Добовляем час в список
            if (hour > maxHour) maxHour = hour;
        }


        for (int i = 0; i < activeHeroesList.Count; i++)
        {
            activeHeroesList[i].GetComponent<ClassHero>().SetMaxHours(maxHour);
        }
        


        for (int i = 0; i < activeHeroesList.Count; i++)
        {
            if (activeHeroesList.Count - i - 1 != 0)
            {
                int down = activeHeroesList[activeHeroesList.Count - i - 1].GetComponent<ClassHero>().NumberOfHours;
                int up = activeHeroesList[activeHeroesList.Count - i - 2].GetComponent<ClassHero>().NumberOfHours;
                
                if(down < up)
                {
                    Debug.Log(activeHeroesList[activeHeroesList.Count - i - 1].transform.GetSiblingIndex());
                }
            }
        }




        //if(activeHeroesList.Count > 7)
        //{
        //    for (int i = 7; i < activeHeroesList.Count; i++)
        //    {
        //        Debug.Log("убираем "+ i + " эллемент");
        //        activeHeroesList[i].SetActive(false);
        //        activeHeroesList.RemoveAt(i);
        //    }
        //}


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
