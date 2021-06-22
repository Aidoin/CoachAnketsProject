using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;




public class MainWork : MonoBehaviour
{

    [SerializeField] private CardValue[] cards;

    public Card SelectedCard;
    private CardValue FunctioningCards;

    [Header("Основное")]
    public GameObject Settings; // Само окно настройки
    public PhotoCapture photoCapture;

    [Header("Список героев")]
    public Transform HeroesListSetting; // Список героев в настройках
    public GameObject HeroString; // Префаб эллемента для списка героев в карточке
    public HeroyList HeroyList;

    [Header("Роли")]
    public Toggle[] InputMainRol; // Тоглы выбора основной роли
    public InputField[] InputSrRolies; // Текстовые поля для ввода рейтинга на ролях
    public Sprite[] SpriteRanks; // Спрайты разных рангов

    [Header("Ники")]
    public InputField[] InputNames; // Поля для ввода миени и тегов

    [Header("Мейн Роль")]
    public Sprite[] MainHeroesSprites; // Иконки всех героев для мейн списка героев
    public GameObject MainHero; // Префаб эллемента для списка Мейн героев в карточке

    public List<GameObject> activeHeroesList = new List<GameObject>(); // Лист для сортировки отображжения героев в карточке
    public List<GameObject> mainHeroesList = new List<GameObject>(); // Лист для отображжения мейн героев в карточке
    private ClassHeroy mainClassHeroy; // Выбранный основной класс 

    private float timeEscape = 0;

    private void Start()
    {
        Screen.SetResolution(1920,1080,true);
        FunctioningCards = cards[(int)SelectedCard];

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].gameObject.SetActive(false);
            cards[i].SettingsLogo.gameObject.SetActive(false);
        }
        FunctioningCards.gameObject.SetActive(true);
        FunctioningCards.SettingsLogo.gameObject.SetActive(true);
    }


    void Update()
    {
        timeEscape += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) && timeEscape > 0.1f)
        {
            timeEscape = 0;
            ShowCard();
        }
    }


    public void ShowCard()
    {
        if (Settings.activeSelf == true)
        {
            Settings.SetActive(false);
        }
        else
        {
            UpdateAll();
            Settings.SetActive(true);
        }
    }


    public void UpdateAll()
    {
        UpdateRoles();
        UpdateHeader();
        UpdateListHero();
    }


    public void MakeScrenshot()
    {
        StartCoroutine(CreateScrenshot());
    }
    IEnumerator CreateScrenshot()
    {
        UpdateAll();
        yield return null;
        photoCapture.MakeScrenshot();
    }


    private void UpdateListHero()
    {
        // удаляем текущие элементы из списка героев в карточке 
        for (int i = 0; i < FunctioningCards.HeroesListWiew.childCount; i++)
        {
           Destroy(FunctioningCards.HeroesListWiew.GetChild(i).gameObject);
        }
        activeHeroesList.Clear(); // Отчищаем сам лист


        // Создаём элементы в списке героев в карточке 
        int maxHour = 0; // Переменная для сохранения максимального значения сыгранных часов
        for (int i = 0; i < HeroesListSetting.childCount; i++) // перебераем элементы в списке героев в настройках
        {
            int hour = HeroesListSetting.GetChild(i).GetComponent<HeroValues>().Hours; ; // Колличество часов текущего элемента
            
            // Если сыгранные часы присутствуют в текущем элементе, то создаём элемент этого героя в карточке
            if (hour > 0)
            {
                GameObject hero = Instantiate(HeroString, FunctioningCards.HeroesListWiew);
                Hero heroScript = hero.GetComponent<Hero>();

                // Присваиваем эллементу в карточке значения текущего элемента
                heroScript.SetInfo(hour, i, HeroyList.heroes[i]);

                hero.name = HeroyList.heroes[i].name;

                activeHeroesList.Add(hero);
            }

            if (hour > maxHour) maxHour = hour;
        }
        


        // Устанавливаем в карточке у всех эллементов максимальное ззначение для бара
        for (int i = 0; i < activeHeroesList.Count; i++)
        {
            activeHeroesList[i].GetComponent<Hero>().SetMaxHours(maxHour);
        }


        // Сортировка по убыванию часов(сортировка выбором) 
        // !!!!!! ИЗМЕНЯТЬ КОД ОСТОРОЖНО! МОЖНО ПОЙМАТЬ БЕСКОНЕЧНЫЙ ЦЫКЛ!!
        List<GameObject> listSort = new List<GameObject>();
        for (int i = 0; i < activeHeroesList.Count;) // Выполняется пока элементы присутствуют
        {
            int maxHourHeroID = 0;

            for (int j = 1; j < activeHeroesList.Count; j++)
            {
                if (activeHeroesList[j].GetComponent<Hero>().NumberOfHours > activeHeroesList[maxHourHeroID].GetComponent<Hero>().NumberOfHours)
                {
                    maxHourHeroID = j;
                }
            }
            listSort.Add(activeHeroesList[maxHourHeroID]);
            activeHeroesList.RemoveAt(maxHourHeroID);
        }

        // Устанавливаем отсортированую позицию
        for (int i = 0; i < listSort.Count; i++)
        {
            listSort[i].transform.SetSiblingIndex(i);
            activeHeroesList.Add(listSort[i]);
        }

        // Обновление мейн героев перед очисткой лишних из списка
        UpdateMainHero();

        // Очитска лишних элементов (всех после 7)
        for (int i = 7; i < activeHeroesList.Count; i++)
        {
            Destroy(activeHeroesList[i]);
        }
    }


    private void UpdateHeader()
    {
        if(InputNames[0].text != "")
            FunctioningCards.NickName.text = (InputNames[0].text).ToUpper();
        else
            FunctioningCards.NickName.text = "HIDDEN";

        if (InputNames[1].text != "")
            FunctioningCards.DiscordId.text = "<color=#" + FunctioningCards.ColorMain + ">   Discord Tag   </color> \n<i>  " + (InputNames[1].text).ToUpper() + "   </i>";
        else
            FunctioningCards.DiscordId.text = "<color=#" + FunctioningCards.ColorMain + ">   Discord Tag   </color> \n<i>  " + "  HIDDEN  " + "   </i>";

        if (InputNames[2].text != "")
            FunctioningCards.BattleTag.text = "<color=#" + FunctioningCards.ColorMain + ">   BattleTag   </color> \n<i>  " + (InputNames[2].text).ToUpper() + "   </i>";
        else
            FunctioningCards.BattleTag.text = "<color=#" + FunctioningCards.ColorMain + ">   BattleTag   </color> \n<i>  " + "  HIDDEN  " + "   </i>";
    }


    private void UpdateRoles()
    {
        for (int i = 0; i < 3; i++)
        {
            string srString = InputSrRolies[i].text;

            // Если ничего не введено то подрозумивается 0
            if (srString == "") srString = "0";

            int sr = System.Convert.ToInt32(srString);
            int spriteID = 0;

            // Переключаем выделяющую рамку на положение выбора основной роли
            FunctioningCards.Roles[i].GetChild(1).gameObject.SetActive(InputMainRol[i].isOn);

            // Вводим значение роли
            FunctioningCards.Roles[i].GetChild(3).gameObject.transform.GetChild(0).GetComponent<Text>().text = sr.ToString();

            // Изменение спрайта рейтинга в зависимости от ретинга (начало с 3 потому что минимальный спрайт длится до 1500)
            for (int l = 3; l < 11; l++)
            {
                if (sr < l * 500)
                {
                    spriteID = l - 3;
                    break;
                }
                else if(sr > 3999) // Если значение больше 4000 то ставится максимальный спрайт
                {
                    spriteID = 6;
                    break;
                }
            }

            // Установка выбранного спрайта
            FunctioningCards.Roles[i].GetChild(3).GetComponent<Image>().sprite = SpriteRanks[spriteID];
        }
    }


    private void UpdateMainHero()
    {
        // Очистка старых значений
        for (int i = 0; i < mainHeroesList.Count; i++)
        {
            Destroy(mainHeroesList[i]);
        }
        mainHeroesList.Clear();

        int countHero = 0; // Колличество Созданных карточек героев

        // Определение основного класса
        if (InputMainRol[0].isOn)
            mainClassHeroy = ClassHeroy.Tank;
        else if (InputMainRol[1].isOn)
            mainClassHeroy = ClassHeroy.Dps;
        else if (InputMainRol[2].isOn)
            mainClassHeroy = ClassHeroy.Heal;

        
        // Проверка класса героев
        for (int i = 0; i < activeHeroesList.Count; i++)
        {

            if (countHero == 3) // Если уже есть 3 карточки, останавливаем цыкл (UI Настроен максимально на 3 карточки)
                return;

            if (activeHeroesList[i].GetComponent<Hero>().ClassHero == mainClassHeroy)
            {
                countHero++;

                // Создаём карточку и настраиваем значения
                GameObject hero = Instantiate(MainHero, FunctioningCards.ContainerMainHeroes);
                mainHeroesList.Add(hero);

                HeroMainValue heroValue = hero.GetComponent<HeroMainValue>();

                Hero heroScript = activeHeroesList[i].GetComponent<Hero>();
                heroValue.Icon.sprite = MainHeroesSprites[heroScript.HeroID];
                heroValue.BarValue.fillAmount = heroScript.Bar.fillAmount;
                heroValue.BarValue.color = StringEx.ToColor(FunctioningCards.ColorMain);
                heroValue.Bar.color = StringEx.ToColor(FunctioningCards.ColorOff);
                heroValue.Hours.text = heroScript.HoursText.text;
                heroValue.name = heroScript.NameText.text;
            }
        }
    }


    public void ChangingAvatar(Sprite newAvatar)
    {
        FunctioningCards.ImageAvatar.sprite = newAvatar;
    }
}
