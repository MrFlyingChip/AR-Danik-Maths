using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlication : MonoBehaviour {
    public Sprite[] sprites;
    public GameObject fuck;

	// Use this for initialization
	void Start () {
        GetComponent<Image>().sprite = sprites[PlayerPrefs.GetInt("Language")];
        if (PlayerPrefs.GetString("Code") != "Paid")
        {
            fuck.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
