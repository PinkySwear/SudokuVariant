﻿using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	public bool active;
	private TextMesh tm;
	private Renderer myRenderer;
	public int number;
	public bool wrong;
	public bool mouseOver;
	public bool isFixed;
	public float offset;
//	private Vector3 originalPos;

	// Use this for initialization
	void Start () {
		active = false;
		tm = this.gameObject.GetComponentInChildren<TextMesh> ();
		myRenderer = this.gameObject.GetComponent<Renderer> ();
		if (isFixed) {
			myRenderer.material.color = Color.gray;
		}
		tm.text = "";

		if(!isFixed) {
			number = 0;
		}
		offset = Random.value*2f;
//		originalPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		//random bullshit to make the cubes float around a bit
//		transform.position = new Vector3(transform.position.x+0.005f*Mathf.Sin(Time.time+offset), transform.position.y+0.005f*Mathf.Sin(Time.time+offset), originalPos.z);


		if (Input.GetMouseButtonDown(0) && !mouseOver) {
			active = false;
		}
		if (number == 0) {
			tm.text = "";
		}
		else {
			tm.text = number.ToString ();
		}
		if (!isFixed) {
			if (wrong) {
				myRenderer.material.color = new Color (1f, 0.5f, 0.5f);
			} else {
				myRenderer.material.color = Color.white;
			}
		} else {
			if (wrong) {
				myRenderer.material.color = new Color(0.65f, 0.45f, 0.45f);
			} else {
				myRenderer.material.color = new Color(0.75f, 0.75f, 0.75f);
			}

		}

		if (active) {
			myRenderer.material.color = Color.yellow;
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
		if (!isFixed) {
			active = true;

		}
	}

	void OnMouseEnter() {
		mouseOver = true;
	}

	void OnMouseExit() {
		mouseOver = false;
	}

}