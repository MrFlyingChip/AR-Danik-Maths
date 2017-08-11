using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour {
    
    public Quest1Controller cardNumberController;
    public SoundManager sound;
    private void Start()
    {
       
    }

    public void TimeIsOver()
    {
        cardNumberController.WrongCard();
        GetComponent<Animator>().SetBool("IsReady", cardNumberController.IsReady);
    }

    public void StartNewQuest()
    {
        cardNumberController.GenerateNewQuest();
        GetComponent<Animator>().SetBool("IsReady", cardNumberController.IsReady);
        gameObject.SetActive(false);     
    }

    public void Correct()
    {
        sound.CorrectSound();
    }
    public void Wrong()
    {
        sound.WrongSound();
    }

    public void PlayCorrectNumber(int number)
    {
        sound.NumberSound(number);
    }
}
