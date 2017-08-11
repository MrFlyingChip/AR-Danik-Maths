using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {
	
	//Here we keep our effect sounds
	public AudioClip wrong;
	public AudioClip levelUp;
	public AudioClip bestScore;
	public AudioClip gameover;

	public TextUnicode soundIcon;						//This is font awesome icon, we need to change its code (on/off sound)
	public static SoundController sound;				//Static object of this object to use in any script we want
	
	
	void Start(){
		sound = this;									//Object assignment
		setVolume();									//Run this function to set current volume
	}
	
	public void switchSound(){							//This is on sound icon button to switch volume
		if(PlayerPrefs.GetInt("sound",1) == 0)			//We read current sound flag and set it conversely
			PlayerPrefs.SetInt("sound", 1);
		else
			PlayerPrefs.SetInt("sound", 0);
			
		setVolume();									//After we set new flag we start this function that will set volume
	}
	
	public void setVolume(){							//This function reads current sound flag and eneble or disable sound. Also it changes the font awesome icon accordinally
		if(PlayerPrefs.GetInt("sound",1) == 0) {
			AudioListener.volume = 0.0F;
			//soundIcon.text = "\uf026";
		} else {
			AudioListener.volume = 1.0F;
			//soundIcon.text = "\uf028";
		}
	}
	
	//These funcions plays each sounds. They being run from "gameController"
	
	public void playWrong(){
		GetComponent<AudioSource>().PlayOneShot(wrong);
	}
	
	public void playlevelUp(){
		GetComponent<AudioSource>().PlayOneShot(levelUp);
	}
	
	public void playbestScore(){
		GetComponent<AudioSource>().PlayOneShot(bestScore);
	}
	
	public void playGameOver(){
		GetComponent<AudioSource>().PlayOneShot(gameover);
	}
}
