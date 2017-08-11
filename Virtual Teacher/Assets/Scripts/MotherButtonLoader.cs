using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherButtonLoader : MonoBehaviour {

    public string questName;
    public Sprite[] sprites;


	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey(questName))
        {
            GetComponent<Image>().sprite = sprites[1];
        }
        else
        {
            GetComponent<Image>().sprite = sprites[0];
        }
	}
	
    public void DeleteQuest()
    {
        PlayerPrefs.DeleteKey(questName);
        GetComponent<Image>().sprite = sprites[0];
    }

	// Update is called once per frame
	void Update () {
		
	}
}
