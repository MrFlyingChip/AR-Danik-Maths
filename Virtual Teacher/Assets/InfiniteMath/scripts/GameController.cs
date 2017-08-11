using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
	//Declaring game parts and windows
	public GameObject menuWindow;
	public GameObject gameWindow;
	public GameObject failWindow;
	public Slider slider;//slider to use as countdonw
	public Text scoreValue;// in game score label
	public Text levelValue;//in game level label
	public Text bestScoreValue;//Best score label in main menu
	public Text ResultScoreValue;//Score label in fail window
    public GameObject newBS;//New best score message in fail window
	public Color[] randomColors;//Colors for camera background to switch them randomly
	
	public Text expression;//mathematical expression text
	public List<Text> answers;//4 answer variants texts
	
	private int currentLevel;//current reached level
	private int score;//current reached score
	
	private List<float> tempAnswers = new List<float>();//List of answer values
	private int trueID;// ID of true answer that helps to place true answer at random position 
	private int bestScore;//best score ever reached
	private bool isPlaying = false;//Are we in playing mode or not
	private bool inMenu = true;//Are we in main menu or not
	private float timer = 1;//Timer var that helps to make countdown sound as faster as near to game over
	private float lastTime;//This float is to keep time between answers
	
	void OnHUI () {
		if(GUI.Button(new Rect(10,10,100,50),"Clear")){
			PlayerPrefs.DeleteAll();
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	void Start(){										//Runs at start up
		gameWindow.SetActive(false);					//After game launched we hide all windows expect main menu
		failWindow.SetActive(false);
		menuWindow.SetActive(true);
		bestScore = PlayerPrefs.GetInt("bestScore", 0); //Reading best score value and put it in text label
        bestScoreValue.text = bestScore.ToString();
		
	}
	
	void Update(){										//Runs always (every frame)
		if(isPlaying) {
			slider.value -= Time.deltaTime/10;			//If we're playing => decreasing slider value as timeleft countdown
			if(slider.value <= 0)						//Run "GameOver" function if timeleft(slider value) is out
				GameOver();
			timer += Time.deltaTime;					//Increasing timer value as usual timer
			if(timer > slider.value+0.06f) {			//If timer reaches the value proportional to slider then we play "tic" sound and reset timer. So we will have "tic tic" sound with dynamical period
				GetComponent<AudioSource>().Play();		//Plays time sound attached to AudioSource component in "gameController" object 
				timer = 0;
			}
		}
		
		if (Input.GetKeyDown("escape")) {				//"Back" button logic. if we in main menu then quit otherwise go to menu
			if(inMenu)
				Application.Quit();
			else
				gotoMenu();
		}
	}
	
	public void StartPlay(){							//This is on Start/Retry button. Here we switch to game window, reset all values, and generate first expression. 
		gameWindow.SetActive(true);
		failWindow.SetActive(false);
		menuWindow.SetActive(false);
		inMenu = false;
		isPlaying = true;
		slider.value = 1;
		score = 0;
		scoreValue.text = score.ToString();
		currentLevel = 1;
		levelValue.text = currentLevel.ToString();
		Generate();
	}
	
	public void CheckAnswer(int asnwerID){				//This is on all 4 of answer buttons that send here answer ID pre-declared in Button component.
		lastTime = Time.time - lastTime;				//Here we get time between answers to prevent fast chaos selecting answers (otherwise we can cheat scores)
		if(asnwerID == trueID && lastTime > 0.2f)		//If answer ID is equal to true ID that means right answer pressed. Also we check if period is ok and we run according function.
			gotRight();
		else
			gotWrong();
		
		lastTime = Time.time;
	}
	
	public void gotRight(){								//This runs when we pressed right answer 
		slider.value += 0.17f - ((currentLevel-1) *0.007f);//Increasing timeleft value proportionally to current level.
		score += currentLevel*10;							//Increasing score value proportionally to current level (at fisrt level +1, at second +2 etc)
		scoreValue.text = score.ToString();
														//Here is "level up" logic. We check current level then current score and compare it with level marks that you can set as wish. If need we run level up function
		switch (currentLevel) {							
			case 1:
			if(score >= 50) levelUp();
			break;
			case 2:
			if(score >= 300) levelUp();
			break;
			case 3:
			if(score >= 500) levelUp();
			break;
			default:
			if(score >= 100*currentLevel*(currentLevel-2)) levelUp();//When level is 4 or higher we get new mark with this equation
			break;
		}
		
		Generate();										//After got correct answer we generate new one
	}
	
	public void levelUp(){								//Runs when we reached level mark in "gotRight" function. Here we increase current level and play sound
		currentLevel++;	
		levelValue.text = currentLevel.ToString();
		SoundController.sound.playlevelUp();
	}
	
	public void gotWrong(){								//Runs from "CheckAnswer" if we pressed wrong answer
		slider.value -= 0.075f;                         //Decrease timleleft
        SoundController.sound.playWrong();				//Play "wrong" sound
	}
	
	public void GameOver(){								//Runs from "Update" function when slider value reached 0
		gameWindow.SetActive(false);					//We switch to game over window
		newBS.SetActive(false);
		failWindow.SetActive(true);
		isPlaying = false;                              //Tell for "Update" that we're not playing now
        ResultScoreValue.text = score.ToString();

        if (score >= bestScore){							//If rached score is greater than current best score we store new record, play sound, and pop up a label about this
			bestScore = score;
			PlayerPrefs.SetInt("bestScore", bestScore);
            SoundController.sound.playbestScore();
			newBS.SetActive(true);
            bestScoreValue.text = bestScore.ToString();	//Apply new best score text for main menu
		}
        SoundController.sound.playGameOver();			//Play "sad" sound :(
	}
	
	public void gotoMenu(){								//Runs from button in fail window or after "Back" system button
		isPlaying = false;								//Tell for "Update" that we're not playing now
		inMenu = true;									//Tell that we are in menu. This is for "Back" system button
		gameWindow.SetActive(false);					//Switch to main menu window
		failWindow.SetActive(false);
		menuWindow.SetActive(true);
	}
	
	public void Generate(){								//Runs at "StartPlay" and after every correct answer. Generates new expression and answers
		tempAnswers.Clear();							//Clear list filled with previous answers
		int operationID;								//Declaring operation ID variable
		
		//Firstly we choose random opeation by generatig random number ("1" is "+", "2" is "-", "3" is "×", "4" is "÷")
		
		if(currentLevel == 1)							//Here I added a condition that limits operaion range for first level 
			operationID = Random.Range(1,2);
		else											//If we are at 1st level then we use only + or -
			operationID = Random.Range(1,5);			//Otherwise we use all operaions
		
		int currentLimit = 10+ (currentLevel-1)*5;		//Here we declaring basic maximum limit for numbers in expression proportionally to current level (level 1 - limit 10, level 2 - 15 etc +5 each)
		float left = Random.Range((currentLevel-1)*5, currentLimit);//"Left side" integer. Generated according to current level and limit
		float right = 0;								//Declaring "Right side" integer variable
		string operationTxt = "";						//Declaring operation symbol variable (+-×÷)
		float result = 0;								//Declaring vaiable that keeps correct answer
		
		//Then according to operation ID and "Left" integer we generate "Right" ineteger and answers
		//Here is logic for each operation
		switch (operationID){							
			case 1:										//Operation +
				right = Random.Range(1,currentLimit);	//Generating random "Right" integer in range
				operationTxt = "+";						//Operation string assignment
				result = left + right;					//Calculating the result of expression

				tempAnswers.Add(result);				//Insert correct answer in answers list
				//Here (and in each case) is logic part that generates three fake answers (should not be as true answer). All 4 results go to tempAnswers list
				if((left-right) >= 0) {					
					tempAnswers.Add(left-right);
				} else {
					if(right-left != result)
						tempAnswers.Add(right-left);
					else
						tempAnswers.Add(result*2);
				}
				tempAnswers.Add(!tempAnswers.Contains(result+1) ? result + 1 : result + 2);
				tempAnswers.Add(result-1);
			break;
			case 2:										//Operation -
				right = Random.Range(1,(int)Mathf.Round (left+1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
				operationTxt = "-";						//Operation string assignment
				result = left - right;					//Calculating the result of expression	
				
				tempAnswers.Add(result);				//Insert correct and three fake answers in answers list
				tempAnswers.Add(result+1);
				tempAnswers.Add(result-1);
				tempAnswers.Add(left+right);
			break;
			case 3:										//Operation ×
				right = Random.Range(1,3+ (currentLevel-1)*2);//Generating random "Right" integer in range. We limit this integer with special logic so the result won't be too big.
				operationTxt = "×";						//Operation string assignment
				result = left * right;					//Calculating the result of expression
				
				tempAnswers.Add(result);				//Insert correct and three fake answers in answers list
				tempAnswers.Add(result+right);
				tempAnswers.Add(result-1);
				tempAnswers.Add(result+1);
			break;
			case 4:										//Operation ÷
				//Here we generate left and right inegers with special logic so that result will be always whole number (not float)
				left = Random.Range(1,3+ (currentLevel-1)*2);
				right = Random.Range(1,3+ (currentLevel-1)*2);
				float tempSum = left*right;
				left = tempSum;
				operationTxt = "÷";
				result = left / right;					//Calculating the result of expression
				
				tempAnswers.Add(result);				//Insert correct and three fake answers in answers list
				if((left-right) != result)
					tempAnswers.Add(left-right);
				else
					tempAnswers.Add(left+right);
				tempAnswers.Add(result+1);
				if(left*right != result)
					tempAnswers.Add(left*right);
				else
					tempAnswers.Add(result*2);
			break;
		}
		
		expression.text = left.ToString() +operationTxt+ right.ToString();//Generated expression goes to UI element as text
		
		trueID = Random.Range(0,4);						//Generating trueID (for random position)
		answers[trueID].text = result.ToString();		//Correct answer as string goes to answers list with generated index
		answers[trueID].fontSize = GetFontSize(answers[trueID].text); //Setting suitable font
		tempAnswers.RemoveAt(0);						//Remove correct answer, it is always with index 0 coz we insert it first
		int i = 0;										//Declaring counter variable
		foreach(Text answer in answers){				//Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
			if(i != trueID){							//Skip the element that is already filled with correct answer
				int rand = Random.Range(0, tempAnswers.Count);
				answer.text = tempAnswers[rand].ToString();
				answer.fontSize = GetFontSize(answer.text);//Setting suitable font
				tempAnswers.RemoveAt(rand);
			}
			i++;
		}

        ChangeColors();								//And at the end we run function that changes the background :)
	}
	
	public int GetFontSize(string answer) {				//Function that gets suitable font size for given string length
		int fontSize;
		switch (answer.Length){
            case 1:
                fontSize = 210;
                break;
            case 2:
                fontSize = 200;
                break;
            case 3:
                fontSize = 140;
                break;
            case 4:
                fontSize = 100;
                break;
            default:
                fontSize = 75;
                break;
        }
		return fontSize;
	}
	
	public void ChangeColors(){						//Runs every time we generate new expression
		Color newBack = randomColors[Random.Range(0, randomColors.Length)];//Random pick up pre-assigned colors, you can create them as much as you wish in inspector
        //Simple logic that changes answers font color.
        List<GameObject> tempList1 = new List<GameObject>();
        foreach (Text answ in answers)
        {
            tempList1.Add(answ.gameObject);
        }
        List<GameObject> tempList2 = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, tempList1.Count);
            tempList2.Add(tempList1[rand]);
            tempList1.RemoveAt(rand);
        }

        for (int i = 0; i < 4; i++)
        {
            tempList2[i].transform.parent.gameObject.GetComponent<Image>().color = randomColors[i];
        }
    }
	
    public void LoadScene()
    {
        SceneManager.LoadScene(9);
    }

	public void goToUrl(string url){					//This is linked to some buttons in inspector. Opens received url. 
		Application.OpenURL(url);
	}	
}
