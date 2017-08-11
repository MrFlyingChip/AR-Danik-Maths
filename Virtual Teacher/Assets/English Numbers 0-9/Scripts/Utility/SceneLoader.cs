using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.

public class MySceneLoader  {

	/// <summary>
	/// Loads the scene Async.
	/// </summary>
	public static IEnumerator LoadSceneAsync (string sceneName)
	{
		if (!string.IsNullOrEmpty (sceneName)) {
			#if UNITY_PRO_LICENSE
			AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
			while (!async.isDone) {
				yield return 0;
			}
			#else
			SceneManager.LoadScene (sceneName);
			yield return 0;
			#endif
		}
	}
}
