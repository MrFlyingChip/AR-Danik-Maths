using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadQRCodeGenerator(int scene)
    {
        if (PlayerPrefs.HasKey("Quest1") && PlayerPrefs.HasKey("Quest2") && PlayerPrefs.HasKey("Quest3"))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
