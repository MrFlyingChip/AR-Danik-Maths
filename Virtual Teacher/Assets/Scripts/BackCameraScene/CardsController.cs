using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class Animal
{
    public GameObject[] animals;
}



public class CardsController : MonoBehaviour {

    public List<int> IDs;
    public List<GameObject> images;
    public bool canRegister = true;
    public bool firstRegistered;
    public bool animated;
    public float timeToRegister;
    private float currentTimeToRegister;

    public SoundManager sound;

    public Animal[] animals;

    public int currentCard;
	// Use this for initialization
	void Start () {
        currentTimeToRegister = timeToRegister;
    }
	
    public void Clean()
    {
        currentTimeToRegister = timeToRegister;
        firstRegistered = false;
        canRegister = true;
        animated = false;
    }

	// Update is called once per frame
	void Update () {
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

    public void RegisterID(int id, GameObject gameObject)
    {
        if (canRegister)
        {
            if (!firstRegistered)
            {
                firstRegistered = true;
            }
            IDs.Add(id);
            images.Add(gameObject);
        }
    }

    public void Visualize()
    {
        animated = true;
        if (IDs.Count == 3)
        {
            ProceedThreeCards();
        }
        else if (IDs.Count == 5)
        {
            ProceedFiveCards();
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ProceedOneCard()
    {
        if (IDs[0] < 11 && IDs[0] > 0)
        {
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 1);
        }
        else if (IDs[0] < 21 && IDs[0] > 10)
        {
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ProceedTwoCards()
    {
        if (((IDs[0] < 11 && IDs[0] > 0) || (IDs[0] < 26 && IDs[0] > 20)) && ((IDs[1] < 11 && IDs[1] > 0) || (IDs[1] < 26 && IDs[1] > 20)))
        {
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
            if (images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>() != null)
            {
                images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 2);
            }
            images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
            if (images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>() != null)
            {
                images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 2);
            }
        }
        else if (((IDs[0] < 11 && IDs[0] > 0) || (IDs[0] < 41 && IDs[0] > 30)) && ((IDs[1] < 11 && IDs[1] > 0) || (IDs[1] < 41 && IDs[1] > 30)))
        {
            
            if (images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children != null)
            {
                images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
                images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 3);
            }       
            if (images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().children != null)
            {
                images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
                images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 3);
            }
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    private bool correct;

    public void ProceedFiveCards()
    {
        images.Sort(delegate (GameObject x, GameObject y)
        {
            if (x == null && y == null) return 0;
            else return x.transform.position.x.CompareTo(y.transform.position.x);
        });
        if (images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID == 42 && images[3].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID == 41)
        {
            if (images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID + images[2].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID == images[4].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID)
            {
                correct = true;
            }
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 4);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetBool("Correct", true);
        }
        else if (images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID == 43 && images[3].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID == 41)
        {
            if (images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID - images[2].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID == images[4].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID)
            {
                correct = true;
            }
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 4);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetBool("Correct", true);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ProceedThreeCards()
    {
        images.Sort(delegate (GameObject x, GameObject y)
        {
            if (x == null && y == null) return 0;
            else return x.transform.position.x.CompareTo(y.transform.position.x);
        });
        if (images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID == 46)
        {
            if (images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID > images[2].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID)
            {
                correct = true;
            }
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 4);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetBool("Correct", true);
        }
        else if (images[1].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID == 47)
        {
            if (images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID < images[2].GetComponent<Vuforia.BackCameraTrackableBehavior>().cardID)
            {
                correct = true;
            }
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 4);
            images[0].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetBool("Correct", true);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ShowAnimals(int number)
    {
        int animal = 0;
        if (IDs[0] == number) animal = IDs[1];
        else animal = IDs[0];

        for (int i = 0; i < number; i++)
        {
            animals[animal - 21].animals[i].SetActive(true);
        }

    }

    public void SayColor()
    {
        int colorNumber = 0;
        if (IDs[0] > IDs[1]) colorNumber = IDs[0] - 30;
        else colorNumber = IDs[1] - 30;
        sound.ColorSound(colorNumber);
    }

    public void ShowNextBall()
    {
        currentCard++;
        if (currentCard < images.Count)
        {
            images[currentCard].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.SetActive(true);
            images[currentCard].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetInteger("Type", 4);
            images[currentCard].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetBool("Correct", true);
        }
        else
        {
            if (correct)
            {
                sound.CorrectSound();
                GameObject.Find("Canvas").GetComponent<UIController>().ShowNextButton();
            }
            else
            {
                for(int i = 0; i < images.Count; i++)
                {
                    images[i].GetComponent<Vuforia.BackCameraTrackableBehavior>().children.GetComponent<Animator>().SetBool("Correct", false);
                }
            }
        }
        
    }

}
