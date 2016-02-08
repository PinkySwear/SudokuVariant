using UnityEngine;
using System.Collections;

public class plane : MonoBehaviour {
	public MovieTexture movTexture;

	public GameObject light;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().material.mainTexture = movTexture;
		movTexture.loop = true;
		movTexture.Play ();
	}
	
	// Update is called once per frame
	void Update () {
//			Vector3 screenPoint = Camera.main.WorldToScreenPoint (light.transform.position);
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 11f);
		Vector3 curPos = Camera.main.ScreenToWorldPoint (curScreenPoint);
		light.transform.position = new Vector3 (curPos.x, curPos.y, -9f);
	}
}
