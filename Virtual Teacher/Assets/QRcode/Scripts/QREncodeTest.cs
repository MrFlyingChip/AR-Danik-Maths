/// <summary>
/// write by 52cwalk,if you have some question ,please contract lycwalk@gmail.com
/// </summary>
/// 
/// 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QREncodeTest : MonoBehaviour {
	public QRCodeEncodeController e_qrController;
	public RawImage qrCodeImage;
   

    // Use this for initialization
    void Start () {
		if (e_qrController != null) {
			e_qrController.onQREncodeFinished += qrEncodeFinished;//Add Finished Event
		}
        Encode();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void qrEncodeFinished(Texture2D tex)
	{
		if (tex != null && tex != null) {
			//int width = tex.width;
			//int height = tex.height;
			//float aspect = width * 1.0f / height;
			//qrCodeImage.GetComponent<RectTransform> ().sizeDelta = new Vector2 (170, 170.0f / aspect);
			qrCodeImage.texture = tex;
		} else {

		}
	}

	public void Encode()
	{
		if (e_qrController != null) {
            if (PlayerPrefs.HasKey("Quest1") && PlayerPrefs.HasKey("Quest2") && PlayerPrefs.HasKey("Quest3"))
            {
                string valueStr = PlayerPrefs.GetString("Quest1") + "&" + PlayerPrefs.GetString("Quest2") + "&" + PlayerPrefs.GetString("Quest3");
                e_qrController.Encode(valueStr);
            }
        }
	}

   

	public void ClearCode()
	{
		qrCodeImage.texture = null;
	}

}
