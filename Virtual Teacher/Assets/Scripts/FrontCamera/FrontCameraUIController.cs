using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontCameraUIController : MonoBehaviour {

    public Quest1Controller cardsNumberController;

    public GameObject nextButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (cardsNumberController.CurrentQuestNumber == 14 && !cardsNumberController.IsReady)
        {
            nextButton.GetComponent<Animator>().SetBool("Ready", true);
        }
	}

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeCardType()
    {
        if (cardsNumberController.CurrentType < cardsNumberController.Types.Length - 1)
        {
            cardsNumberController.CurrentType++;
        }
        else
        {
            cardsNumberController.CurrentType = 0;
        }
        cardsNumberController.ChangeCardType();
    }

    public void NextQuest()
    {
        
        if (cardsNumberController.IsReady)
        {
            cardsNumberController.WrongCard();
        }
        else if (cardsNumberController.CurrentQuestNumber == 14)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void PauseGame()
    {
        cardsNumberController.IsReady = !cardsNumberController.IsReady;
        Time.timeScale = 1 - Time.timeScale;
    }
}
