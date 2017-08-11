using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenario1Mother : MonoBehaviour {

    public Text text;
    public Image numberImage;
    public Sprite[] numberSprites;
    private int currentNumberSprite;
    private List<int> numbers = new List<int>();
    public Animator saveButton;
    public void ChangeNumberSprite()
    {
        if (currentNumberSprite < numberSprites.Length - 1)
        {
            currentNumberSprite++;          
        }
        else
        {
            currentNumberSprite = 0;
        }
        numberImage.sprite = numberSprites[currentNumberSprite];
    }

    public void AcceptNumber()
    {
        if (numbers.Count < 15)
        {
            numbers.Add(currentNumberSprite + 1);
            string numberStr = numbers[0].ToString();
            for (int i = 1; i < numbers.Count; i++)
            {
                numberStr += "\n" + numbers[i];
            }
            text.text = numberStr;
            currentNumberSprite = 0;
            numberImage.sprite = numberSprites[currentNumberSprite];
            if (numbers.Count == 15)
            {
                saveButton.SetBool("Ready", true);
            }
        }     
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
            PlayerPrefs.SetString("Quest1", numberStr);
            SceneManager.LoadScene(8);
        }
        else
        {
            string numberStr = (numbers.Count > 0) ? numbers[0].ToString() : "?";
            for (int i = 1; i < 15; i++)
            {
                if (i < numbers.Count)
                {
                    numberStr += "\n" + numbers[i];
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
            string numberStr = (numbers.Count > 0) ? numbers[0].ToString() : "";
            for (int i = 1; i < numbers.Count; i++)
            {
                numberStr += "\n" + numbers[i];
            }
            text.text = numberStr;
        }
        
    }

}
