using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour {

    public SoundManager sound;
    public int n;

    public GameObject mesh;
    public GameObject verevka;
    public GameObject bomb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreatingBall()
    {
        sound.BallWoopSound();
    }

    public void SayBall(int number)
    {
        n = number;
        sound.NumberSound(number);
    }

    public void BreakBall()
    {
        sound.BallBreakSound();
    }
    public void BreakBallUncorrect()
    {
        sound.WrongSound();
    }
    public void BreakBallCorrect()
    {
        sound.CorrectSound();
        if (GameObject.Find("BackCameraController").GetComponent<BackCameraController>().questType == 6)
        {
            GameObject.Find("BackCameraController").GetComponent<BackCameraController>().StartGame();
        }
    }

    public void HideBall()
    {
        GetComponent<Animator>().SetBool("Ready", false);
        mesh.SetActive(true);
        verevka.SetActive(true);
        bomb.SetActive(false);
        gameObject.SetActive(false);
    }

    public void BallToIdle()
    {
        GetComponent<Animator>().SetBool("Ready", false);
        mesh.SetActive(true);
        verevka.SetActive(true);
        bomb.SetActive(false);
        if (GameObject.Find("BackCameraController").GetComponent<BackCameraController>().questType != 6 && GetComponent<Animator>().GetBool("Correct"))
        {
            GameObject.Find("BackCameraController").GetComponent<BackCameraController>().GenerateQuest();
        }
        else if (!GetComponent<Animator>().GetBool("Correct"))
        {
            GameObject.Find("BackCameraController").GetComponent<BackCameraController>().GenerateQuest();
        }
        gameObject.SetActive(false);
    }

    public void DestroyBall()
    {
        if (GetComponent<Animator>().GetInteger("Type") == 1) GameObject.Find("Canvas").GetComponent<UIController>().StartGame(n);
        else if (GetComponent<Animator>().GetInteger("Type") == 2)
        {
            GameObject.Find("CardsController").GetComponent<CardsController>().ShowAnimals(n);
            GameObject.Find("Canvas").GetComponent<UIController>().ShowNextButton();
        }
        else if(GetComponent<Animator>().GetInteger("Type") == 4) GameObject.Find("Canvas").GetComponent<UIController>().ShowNextButton();
        Destroy(gameObject);
    }

    public void SayColor()
    { 
        GameObject.Find("Canvas").GetComponent<UIController>().ShowNextButton();
        GameObject.Find("CardsController").GetComponent<CardsController>().SayColor();
    }

    public void SayZnak(int number)
    {
        sound.ZnakSound(number);
    }

    public void NextBall()
    {
        GameObject.Find("CardsController").GetComponent<CardsController>().ShowNextBall();
    }
}
