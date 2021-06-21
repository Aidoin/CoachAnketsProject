using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingHeroes : MonoBehaviour
{
    [SerializeField] private Transform containerForHeroes;


    public void ShowRoles(int indexRole)
    {
        ClassHeroy role = (ClassHeroy)indexRole;
        Debug.Log(role);
        // Скрыть всех героев
        for (int i = containerForHeroes.childCount - 1; i > -1; i--)
        {
            containerForHeroes.GetChild(i).gameObject.SetActive(false);
        }

        //Показать выбранную роль
        for (int i = 0; i < containerForHeroes.childCount; i++)
        {
            HeroValues ThisHero = containerForHeroes.GetChild(i).GetComponent<HeroValues>();

            if (role == ClassHeroy.None || ThisHero.ClassHeroy == role)
            {
                ThisHero.gameObject.SetActive(true);
            }
        }
    }
}