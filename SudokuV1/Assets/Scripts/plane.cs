using UnityEngine;
using System.Collections;

public class plane : MonoBehaviour {
	public MovieTexture movTexture;

	public GameObject light;
	public Renderer myRenderer;

//	public WebCamTexture webCam;


	// Use this for initialization
	void Start () {

		myRenderer = this.gameObject.GetComponent<Renderer> ();
		myRenderer.material.mainTexture = movTexture;
		movTexture.loop = true;
		movTexture.Play ();

		//testing webcam textures, ignore
//		webCam = new WebCamTexture ();
//		webCam.wrapMode = TextureWrapMode.Repeat;
//		myRenderer.material.mainTexture = webCam;
//		webCam.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 11f);
		Vector3 curPos = Camera.main.ScreenToWorldPoint (curScreenPoint);
		light.transform.position = new Vector3 (curPos.x, curPos.y, -9f);
	}
}
