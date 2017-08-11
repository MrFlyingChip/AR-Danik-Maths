using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCameraController : MonoBehaviour
{
    public static int currentNumber;

    public int questType;

    public int type;

    public int firstNumber;
    public int secondNumber;
    public int correctNumber;

    public string primerStr;
    public string correctStr;

    public int umnoc;

    public BackCameraUI backCameraUI;


    public List<GameObject> images;
    public bool canRegister = false;
    public bool firstRegistered;
    public float timeToRegister;
    private float currentTimeToRegister;

    public SoundManager sound;

    private bool animated = true;
    public bool gameStarted;

    public bool ballGo;

    private void Start()
    {
        if (questType != 5)
        {
            GenerateQuest();
        }     
    }

    public void Update()
    {
        if (firstRegistered)
        {
            if (currentTimeToRegister > 0)
            {
                currentTimeToRegister -= Time.deltaTime;
            }
            else
            {
                canRegister = false;
            }
            if (!canRegister && !animated)
            {
                Visualize();
            }
        }
    }

    public void GenerateQuest()
    {
        ballGo = false;
        StopCoroutine(Wait());
        gameStarted = false;
        currentNumber = 0;
        animated = false;
        firstRegistered = false;
        currentTimeToRegister = timeToRegister;
        canRegister = true;
        images = new List<GameObject>();
        type = Random.Range(1, 4);
        switch (questType)
        {
            case 1:
                GeneratePlusQuest();
                break;
            case 2:
                GenerateMinusQuest();
                break;
            case 3:
                GenerateMultiQuest();
                break;
            case 4:
                GenerateDivideQuest();
                break;
            case 5:
                GenerateTableQuest();
                break;
              case 6:
                GenerateColorQuest();
                break;
        }
        backCameraUI.Display(primerStr);
    }

    public void GeneratePlusQuest()
    {
        switch (type)
        {
            case 1:
                firstNumber = Random.Range(1, 10);
                correctNumber = Random.Range(1, 10);
                secondNumber = correctNumber + firstNumber;
                primerStr = "?+" + firstNumber + "=" + secondNumber;
                correctStr = correctNumber + "+" + firstNumber + "=" + secondNumber;
                break;
            case 2:
                firstNumber = Random.Range(1, 10);
                correctNumber = Random.Range(1, 10);
                secondNumber = correctNumber + firstNumber;
                primerStr = firstNumber + "+?" + "=" + secondNumber;
                correctStr = firstNumber + "+" + correctNumber + "=" + secondNumber;
                break;
            case 3:
                firstNumber = Random.Range(1, 10);
                secondNumber = Random.Range(2, 10);
                correctNumber = secondNumber + firstNumber;
                if (correctNumber == 10)
                {
                    correctNumber--;
                    secondNumber--;
                }
                primerStr = firstNumber + "+" + secondNumber + "=" + "?";
                correctStr = firstNumber + "+" + secondNumber + "=" + correctNumber;
                break;
        }
    }

    public void GenerateMinusQuest()
    {
        switch (type)
        {
            case 1:
                firstNumber = Random.Range(1, 10);
                secondNumber = Random.Range(2, 10);
                correctNumber = secondNumber + firstNumber;
                if (correctNumber == 10)
                {
                    correctNumber--;
                    secondNumber--;
                }
                primerStr = "?-" + firstNumber + "=" + secondNumber;
                correctStr = correctNumber + "-" + firstNumber + "=" + secondNumber;
                break;
            case 2:
                secondNumber = Random.Range(1, 10);
                correctNumber = Random.Range(1, 10);
                firstNumber = correctNumber + secondNumber;
                primerStr = firstNumber + "-?" + "=" + secondNumber;
                correctStr = firstNumber + "-" + correctNumber + "=" + secondNumber;
                break;
            case 3:
                secondNumber = Random.Range(1, 10);
                correctNumber = Random.Range(1, 10);
                firstNumber = correctNumber + secondNumber;
                primerStr = firstNumber + "-" + secondNumber + "=" + "?";
                correctStr = firstNumber + "-" + secondNumber + "=" + correctNumber;
                break;
        }
    }

    public void GenerateMultiQuest()
    {
        switch (type)
        {
            case 1:
                firstNumber = Random.Range(1, 10);
                correctNumber = Random.Range(1, 10);
                secondNumber = correctNumber * firstNumber;
                primerStr = "?*" + firstNumber + "=" + secondNumber;
                correctStr = correctNumber + "*" + firstNumber + "=" + secondNumber;
                break;
            case 2:
                firstNumber = Random.Range(1, 10);
                correctNumber = Random.Range(1, 10);
                secondNumber = correctNumber * firstNumber;
                primerStr = firstNumber + "*?" + "=" + secondNumber;
                correctStr = firstNumber + "*" + correctNumber + "=" + secondNumber;
                break;
            case 3:
                firstNumber = Random.Range(2, 10);
                secondNumber = Random.Range(1, 10);
                correctNumber = secondNumber * firstNumber;
                if (correctNumber % 10 == 0)
                {
                    firstNumber--;
                    correctNumber = secondNumber * firstNumber;
                }
                primerStr = firstNumber + "*" + secondNumber + "=" + "?";
                correctStr = firstNumber + "*" + secondNumber + "=" + correctNumber;
                break;
        }
    }

    public void GenerateTableQuest()
    {
        switch (type)
        {
            case 1:
                firstNumber = umnoc;
                correctNumber = Random.Range(1, 10);
                secondNumber = correctNumber * firstNumber;
                primerStr = "?*" + firstNumber + "=" + secondNumber;
                correctStr = correctNumber + "*" + firstNumber + "=" + secondNumber;
                break;
            case 2:
                firstNumber = umnoc;
                correctNumber = Random.Range(1, 10);
                secondNumber = correctNumber * firstNumber;
                primerStr = firstNumber + "*?" + "=" + secondNumber;
                correctStr = firstNumber + "*" + correctNumber + "=" + secondNumber;
                break;
            case 3:
                firstNumber = umnoc;
                secondNumber = Random.Range(2, 10);
                correctNumber = secondNumber * firstNumber;
                if (correctNumber % 10 == 0)
                {
                    secondNumber--;
                    correctNumber = secondNumber * firstNumber;
                }
                primerStr = firstNumber + "*" + secondNumber + "=" + "?";
                correctStr = firstNumber + "*" + secondNumber + "=" + correctNumber;
                break;
        }
    }

    public void GenerateDivideQuest()
    {
        switch (type)
        {
            case 1:
                firstNumber = Random.Range(2, 10);
                secondNumber = Random.Range(1, 10);
                correctNumber = secondNumber * firstNumber;
                if (correctNumber % 10 == 0)
                {
                    firstNumber--;
                    correctNumber = secondNumber * firstNumber;
                }
                primerStr = "?/" + firstNumber + "=" + secondNumber;
                correctStr = correctNumber + "/" + firstNumber + "=" + secondNumber;
                break;
            case 2:
                secondNumber = Random.Range(1, 10);
                correctNumber = Random.Range(1, 10);
                firstNumber = correctNumber * secondNumber;
                primerStr = firstNumber + "/?" + "=" + secondNumber;
                correctStr = firstNumber + "/" + correctNumber + "=" + secondNumber;
                break;
            case 3:
                secondNumber = Random.Range(1, 10);
                correctNumber = Random.Range(1, 10);
                firstNumber = correctNumber * secondNumber;
                primerStr = firstNumber + "/" + secondNumber + "=" + "?";
                correctStr = firstNumber + "/" + secondNumber + "=" + correctNumber;
                break;
        }
    }
    public int number = 0;
    public int c = 0;
    public void GenerateColorQuest()
    {
        
        
        switch (type)
        {
            case 1:
                //correctNumber = Random.Range(21, 31);
                //sound.GetComponent<AudioSource>().clip = sound.languageSounds.colors[correctNumber - 20];
                //sound.GetComponent<AudioSource>().Play();
                //sound.GetComponent<AudioSource>().PlayDelayed(2f);
                //primerStr = "";
                number = Random.Range(31, 36);
                c = Random.Range(1, 10);
                correctNumber = number + 10 * c;
                primerStr = c.ToString();
                correctStr = c.ToString();
                backCameraUI.number = number - 30;
                break;
            case 2:
                number = Random.Range(31, 36);
                c = Random.Range(1, 9);
                correctNumber = number + 10 * c;
                primerStr = c.ToString();
                correctStr = c.ToString();
                backCameraUI.number = number - 30;
                break;
            case 3:
                number = Random.Range(31, 36);
                c = Random.Range(1, 10);
                correctNumber = number + 10 * c;
                primerStr = c.ToString();
                correctStr = c.ToString();
                backCameraUI.number = number - 30;
                break;
        }
    }


    public void RegisterID(int id, GameObject go)
    {
        if (canRegister)
        {
            if (!firstRegistered)
            {
                firstRegistered = true;
            }
            images.Add(go);
        }
    }

    public void Visualize()
    {
        animated = true;
        if (images.Count == 1)
        {
            ProceedOneCard();
        }
        else if (images.Count == 2)
        {
            ProceedTwoCards();
        }
        else
        {
            sound.WrongSound();
            GenerateQuest();
        }
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(20);
        if (!canRegister && animated && !ballGo)
        {
            sound.WrongSound();
            GenerateQuest();
        }
    }

    public IEnumerator WaitForNewQuest(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //sound.WrongSound();
        if (images.Count == 1)
        {
            images[0].GetComponent<Vuforia.BackCameraTarget>().child.SetActive(false);
        }
        else if (images.Count == 2)
        {
            images[0].GetComponent<Vuforia.BackCameraTarget>().child.SetActive(false);
            images[1].GetComponent<Vuforia.BackCameraTarget>().child.SetActive(false);
        }
        GenerateQuest();
       
    }

    public void ProceedOneCard()
    {
        ballGo = true;
        backCameraUI.Display(correctStr);
        images[0].GetComponent<Vuforia.BackCameraTarget>().child.SetActive(true);
        if (images[0].GetComponent<Vuforia.BackCameraTarget>().cardID == correctNumber)
        {
            StartCoroutine(WaitForNewQuest(8));
            if (images[0].GetComponentInChildren<Animator>() != null) images[0].GetComponentInChildren<Animator>().SetBool("Correct", true);
        }
        else
        {
            StartCoroutine(WaitForNewQuest(9));
            if (images[0].GetComponentInChildren<Animator>() != null) images[0].GetComponentInChildren<Animator>().SetBool("Correct", false);
        }
        if (images[0].GetComponent<Vuforia.BackCameraTarget>().cardID >= 10)
        {
            sound.WrongSound();
            GenerateQuest();
        }
    }

    public void ProceedTwoCards()
    {
        ballGo = true;
        images.Sort(delegate (GameObject x, GameObject y)
        {
            if (x == null && y == null) return 0;
            else return x.transform.position.x.CompareTo(y.transform.position.x);
        });
        backCameraUI.Display(correctStr);
        int cardNumber = images[0].GetComponent<Vuforia.BackCameraTarget>().cardID * 10 + images[1].GetComponent<Vuforia.BackCameraTarget>().cardID;
        images[0].GetComponent<Vuforia.BackCameraTarget>().child.SetActive(true);
        images[1].GetComponent<Vuforia.BackCameraTarget>().child.SetActive(true);
        if (cardNumber == correctNumber)
        {
            if(questType != 6) StartCoroutine(WaitForNewQuest(8));
            if (images[0].GetComponentInChildren<Animator>() != null) images[0].GetComponentInChildren<Animator>().SetBool("Correct", true);
            if (images[1].GetComponentInChildren<Animator>() != null) images[1].GetComponentInChildren<Animator>().SetBool("Correct", true);
        }
        else
        {
            StartCoroutine(WaitForNewQuest(9));
            if (images[0].GetComponentInChildren<Animator>() != null) images[0].GetComponentInChildren<Animator>().SetBool("Correct", false);
            if (images[1].GetComponentInChildren<Animator>() != null) images[1].GetComponentInChildren<Animator>().SetBool("Correct", false);
        }
        if (images[0].GetComponent<Vuforia.BackCameraTarget>().cardID > 11 && images[1].GetComponent<Vuforia.BackCameraTarget>().cardID > 11)
        {
            GenerateQuest();
        }

    }

    private void Correct()
    {
        GetComponent<AudioSource>().Play();
        GenerateQuest();
    }

    private void Wrong()
    {
        Handheld.Vibrate();
        GenerateQuest();
    }

    public void StartGame()
    {
        gameStarted = true;
        for (int i = 0; i < c; i++)
        {
            images[1].GetComponent<Vuforia.BackCameraTarget>().children[i].SetActive(true);
            images[1].GetComponent<Vuforia.BackCameraTarget>().children[i].GetComponent<Animation>().Play("Walk");
        }
       
    }

   
}
