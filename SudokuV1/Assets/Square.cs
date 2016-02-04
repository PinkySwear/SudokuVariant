using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	public bool active;
	private TextMesh tm;
	private Renderer myRenderer;
	public int number;
	public bool wrong;

	// Use this for initialization
	void Start () {
		active = false;
		tm = this.gameObject.GetComponentInChildren<TextMesh> ();
		myRenderer = this.gameObject.GetComponent<Renderer> ();
		tm.text = "";
		number = 0;
	}

	// Update is called once per frame
	void Update () {
		if (number == 0) {
			tm.text = "";
		}
		else {
			tm.text = number.ToString ();
		}

		myRenderer.material.color = Color.white;
		if (wrong) {
			myRenderer.material.color = Color.red;
		}

		if (active) {
			myRenderer.material.color = Color.black;
			Debug.Log ("I am active");
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				number = 1;
				active = false;
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				number = 2;
				active = false;
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				number = 3;
				active = false;
			} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				number = 4;
				active = false;
			} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
				number = 5;
				active = false;
			} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
				number = 6;
				active = false;
			} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
				number = 7;
				active = false;
			} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
				number = 8;
				active = false;
			} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
				number = 9;
				active = false;
			} else if (Input.GetKeyDown (KeyCode.Backspace)) {
				number = 0;
				active = false;
			} else {
				return;
			}
			
		}
	}

	void OnMouseDown() {
		Debug.Log ("on mouse down");
		active = true;
	}
}
