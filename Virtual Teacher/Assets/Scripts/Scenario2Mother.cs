using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenario2Mother : MonoBehaviour {


    public Text text;
    public Image[] numberImage;
    public Sprite[] numberSprites;
    public Image znakImage;
    public Sprite[] znakSprites;
    private int[] currentNumberSprite = new int[2];
    private List<string> numbers = new List<string>();
    private List<string> numbersFotText = new List<string>();
    private int[] currentNumbers = new int[2];

    public Animator saveButton;
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
        SetZnakImage();
    }

    private void SetZnakImage()
    {
        if (currentNumberSprite[0] > currentNumberSprite[1]) znakImage.sprite = znakSprites[0];
        else if (currentNumberSprite[0] < currentNumberSprite[1]) znakImage.sprite = znakSprites[1];
        else if(currentNumberSprite[0] == currentNumberSprite[1]) znakImage.sprite = znakSprites[2];
    }

    public string SetZnak()
    {
        string znak = "";
        if (currentNumberSprite[0] > currentNumberSprite[1]) znak = ">";
        else if (currentNumberSprite[0] < currentNumberSprite[1]) znak = "<";
        else if (currentNumberSprite[0] == currentNumberSprite[1]) znak = "=";
        return znak;
    }

    public void AcceptNumber()
    {
        currentNumbers[0] = currentNumberSprite[0] + 1;
        currentNumbers[1] = currentNumberSprite[1] + 1;
        if (numbers.Count < 15)
            {
                numbers.Add(currentNumbers[0] + "," + currentNumbers[1]);
                numbersFotText.Add(currentNumbers[0] + SetZnak() + currentNumbers[1]);
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
                Clear();
            }
        }   
    

    private void Clear()
    {
        currentNumberSprite[0] = 0;
        currentNumberSprite[1] = 0;
        numberImage[0].sprite = numberSprites[currentNumberSprite[0]];
        numberImage[1].sprite = numberSprites[currentNumberSprite[1]];
        SetZnakImage();
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
            PlayerPrefs.SetString("Quest2", numberStr);
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
