using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenario3Mother : MonoBehaviour {
    public Text text;
    public Image[] numberImage;
    public Sprite[] numberSprites;
    public Image znakImage;
    public Sprite[] znakSprites;
    private int[] currentNumberSprite = new int[3] { 10, 10, 10};
    private List<string> numbers = new List<string>();
    private List<string> numbersFotText = new List<string>();
    private int[] currentNumbers = new int[3];
    private int plus;
    private int type;

    public int correct;

    public Animator saveButton;
    private int[] numbersToWrite = new int[2];

    private bool correctPrim = true;

    public void ChangeNumberSprite(int imageNumber)
    {
        if (currentNumberSprite[imageNumber] < numberSprites.Length - 1)
        {
            currentNumberSprite[imageNumber]++;
        }
        else
        {
            currentNumberSprite[imageNumber] = 0;
        }
        numberImage[imageNumber].sprite = numberSprites[currentNumberSprite[imageNumber]];
    }

    public void SetZnakImage()
    {
        if (plus == 0)
        {
            plus = 1;
        }
        else
        {
            plus = 0;
        }
        znakImage.sprite = znakSprites[plus];
    }

    public string SetZnak()
    {
        string znak = "";
        if (plus == 0) znak = "+";
        else if (plus == 1) znak = "-";
        return znak;
    }

    public void AcceptNumber()
    {
        currentNumbers[0] = currentNumberSprite[0] + 1;
        currentNumbers[1] = currentNumberSprite[1] + 1;
        currentNumbers[2] = currentNumberSprite[2] + 1;
        if (OneQuestionMark())
        {
            if (numbers.Count < 15)
            {
                SetType();
                Type();
                if (correctPrim)
                {
                    numbers.Add(numbersToWrite[0] + "," + numbersToWrite[1] + "," + correct + "," + plus + "," + type);
                    string numberStr = numbersFotText[0].ToString();
                    for (int i = 1; i < numbersFotText.Count; i++)
                    {
                        numberStr += "\n" + numbersFotText[i];
                    }
                    text.text = numberStr;
                    if (numbers.Count == 15)
                    {
                        saveButton.SetBool("Ready", true);
                    }
                }
                else
                {
                    numbersFotText.Remove(numbersFotText[numbersFotText.Count - 1]);
                }
            }
            
        }
        Clear();
    }

    private void Type()
    {
        if (type == 1)
        {
            numbersToWrite[0] = currentNumbers[1];
            numbersToWrite[1] = currentNumbers[2];
            numbersFotText.Add("?" + SetZnak() + currentNumbers[1] + "=" + currentNumbers[2]);
            if (plus == 0) correct = (currentNumbers[2]) - (currentNumbers[1]);
            else correct = (currentNumbers[2]) + (currentNumbers[1]) - 1;
            if (correct < 1 || correct > 9)
            {
                correctPrim = false;
            }
        }
        else if (type == 2)
        {
            numbersToWrite[0] = currentNumbers[0];
            numbersToWrite[1] = currentNumbers[2];
            numbersFotText.Add(currentNumbers[0] + SetZnak() + "?" + "=" + currentNumbers[2]);
            if (plus == 0) correct = (currentNumbers[2]) - (currentNumbers[0]);
            else correct = (currentNumbers[0]) - (currentNumbers[2]);
            if (correct < 1 || correct > 9)
            {
                correctPrim = false;
            }
        }
        else if (type == 3)
        {
            numbersToWrite[0] = currentNumbers[0];
            numbersToWrite[1] = currentNumbers[1];
            numbersFotText.Add(currentNumbers[0] + SetZnak() + currentNumbers[1] + "=" + "?");
            if (plus == 0) correct = (currentNumbers[0]) + (currentNumbers[1]);
            else correct = (currentNumbers[0]) - (currentNumbers[1]);
            if (correct < 1 || correct > 9)
            {
                correctPrim = false;
            }
        }
    }


    private void SetType()
    {
        for (int i = 0; i < currentNumbers.Length; i++)
        {
            if (currentNumbers[i] == 10)
            {
                type = i + 1;
            }
        }
    }

    private bool OneQuestionMark()
    {
        int q = 0;
        for (int i = 0; i < currentNumbers.Length; i++)
        {
            if (currentNumbers[i] == 10) q++;
        }
        if (q == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Clear()
    {
        for (int i = 0; i < numberImage.Length; i++)
        {
            currentNumberSprite[i] = 9;
            numberImage[i].sprite = numberSprites[currentNumberSprite[i]];
            plus = 0;
            znakImage.sprite = znakSprites[plus];
        }
        correctPrim = true;
    }

    public void SaveQuest()
    {
        if (numbers.Count == 15)
        {
            string numberStr = numbers[0].ToString();
            for (int i = 1; i < numbers.Count; i++)
            {
                numberStr += "-" + numbers[i];
            }
            PlayerPrefs.SetString("Quest3", numberStr);
            SceneManager.LoadScene(8);
        }
        else
        {
            string numberStr = (numbersFotText.Count > 0) ? numbersFotText[0].ToString() : "?";
            for (int i = 1; i < 15; i++)
            {
                if (i < numbersFotText.Count)
                {
                    numberStr += "\n" + numbersFotText[i];
                }
                else
                {
                    numberStr += "\n?";
                }
            }
            text.text = numberStr;
        }
    }

    public void Delete()
    {
        if (numbers.Count > 0)
        {
            numbers.Remove(numbers[numbers.Count - 1]);
            numbersFotText.Remove(numbersFotText[numbersFotText.Count - 1]);
            string numberStr = (numbersFotText.Count > 0) ? numbersFotText[0].ToString() : "";
            for (int i = 1; i < numbersFotText.Count; i++)
            {
                numberStr += "\n" + numbersFotText[i];
            }
            text.text = numberStr;
        }

    }

}
