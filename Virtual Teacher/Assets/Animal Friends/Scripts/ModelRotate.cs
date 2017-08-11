using UnityEngine;
using System.Collections;

public class ModelRotate : MonoBehaviour {
	void Update () {
		transform.Rotate(Vector3.down * Time.deltaTime * 60);
	}
}
