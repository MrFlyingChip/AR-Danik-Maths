using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using UnityEngine.Purchasing;

public class DemoScript : MonoBehaviour {

	public bool hideGUI = false;
	public Texture2D texture;

    public GameObject paidMessageBox;

    public RawImage qrCodeImage;
    public RawImage qrCodeImage2;

    public GameObject QRInput;
    public GameObject QROutput;

    public InputField if1;
    public InputField if2;

    public Text text1;
    public Text text2;

    public GameObject home;
    public GameObject ok;

    public GameObject homeButton;
    
    void OnEnable ()
	{
		// call backs
		ScreenshotManager.OnScreenshotTaken += ScreenshotTaken;
		ScreenshotManager.OnScreenshotSaved += ScreenshotSaved;	
		ScreenshotManager.OnImageSaved += ImageSaved;
    }

	void OnDisable ()
	{
		ScreenshotManager.OnScreenshotTaken -= ScreenshotTaken;
		ScreenshotManager.OnScreenshotSaved -= ScreenshotSaved;	
		ScreenshotManager.OnImageSaved -= ImageSaved;
	}

	public void OnSaveScreenshotPress()
	{  
            if (PlayerPrefs.GetString("Code") != "Paid")
            {
                PlayerPrefs.SetString("Code", "Played");
            }
            Show();
            ScreenshotManager.SaveScreenshot("Math", "AR Danik Maths", "png");
        StartCoroutine(ShowHome());   
    }

    public IEnumerator ShowHome()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3);
        QROutput.SetActive(false);
        homeButton.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

	public void OnSaveImagePress()
	{
		ScreenshotManager.SaveImage(texture, "MyImage", "MyImages", "png");
	}

	void ScreenshotTaken(Texture2D image)
	{

	}
	
	void ScreenshotSaved(string path)
	{
	}
	
	void ImageSaved(string path)
	{
	}

    public void Show()
    {
        home.SetActive(true);
        ok.SetActive(true);
        QROutput.SetActive(true);
        text1.text = if1.text;
        text2.text = if2.text;
        qrCodeImage2.texture = qrCodeImage.texture;
        QRInput.SetActive(false);
    }
}