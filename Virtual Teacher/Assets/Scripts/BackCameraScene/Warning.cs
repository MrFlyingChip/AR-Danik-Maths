using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour {

    public string[] languageWarnings;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Language"))
        {
            GetComponent<Text>().text = languageWarnings[PlayerPrefs.GetInt("Language")];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
