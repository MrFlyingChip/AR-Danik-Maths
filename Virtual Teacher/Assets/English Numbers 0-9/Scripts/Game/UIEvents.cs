using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.

public class UIEvents : MonoBehaviour
{
		public void AlbumShapeEvent (TableShape tableShape)
		{
				if (tableShape == null) {
						return;
				}

				if (tableShape.isLocked) {
					return;
				}

				TableShape.selectedShape = tableShape;
				LoadGameScene ();
		}

		public void PointerButtonEvent (Pointer pointer)
		{
				if (pointer == null) {
						return;
				}
				if (pointer.group != null) {
						ScrollSlider scrollSlider = GameObject.FindObjectOfType (typeof(ScrollSlider)) as ScrollSlider;
						if (scrollSlider != null) {
								scrollSlider.DisableCurrentPointer ();
								FindObjectOfType<ScrollSlider> ().currentGroupIndex = pointer.group.Index;
								scrollSlider.GoToCurrentGroup ();
						}
				}
		}

		public void LoadGameScene(){
			StartCoroutine(MySceneLoader.LoadSceneAsync ("Game"));
		}

		public void LoadAlbumScene ()
		{
			StartCoroutine(MySceneLoader.LoadSceneAsync ("Album"));
		}
	
		public void NextClickEvent ()
		{
			try{
				GameObject.FindObjectOfType<GameManager> ().NextShape ();
			}catch(System.Exception ex){

			}
		}

		public void PreviousClickEvent ()
		{
			try{
				GameObject.FindObjectOfType<GameManager> ().PreviousShape ();
			}catch(System.Exception ex){
			
			}
		}

		public void SpeechClickEvent ()
		{
				Shape shape = GameObject.FindObjectOfType<Shape> ();
				if (shape == null) {
						return;
				}
				shape.Spell ();
		}

		public void ResetShape ()
		{
				GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
				if (gameManager != null) {
            gameManager.ResetShape();
            //if(!gameManager.shape.completed){
            //		gameManager.DisableGameManager ();
            //		GameObject.Find ("ResetConfirmDialog").GetComponent<Dialog> ().Show ();
            //}else{
            //	gameManager.ResetShape();
            //}
        }
		}

		public void PencilClickEvent (Pencil pencil)
		{
				if (pencil == null) {
						return;
				}
				GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
				if (gameManager == null) {
						return;
				}
				if (gameManager.currentPencil != null) {
						gameManager.currentPencil.DisableSelection ();
						gameManager.currentPencil = pencil;
				}
				gameManager.SetShapeOrderColor ();
				pencil.EnableSelection ();
		}

		public void ResetConfirmDialogEvent (GameObject value)
		{
				if (value == null) {
						return;
				}
		
				GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
		
				if (value.name.Equals ("YesButton")) {
						Debug.Log ("Reset Confirm Dialog : Yes button clicked");
						if (gameManager != null) {
								gameManager.ResetShape ();
						}
			
				} else if (value.name.Equals ("NoButton")) {
						Debug.Log ("Reset Confirm Dialog : No button clicked");
				}
				value.GetComponentInParent<Dialog> ().Hide ();
				if (gameManager != null) {
						gameManager.EnableGameManager ();
				}
		}

		public void ResetGame(){
			DataManager.ResetGame ();
		}

		public void LeaveApp(){
        StartCoroutine(MySceneLoader.LoadSceneAsync("BackCameraSceneChoose"));
    }
}
