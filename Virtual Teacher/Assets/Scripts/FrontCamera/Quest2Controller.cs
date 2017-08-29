using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quest2Controller : MonoBehaviour, CardsNumberController
{

    public string[] quests;

    public Image cardImage1;
    public Image cardImage2;

    public CardsNumberType[] types;
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

    public void Start()
    {
        if (PlayerPrefs.HasKey("Quest2"))
        {
            quests = PlayerPrefs.GetString("Quest2").Split('-');
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
            if (PlayerPrefs.HasKey("Quest2"))
            {
                currentCardNumber1 = quests[currentQuestNumber][0] - 49;
                currentCardNumber2 = quests[currentQuestNumber][2] - 49;
            }
            else
            {
                currentCardNumber1 = Random.Range(0, types[currentType].typeSprites.Length);
                currentCardNumber2 = Random.Range(0, types[currentType].typeSprites.Length);
            }      
            cardImage1.sprite = types[currentType].typeSprites[currentCardNumber1];
            cardImage2.sprite = types[currentType].typeSprites[currentCardNumber2];
            clock.GetComponent<Animator>().SetBool("IsReady", isReady);
            questionMark.GetComponent<Animator>().SetBool("IsReady", isReady);
            if (currentCardNumber1 > currentCardNumber2) correctID = 5;
            else if (currentCardNumber1 < currentCardNumber2) correctID = 6;
            else if (currentCardNumber1 == currentCardNumber2) correctID = 0;
        }
    }

    public void CorrectCard()
    {
        isReady = false;
        clock.GetComponent<Animator>().SetBool("IsReady", isReady);
        sound.CorrectSound(); 
        resultImages[currentQuestNumber].sprite = correctSprite;
        ShowCorrectAnswer();
    }

    public void WrongCard()
    {
        isReady = false;
        clock.GetComponent<Animator>().SetBool("IsReady", isReady);
        sound.WrongSound();
        resultImages[currentQuestNumber].sprite = wrongSprite;
        ShowCorrectAnswer();
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

    public void ShowCorrectAnswer()
    {
        questionMark.GetComponent<Animator>().SetBool("IsReady", isReady);
        questionMark.GetComponent<Animator>().SetInteger("ID", correctID);
        StartCoroutine(WaitBeforeGo());
    }

    public void ChangeCardType()
    {
        cardImage1.sprite = types[currentType].typeSprites[currentCardNumber1];
        cardImage2.sprite = types[currentType].typeSprites[currentCardNumber2];
    }

    public void PlayCorrentZnak()
    {
        sound.ZnakSound(correctID + 1);
    }
}
