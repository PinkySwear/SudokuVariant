using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Square[,] grid;
	public GameObject[] subGrids;
	public int size;

	// Use this for initialization
	void Start () {
		grid = new Square [size, size];

		//very hacky way of filling the grid
		//use overlap spheres to find which square is at each position
		//may change later
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				grid[i,j] = (Physics.OverlapSphere(new Vector3((-1 * size + 1 + 2f*i), (-1 * size + 1 + 2f*j), 0f), 0.01f))[0].gameObject.GetComponent<Square>();
//				grid [i, j].active = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
//		grid [1, 2].active = true;
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Debug.Log ("(" + i + ", " + j + ")");
				grid [i, j].wrong = false;
			}
		}

		//if all rules satisfied, and the board is full
		bool cols = checkCols();
		bool rows = checkRows();
		bool subs = checkSubs(size);
		if (cols && rows && subs && allFull ()) {
			Debug.Log ("YOU WIN");
		}
	}

	bool checkCols() {
		bool correct = true;
		for (int i = 0; i < size; i++) {
			bool[] checker = new bool[9];
			for (int j = 0; j < size; j++) {
				Square curr = grid [i, j];
				if (curr.number == 0) {
					//donothing
				} else if (checker [(curr.number - 1)]) {
					for (int k = 0; k < size; k++) {
						grid [i, k].wrong = true;
					}
					correct = false;break;
				} else {
					checker [(curr.number - 1)] = true;
				}
			}
		}
		return correct;
	}

	bool checkRows() {
		bool correct = true;
		for (int j = 0; j < size; j++) {
			bool[] checker = new bool[9];
			for (int i = 0; i < size; i++) {
				Square curr = grid [i, j];
				if (curr.number == 0) {
					//donothing
				} else if (checker [(curr.number - 1)]) {
					for (int k = 0; k < size; k++) {
						grid [k, j].wrong = true;
					}
					correct = false;break;
				} else {
					checker [(curr.number - 1)] = true;
				}
			}
		}
		return correct;
	}

	bool checkSubs(int aSize) {
		bool correct = true;
		switch (aSize) {
		case 3:
			{
				bool[] checker = new bool[9];
				for (int i = 0; i < 3; i++) {
					for (int j = 0; j < 3; j++) {
//					bool[] checker = new bool[9];
						Square curr = grid [i, j];
						if (curr.number == 0) {
							//donothing
						} else if (checker [curr.number - 1]) {
							for (int k = 0; k < 3; k++) {
								for (int l = 0; l < 3; l++) {
									grid [k, l].wrong = true;
								}
							}
							correct = false;
							break;
						} else {
							checker [(curr.number - 1)] = true;
						}
					}
				}
				break;
			}
		case 4:
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					int xcoord = i * 2;
					int ycoord = j * 2;

					bool[] checker = new bool[9];
					Square[] gridSquares = new Square[size];
					int index = 0;
					for (int k = 0; k < 2; k++) {
						for (int l = 0; l < 2; l++) {
							gridSquares [index] = grid [xcoord + k, ycoord + l];
							index++;
						}
					}

					for (int m = 0; m < size; m++) {
						Square curr = gridSquares [m];
						if (curr.number == 0) {
							//donothing
						} else if (checker [(curr.number - 1)]) {
							for (int n = 0; n < size; n++) {
								gridSquares [n].wrong = true;
							}
							correct = false;
							break;
						} else {
							checker [(curr.number - 1)] = true;
						}
					}
				}
			}
			break;
		case 9: 
			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 3; j++) {
					int xcoord = 1 + i * 3;
					int ycoord = 1 + j * 3;

					bool[] checker = new bool[9];
					Square[] gridSquares = new Square[9];
					int index = 0;
					for (int k = -1; k < 2; k++) {
						for (int l = -1; l < 2; l++) {
							gridSquares [index] = grid [xcoord + k, ycoord + l];
							index++;
						}
					}

					for (int m = 0; m < 9; m++) {
						Square curr = gridSquares [m];
						if (curr.number == 0) {
							//donothing
						} else if (checker [(curr.number - 1)]) {
							for (int n = 0; n < 9; n++) {
								gridSquares [n].wrong = true;
							}
							correct = false;
							break;
						} else {
							checker [(curr.number - 1)] = true;
						}
					}
				}
			}
			break;
		default: 
			correct = false;
			break;
		}
		return correct;
	}

	bool allFull() {
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				if (grid [i, j].number == 0) {
					return false;
				}
			}
		}
		return true;
	}
}
