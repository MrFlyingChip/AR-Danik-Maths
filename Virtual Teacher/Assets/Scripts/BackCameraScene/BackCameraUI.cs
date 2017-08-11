using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackCameraUI : MonoBehaviour {

    public Image[] images;
    public Sprite[] sprites;
    public char[] chars;

    public GameObject holder;
    public GameObject home;
    public GameObject game;
    public GameObject game1;
    public GameObject playButton;
    public int number;

   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Display(string str)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
        if (GameObject.Find("BackCameraController").GetComponent<BackCameraController>().questType == 6)
        {
            int count = int.Parse(str);
           
            images[0].gameObject.SetActive(true);
            images[0].sprite = sprites[0];
            for (int i = 1; i <= count; i++)
            {
                images[i].gameObject.SetActive(true);
                images[i].sprite = sprites[number];
            }
        }
        else
        {   
            char[] charStr = str.ToCharArray();
            for (int i = 0; i < charStr.Length; i++)
            {
                for (int j = 0; j < chars.Length; j++)
                {
                    if (charStr[i] == chars[j])
                    {
                        images[i].gameObject.SetActive(true);
                        images[i].sprite = sprites[j];
                    }
                }
            }
        }
        
    }

    public void Click(int number)
    {
        GameObject.Find("BackCameraController").GetComponent<BackCameraController>().umnoc = number;
        playButton.SetActive(true);
    }

    public void Play()
    {
        GameObject.Find("BackCameraController").GetComponent<BackCameraController>().GenerateQuest();
        home.SetActive(true);
        game.SetActive(true);
        game1.SetActive(true);
        playButton.SetActive(false);
        holder.SetActive(false);
    }


}
