using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public SoundManager sound;

    public GameObject[] numbers;
    public int maxNumber;

    private bool reloadScene;
    private float timeToReload = 1;

    public GameObject nextButton;

    public void Update()
    {
        if (reloadScene)
        {
            timeToReload -= Time.deltaTime;
        }
        if (timeToReload < 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void StartGame(int number)
    {
        maxNumber = number - 1;
        for (int i = 0; i < number; i++)
        {
            numbers[i].SetActive(true);
        }
    }

    public void NumberButton(int number)
    {
        sound.NumberSound(number + 1);
        numbers[number].SetActive(false);
        if (number == maxNumber)
        {
            reloadScene = true;
        }
    }

    public void ShowNextButton()
    {
        nextButton.SetActive(true);
    }

}
