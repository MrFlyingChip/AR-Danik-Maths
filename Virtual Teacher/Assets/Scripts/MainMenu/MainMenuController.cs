using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Purchasing;

[Serializable]
public class Language
{
    public Sprite languageSprite;
    public string linkToPDF;
    public string buyText;

    public void SetNewLanguage(Image languageImage, ref string currentLinkToPDF)
    {
        languageImage.sprite = languageSprite;
        currentLinkToPDF = linkToPDF;
    }
}

public class MainMenuController : MonoBehaviour {

    public Language[] languages;
    public Image languageImage;
    private string currentLinkToPDF;
    private int currentLanguage;

    public Image motherModeButton;
    public Sprite hasQuests;

    public static int paid;
    public GameObject paidMessageBox;
    public GameObject paidMessageBox1;
    public InputField paidText;
    public string serverResponse;

    public Text buyText;
    public Text buyText1;
    public GameObject invalid;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Language"))
        {
            currentLanguage = PlayerPrefs.GetInt("Language");
        }
        else
        {
            PlayerPrefs.SetInt("Language", 0);
        }
        languages[currentLanguage].SetNewLanguage(languageImage, ref currentLinkToPDF);
        buyText.text = languages[currentLanguage].buyText;
        if (PlayerPrefs.HasKey("Paid"))
        {
            paid = PlayerPrefs.GetInt("Paid");
        }
        else
        {
            PlayerPrefs.SetInt("Paid", 0);
        }

        if (!PlayerPrefs.HasKey("Game1") && !PlayerPrefs.HasKey("Game2"))
        {
            PlayerPrefs.SetString("Game1",  "none");
            PlayerPrefs.SetString("Game2", "none");
        }
        if (!PlayerPrefs.HasKey("Mother"))
        {
            PlayerPrefs.SetString("Mother", "none");
        }
        if (!PlayerPrefs.HasKey("Code"))
        {
            PlayerPrefs.SetString("Code", "none");
        }
        if (PurchaseManager.CheckBuyState("code1"))
        {
            PlayerPrefs.SetString("Code", "Paid");

        }
        if (PurchaseManager.CheckBuyState("game"))
        {
            PlayerPrefs.SetString("Game1", "Paid");
            PlayerPrefs.SetString("Game2", "Paid");
            PlayerPrefs.SetString("Mother", "Paid");
        }
        if (PlayerPrefs.HasKey("Quest1") && PlayerPrefs.HasKey("Quest2") && PlayerPrefs.HasKey("Quest3"))
        {
            motherModeButton.sprite = hasQuests;
        }
        PurchaseManager.OnPurchaseNonConsumable += PurchaseManager_OnPurchaseNonConsumable;
	}

    private void PurchaseManager_OnPurchaseNonConsumable(PurchaseEventArgs args)
    {
        if(args.purchasedProduct.definition.id == "game")
        {
            PlayerPrefs.SetString("Game1", "Paid");
            PlayerPrefs.SetString("Game2", "Paid");
            PlayerPrefs.SetString("Mother", "Paid");
            SceneManager.LoadScene(0);
        }
        else if (args.purchasedProduct.definition.id == "code1")
        {
            PlayerPrefs.SetString("Code", "Paid");
            SceneManager.LoadScene(0);
        }
    }

    public void LoadMother()
    {
        if (PlayerPrefs.GetString("Code") != "Played")
        {
            SceneManager.LoadScene(8);
        }
        else
        {
            paidMessageBox1.SetActive(true);
        }
    }

    public void LoadCode()
    {
        if (PlayerPrefs.GetString("Mother") == "Paid")
        {
            
            SceneManager.LoadScene(20);
        }
        else
        {
            paidMessageBox.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update () {
        if (serverResponse == "{\"status\":\"0\"}")
        {
            paid = 1;
            PlayerPrefs.SetString("Game1", "Paid");
            PlayerPrefs.SetString("Game2", "Paid");
            invalid.SetActive(false);
            paidMessageBox.SetActive(false);
            SceneManager.LoadScene(0);
        }
        else if(serverResponse == "{\"status\":\"1\"}")
        {
            
            invalid.SetActive(true);
            invalid.GetComponent<Text>().text = "Invalid Code!";
        }
        else if (serverResponse == "{\"status\":\"2\"}")
        {
            invalid.SetActive(true);
            invalid.GetComponent<Text>().text = "Limit Reached!";
        }
    }

    public void SetNewLanguage()
    {
        if (currentLanguage != languages.Length - 1)
        {
            currentLanguage++;
        }
        else
        {
            currentLanguage = 0;
        }
        languages[currentLanguage].SetNewLanguage(languageImage, ref currentLinkToPDF);
        buyText.text = languages[currentLanguage].buyText;
        buyText1.text = languages[currentLanguage].buyText;
        PlayerPrefs.SetInt("Language", currentLanguage);
    }

    public void LinkToPDF()
    {
        LinkToWebSite(currentLinkToPDF);
    }

    public void LinkToWebSite(string url)
    {
        Application.OpenURL(url);
    }

    public void ShowPaidMessageBox()
    {
        if (PlayerPrefs.GetString("Game1") != "Paid")
        {
            paidMessageBox.SetActive(true);
        }
       
    }

    public void HidePaidMessageBox()
    {
        paidMessageBox.SetActive(false);
    }

    public void CheckCodeForPaid()
    {
        MD5 md5Hash = MD5.Create();
        string hash = GetMd5Hash(md5Hash, paidText.text.ToUpper());
        serverResponse = null;
       
        StartCoroutine(GET(hash));
             
        
    }

    string GetMd5Hash(MD5 md5Hash, string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    public void ChangeText()
    {
        string text = paidText.text.ToUpper();
        Debug.Log(text);
        paidText.text = text;
    }

    public IEnumerator GET(string data)
    {
        WWW Query = new WWW("http://870037.kidsdan.web.hosting-test.net/controllers/getcode.php?code=" + data + "&id=11");
        yield return Query;
        if (Query.error != null)
        {
            Debug.Log("Server does not respond : " + Query.error);
        }
        else
        {
            serverResponse = Query.text;
        }
        Query.Dispose();
        
    }
}


