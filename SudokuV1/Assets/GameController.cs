using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Square[,] grid;
	public GameObject[] subGrids;


	// Use this for initialization
	void Start () {
		grid = new Square [9, 9];
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				grid[i,j] = (Physics.OverlapSphere(new Vector3((-8f + 2f*i), (-8f + 2f*j), 0f), 0.01f))[0].gameObject.GetComponent<Square>();
//				grid [i, j].active = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
//		grid [1, 2].active = true;
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				grid [i, j].wrong = false;
			}
		}
		checkCols();
		checkRows();
//		checkSubs ();
	}

	void checkCols() {
		for (int i = 0; i < 9; i++) {
			bool[] checker = new bool[9];
			for (int j = 0; j < 9; j++) {
				Square curr = grid [i, j];
				if (curr.number == 0) {
					//donothing
				} else if (checker [(curr.number - 1)]) {
					for (int k = 0; k < 9; k++) {
						grid [i, k].wrong = true;
					}
					break;
				} else {
					checker [(curr.number - 1)] = true;
				}
//				for (int k = 0; k < 9; k++) {
//					grid [i, k].wrong = false;
//				}
			}
		}
	}

	void checkRows() {
		for (int j = 0; j < 9; j++) {
			bool[] checker = new bool[9];
			for (int i = 0; i < 9; i++) {
				Square curr = grid [i, j];
				if (curr.number == 0) {
					//donothing
				} else if (checker [(curr.number - 1)]) {
					for (int k = 0; k < 9; k++) {
						grid [k, j].wrong = true;
					}
					break;
				} else {
					checker [(curr.number - 1)] = true;
				}
//				for (int k = 0; k < 9; k++) {
//					grid [k, j].wrong = false;
//				}
			}
		}
	}
//
//	void checkSubs() {
//		
//	}
}
