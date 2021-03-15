using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Основное")]
    public GameObject settings;

    [Header("Список героев")]
    public GameObject HeroesListSetting;
    public GameObject HeroesListWiew;
    public List<GameObject> activeHeroesList = new List<GameObject>();

    [Header("Роли")]
    public Toggle[] InputMainRol;
    public InputField[] InputSrRolies;
    public Transform[] DsplayRol;
    public Sprite[] SpriteRol;

    [Header("Ники")]
    public InputField[] InputNames;
    public Text[] DsplayNames;

    private void UpdateListHero()
    {
        int maxHour = 0;

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

    public void UpdateHeader()
    {
        if(InputNames[0].text != "")
            DsplayNames[0].text = (InputNames[0].text).ToUpper();
        else
            DsplayNames[0].text = "HIDDEN";

        if (InputNames[1].text != "")
            DsplayNames[1].text = ("Diskord Tag: " + InputNames[1].text).ToUpper();
        else
            DsplayNames[1].text = "";

        if (InputNames[2].text != "")
            DsplayNames[2].text = ("Battlenet Tag: " + InputNames[2].text).ToUpper();
        else
            DsplayNames[2].text = "";
    }

    public void UpdateRoles()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Основной цыкл, проход " + i);
            string srString = InputSrRolies[i].text;
            if (srString == "") srString = "0";
            int sr = System.Convert.ToInt32(srString);
            int spriteID = 0;

            Debug.Log(InputSrRolies[i]);

            DsplayRol[i].GetChild(1).gameObject.SetActive(InputSrRolies[i]);


            DsplayRol[i].GetChild(3).gameObject.transform.GetChild(0).GetComponent<Text>().text = sr.ToString();


            for (int l = 3; l < 11; l++)
            {
                //Debug.Log("Проврка рейтинга, проход " + l);
                if (sr < l * 500)
                {
                    spriteID = l - 3;
                    //Debug.Log("Установка прайта, sr(" + sr + ") мньше " + l * 500);
                    break;
                }
                else if(sr > 3999)
                {
                    spriteID = 6;
                    break;
                }
            }

            DsplayRol[i].GetChild(3).GetComponent<Image>().sprite = SpriteRol[spriteID];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settings.activeSelf == true)
            {
                UpdateRoles();
                UpdateHeader();
                UpdateListHero();
                settings.SetActive(false);
            }
            else
            {
                settings.SetActive(true);
            }
        }
    }
}
