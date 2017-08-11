using UnityEngine;
using System.Collections;

public class VoxelModel1Manager : MonoBehaviour {
	public GameObject[] Models = new GameObject[5];
	private GameObject SelectModel;
	public string[] AniName = new string[5];
	private int ModelNumber = 0;
	private int AniNumber = 0;

	void Start () {
		SelectModel = Models[0];

		for(int i = 1; i < Models.Length; ++i) {
			OnTrackingLost(Models[i]);
		}
	}

	void OnModelButtonNext() {
		if(SelectModel.GetComponent<Animation>().IsPlaying("walk")) {
			if(ModelNumber == 4) return;
			ModelNumber++;
			AniNumber = 0;
			OnTrackingLost(SelectModel);
			SelectModel = Models[ModelNumber];
			OnTrackingFound(SelectModel);
			PlayAnimation();
		} else {
			AniNumber++;
			PlayAnimation();
		}
	}
	
	void OnModelButtonPrev() {
		if(SelectModel.GetComponent<Animation>().IsPlaying("Take 001")) {
			if(ModelNumber == 0) return;
			ModelNumber--;
			AniNumber = 4;
			OnTrackingLost(SelectModel);
			SelectModel = Models[ModelNumber];
			OnTrackingFound(SelectModel);
			PlayAnimation();
		} else {
			AniNumber--;
			PlayAnimation();
		}
	}

	private void OnTrackingFound(GameObject _obj)
	{
		Renderer[] rendererComponents = _obj.GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = _obj.GetComponentsInChildren<Collider>(true);
		
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = true;
		}
		
		foreach (Collider component in colliderComponents)
		{
			component.enabled = true;
		}
	}
	
	
	private void OnTrackingLost(GameObject _obj)
	{
		Renderer[] rendererComponents = _obj.GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = _obj.GetComponentsInChildren<Collider>(true);
		
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = false;
		}
		
		foreach (Collider component in colliderComponents)
		{
			component.enabled = false;
		}
	}

	void PlayAnimation() {
		SelectModel.GetComponent<Animation>().Play(AniName[AniNumber]);
	}

	void OnGUI() {
		if (GUI.Button(new Rect(160,40,25,25), "<")) {
			OnModelButtonPrev();
		}

		if(SelectModel != null) {
			if (GUI.Button(new Rect(190,25,200,25), SelectModel.name)) {
				Debug.Log("Clicked");
			}
		}

		if(SelectModel != null) {
			if (GUI.Button(new Rect(190,60,200,25), AniName[AniNumber])) {
				Debug.Log("Clicked");
			}
		}
		
		if (GUI.Button(new Rect(395,40,25,25), ">")) {
			OnModelButtonNext();
		}
	}
}
