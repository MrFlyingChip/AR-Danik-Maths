using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QRDecodeTest : MonoBehaviour
{
	public QRCodeDecodeController e_qrController;

	public GameObject resetBtn;

	public GameObject scanLineObj;

	/// <summary>
	/// when you set the var is true,if the result of the decode is web url,it will open with browser.
	/// </summary>
	public bool isOpenBrowserIfUrl;

	private void Start()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.onQRScanFinished += new QRCodeDecodeController.QRScanFinished(this.qrScanFinished);
		}
	}

	private void Update()
	{
	}

	private void qrScanFinished(string dataText)
	{
        string[] quests = dataText.Split('&');

        if (quests.Length == 3)
        {
            PlayerPrefs.SetString("Quest1", quests[0]);
            PlayerPrefs.SetString("Quest2", quests[1]);
            PlayerPrefs.SetString("Quest3", quests[2]);
            SceneManager.LoadScene(0);
        }
        else if (this.resetBtn != null)
		{
            SceneManager.LoadScene(20);
        }
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(false);
		}

	}

	public void Reset()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.Reset();
		}
		
		if (this.resetBtn != null)
		{
			this.resetBtn.SetActive(false);
		}
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(true);
		}
	}

	public void Play()
	{
		Reset ();
		if (this.e_qrController != null)
		{
			this.e_qrController.StartWork();
		}
	}

	public void Stop()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.StopWork();
		}

		if (this.resetBtn != null)
		{
			this.resetBtn.SetActive(false);
		}
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(false);
		}
	}

	public void GotoNextScene(int scenename)
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.StopWork();
		}
		//Application.LoadLevel(scenename);
		SceneManager.LoadScene(scenename);
	}
}
