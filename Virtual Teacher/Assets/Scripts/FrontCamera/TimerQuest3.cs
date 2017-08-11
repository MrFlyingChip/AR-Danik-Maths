using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerQuest3 : MonoBehaviour {

    public Quest3Controller cardNumberController;
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
    }

    public void PlayCorrectNumber()
    {
        cardNumberController.PlayCorrectNumber();
    }
}
