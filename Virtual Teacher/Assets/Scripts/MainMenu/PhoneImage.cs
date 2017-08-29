using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneImage : MonoBehaviour {

    public GameObject questionMark;

    public GameObject paidMessageBox;

    public FrontCameraButton front;

    public string QuestName;
    public void ChangeQuestionMark()
    {
        questionMark.GetComponent<Animator>().SetBool("IsReady", true);
    }
    public void FalseQuestionMark()
    {
        questionMark.GetComponent<Animator>().SetBool("IsReady", false);
    }


    public void LoadScene(int scene)
    {
        if (PlayerPrefs.GetString(QuestName) != "Played")
        {
            SceneManager.LoadScene(scene);
        }
        else
        {
            paidMessageBox.SetActive(true);
        }
    }

}
