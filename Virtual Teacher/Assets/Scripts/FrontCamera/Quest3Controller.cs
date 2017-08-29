using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class Quest
{
    public int type;
    public bool isPlus;
    public int cardNumber1;
    public int cardNumber2;
    public int correctAnswer;
}

public class Quest3Controller : MonoBehaviour, CardsNumberController
{

    public string[] quests1;

    public Image cardImage1;
    public Image cardImage2;
    public Image cardImage3;
    public Image plus;

    public Sprite plusSprite;
    public Sprite minusSprite;

    public CardsNumberType[] types;
    public Quest[] quests;
    public int currentType;
    public int currentQuestNumber = -1;
    public int currentCardNumber1;
    public int currentCardNumber2;

    public Image[] resultImages;
    public Sprite correctSprite;
    public Sprite wrongSprite;

    public GameObject questionMark;

    public GameObject clock;

    public static bool isReady;

    public SoundManager sound;

    public int correctID;

    public RectTransform[] places;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Quest3"))
        {
            quests1 = PlayerPrefs.GetString("Quest3").Split('-');
        }
        GenerateNewQuest();
    }

    public int CurrentType
    {
        get
        {
            return currentType;
        }

        set
        {
            currentType = value;
        }
    }

    public bool IsReady
    {
        get
        {
            return isReady;
        }

        set
        {
            isReady = value;
        }
    }

    public int CurrentQuestNumber
    {
        get
        {
            return currentQuestNumber;
        }

        set
        {
            currentQuestNumber = value;
        }
    }

    public CardsNumberType[] Types
    {
        get
        {
            return types;
        }
    }

    public void RegisterCard(int cardId)
    {
        if (currentQuestNumber < 15)
        {
            if (isReady)
            {
                if (cardId != correctID)
                {
                    WrongCard();
                }
                else
                {
                    CorrectCard();
                }

            }
        }
    }

    public void GenerateNewQuest()
    {
        if (currentQuestNumber < 14)
        {
            isReady = true;
            currentQuestNumber++;
            cardImage1.gameObject.SetActive(true);
            cardImage2.gameObject.SetActive(true);
            if (PlayerPrefs.HasKey("Quest3"))
            {
                currentCardNumber1 = quests1[currentQuestNumber][0] - 49;
                currentCardNumber2 = quests1[currentQuestNumber][2] - 49;
                correctID = quests1[currentQuestNumber][4] - 49;
                plus.sprite = (quests1[currentQuestNumber][6] - 48 == 0) ? plusSprite : minusSprite;
                SetPlaces(quests1[currentQuestNumber][8] - 48);
            }
            else
            {
                currentCardNumber1 = quests[currentQuestNumber].cardNumber1;
                currentCardNumber2 = quests[currentQuestNumber].cardNumber2;
                correctID = quests[currentQuestNumber].correctAnswer;
                plus.sprite = (quests[currentQuestNumber].isPlus) ? plusSprite : minusSprite;
                SetPlaces(quests[currentQuestNumber].type);
            }
            
            cardImage1.sprite = types[currentType].typeSprites[currentCardNumber1];
            cardImage2.sprite = types[currentType].typeSprites[currentCardNumber2];
            clock.GetComponent<Animator>().SetBool("IsReady", isReady);
            questionMark.GetComponent<Animator>().SetBool("IsReady", isReady);      
        }
    }

    public void SetPlaces(int type)
    {
        Debug.Log(type);
        if (type == 1)
        {
            questionMark.GetComponent<RectTransform>().anchoredPosition = places[0].anchoredPosition;
            cardImage3.GetComponent<RectTransform>().anchoredPosition = places[0].anchoredPosition;
            cardImage1.GetComponent<RectTransform>().anchoredPosition = places[1].anchoredPosition;
            cardImage2.GetComponent<RectTransform>().anchoredPosition = places[2].anchoredPosition;
        }
        else if (type == 2)
        {
            questionMark.GetComponent<RectTransform>().anchoredPosition = places[1].anchoredPosition;
            cardImage3.GetComponent<RectTransform>().anchoredPosition = places[1].anchoredPosition;
            cardImage1.GetComponent<RectTransform>().anchoredPosition = places[0].anchoredPosition;
            cardImage2.GetComponent<RectTransform>().anchoredPosition = places[2].anchoredPosition;
        }
        else if (type == 3)
        {
            questionMark.GetComponent<RectTransform>().anchoredPosition = places[2].anchoredPosition;
            cardImage3.GetComponent<RectTransform>().anchoredPosition = places[2].anchoredPosition;
            cardImage1.GetComponent<RectTransform>().anchoredPosition = places[0].anchoredPosition;
            cardImage2.GetComponent<RectTransform>().anchoredPosition = places[1].anchoredPosition;
        }
        cardImage3.gameObject.SetActive(false);
    }

    public void CorrectCard()
    {
        isReady = false;
        clock.GetComponent<Animator>().SetBool("IsReady", isReady);
        resultImages[currentQuestNumber].sprite = correctSprite;
        sound.CorrectSound();
        ShowCorrectAnswer();
    }

    public void WrongCard()
    {
        isReady = false;
        clock.GetComponent<Animator>().SetBool("IsReady", isReady);
        resultImages[currentQuestNumber].sprite = wrongSprite;
        sound.WrongSound();
        ShowCorrectAnswer();
    }

    public void ShowCorrectAnswer()
    {
        questionMark.GetComponent<Animator>().SetBool("IsReady", isReady);
        questionMark.GetComponent<Image>().sprite = types[currentType].typeSprites[correctID];
        cardImage3.gameObject.SetActive(true);
        cardImage3.sprite = types[currentType].typeSprites[correctID];
        StartCoroutine(WaitBeforeGo());
    }

    public IEnumerator WaitBeforeGo()
    {
        yield return new WaitForSeconds(5);
        if (PlayerPrefs.GetString("Game1") != "Paid")
        {
            PlayerPrefs.SetString("Game1", "Played");
            SceneManager.LoadScene(0);
        }
    }

    public void ChangeCardType()
    {
        cardImage1.sprite = types[currentType].typeSprites[currentCardNumber1];
        cardImage2.sprite = types[currentType].typeSprites[currentCardNumber2];
        cardImage3.sprite = types[currentType].typeSprites[correctID];
    }

    public void PlayCorrectNumber()
    {
        sound.NumberSound(correctID + 1);
    }
}
