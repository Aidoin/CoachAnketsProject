#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class FillingCellsHeroesInSettings : MonoBehaviour
{
    [SerializeField] private Transform containerForHeroes;
    [SerializeField] private HeroyList heroyLis;
    [SerializeField] private GameObject heroCellSetValue;
    [SerializeField] private Sprite[] spriteHeroes;
    [SerializeField] private string tankColor;
    [SerializeField] private string dPSColor;
    [SerializeField] private string healColor;

    [SerializeField] private bool Run = false;

    private void Update()
    {
        // Запустить пересоздание?
        if (Run)
        {
            Run = false;
            if (containerForHeroes.childCount == 0) // Если контейнер пуст
            {
                CreateHeroes();
            }
            else
            {
                for (int i = containerForHeroes.childCount - 1; i > -1; i--)
                {
                    DestroyImmediate(containerForHeroes.GetChild(i).gameObject);

                    if (containerForHeroes.childCount == 0) // Если контейнер уже пуст
                        CreateHeroes();
                }
            }
        }
    }

    private void CreateHeroes()
    {
        for (int i = 0; i < heroyLis.heroes.Length; i++)
        {
            //HeroValues 
            HeroValues ThisHero = Instantiate(heroCellSetValue, containerForHeroes).GetComponent<HeroValues>();

            // Цвет
            if (heroyLis.heroes[i].classHeroy == ClassHeroy.Dps)
            {
                ThisHero.Background.color = StringEx.ToColor(dPSColor);
            }
            else if (heroyLis.heroes[i].classHeroy == ClassHeroy.Heal)
            {
                ThisHero.Background.color = StringEx.ToColor("01A34B");
            }
            else if (heroyLis.heroes[i].classHeroy == ClassHeroy.Tank)
            {
                ThisHero.Background.color = StringEx.ToColor(tankColor);
            }

            // Класс
            ThisHero.ClassHeroy = heroyLis.heroes[i].classHeroy;

            // Имя
            ThisHero.HeroName.text = heroyLis.heroes[i].name;

            // Иконка
            ThisHero.Icon.sprite = spriteHeroes[i];

            FindObjectOfType<CleatText>().Texts.Add(ThisHero.HeroHours);
        }
    }
}
#endif