using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FrontCameraButton : MonoBehaviour {

    public GameObject children;
    public int scene;

	void OnMouseDown()
    {
        GetComponent<Animator>().SetBool("Clicked", true);
        children.GetComponent<Animator>().SetBool("IsReady", true);
    }
}
