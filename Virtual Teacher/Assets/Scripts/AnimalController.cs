using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour {

    public SoundManager sound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        if (GameObject.Find("BackCameraController").GetComponent<BackCameraController>().gameStarted)
        {
            BackCameraController.currentNumber++;
            sound.NumberSound(BackCameraController.currentNumber);
            if (BackCameraController.currentNumber == GameObject.Find("BackCameraController").GetComponent<BackCameraController>().c)
            {
                GameObject.Find("BackCameraController").GetComponent<BackCameraController>().GenerateQuest();
            }
            gameObject.SetActive(false);
        }      
    }
}
