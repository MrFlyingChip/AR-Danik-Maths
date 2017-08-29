using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Quest1Controller : MonoBehaviour, CardsNumberController
{

    public string[] quests;

    public Image cardImage;

    public CardsNumberType[] types;
    public int currentType;
    public int currentQuestNumber = -1;
    public int currentCardNumber;
    public int previousCard;

    public Image[] resultImages;
    public Sprite correctSprite;
    public Sprite wrongSprite;

    public GameObject clock;

    public static bool isReady;

    public Sprite[] numberSprites;
    public Sprite[] textSprites;

    public Image numberImage;
    public Image textImage;

    public SoundManager sound;

    public GameObject ball;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Quest1"))
        {
            quests = PlayerPrefs.GetString("Quest1").Split('-');
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
                if (cardId != types[currentType].cardsID[currentCardNumber])
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
            previousCard = currentCardNumber;
            numberImage.gameObject.SetActive(false);
            textImage.gameObject.SetActive(false);
            isReady = true;
            currentQuestNumber++;
            cardImage.gameObject.SetActive(true);
            if (PlayerPrefs.HasKey("Quest1"))
            {
                currentCardNumber = int.Parse(quests[currentQuestNumber]) - 1;
            }
            else
            {
                while (currentCardNumber == previousCard)
                {
                    currentCardNumber = UnityEngine.Random.Range(0, types[currentType].typeSprites.Length);
                }
            }
            
            
            cardImage.sprite = types[currentType].typeSprites[currentCardNumber];
            clock.GetComponent<Animator>().SetBool("IsReady", isReady);
        }   
    }

    public void CorrectCard()
    {
        sound.BallWoopSound();
        isReady = false;
        clock.GetComponent<Animator>().SetBool("IsReady", isReady);
        ball.SetActive(true);
        ball.GetComponent<Animator>().SetBool("IsReady", isReady);
        ball.GetComponent<Animator>().SetBool("Correct", true);
        ball.GetComponent<Animator>().SetInteger("Number", currentCardNumber);
        resultImages[currentQuestNumber].sprite = correctSprite;
        ShowCorrectAnswer();
    }

    public void WrongCard()
    {
        isReady = false;
        sound.BallWoopSound();
        clock.GetComponent<Animator>().SetBool("IsReady", isReady);
        ball.SetActive(true);
        ball.GetComponent<Animator>().SetBool("IsReady", isReady);
        ball.GetComponent<Animator>().SetBool("Correct", false);
        ball.GetComponent<Animator>().SetInteger("Number", currentCardNumber);
        resultImages[currentQuestNumber].sprite = wrongSprite;
        ShowCorrectAnswer();
    }

    public void ShowCorrectAnswer()
    {
        numberImage.gameObject.SetActive(true);
        numberImage.sprite = numberSprites[currentCardNumber];
        textImage.gameObject.SetActive(true);
        textImage.sprite = textSprites[currentCardNumber];
        StartCoroutine(WaitBeforeGo());
    }

    public IEnumerator WaitBeforeGo()
    {
        yield return new WaitForSeconds(4);
        if (PlayerPrefs.GetString("Game1") != "Paid")
        {
            PlayerPrefs.SetString("Game1", "Played");
            SceneManager.LoadScene(0);
        }
    }

    public void ChangeCardType()
    {
        cardImage.sprite = types[currentType].typeSprites[currentCardNumber];
    }

}
