using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlication : MonoBehaviour {
    public Sprite[] sprites;

	// Use this for initialization
	void Start () {
        GetComponent<Image>().sprite = sprites[PlayerPrefs.GetInt("Language")];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
