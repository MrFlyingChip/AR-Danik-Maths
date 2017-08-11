using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface CardsNumberController
{

    void RegisterCard(int cardId);
    void GenerateNewQuest();
    void CorrectCard();
    void WrongCard();
    void ChangeCardType();

    int CurrentType { get; set; }
    bool IsReady { get; set; }
    int CurrentQuestNumber { get; set; }
    CardsNumberType[] Types { get;}
}

[Serializable]
public class CardsNumberType
{
    public Sprite[] typeSprites;
    public int[] cardsID;
}


