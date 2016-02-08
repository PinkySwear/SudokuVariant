using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Square[,] grid;
//	public GameObject[] subGrids;
	public int size;
	public bool suggesting;
	public int [] suggestions;
	private int suggestionIndex;
	public float refreshTime;
	private float timer;
//	public int[] counter;
//	public int totalNumsPlaced;
	public ArrayList pool;
	public int numSuggestions;
	public bool transparent;


	// Use this for initialization
	void Start () {
		if (suggesting) {
			pool = new ArrayList ();
			for (int i = 0; i < size * size; i++) {
				pool.Add (i / size + 1);
			}
		}
		grid = new Square [size, size];
//		counter = new int[10];

		//very hacky way of filling the grid
		//use overlap spheres to find which square is at each position
		//may change later

		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				grid[i,j] = (Physics.OverlapSphere(new Vector3((-1 * size + 1 + 2f*i), (-1 * size + 1 + 2f*j), 0f), 0.01f))[0].gameObject.GetComponent<Square>();
//				grid [i, j].active = true;
//				counter[grid[i,j].number]++;
				if (suggesting && grid [i, j].number != 0) {
					pool.Remove (grid [i, j].number);
				}
			}
		}
	
		if (suggesting) {
			Debug.Log ("GENERATING NEW SET");
			suggestions = generateNums ();
			string numBuf = "";
			for (int i = 0; i < numSuggestions; i++) {
				numBuf = numBuf + " | " + suggestions [i];
			}
			Debug.Log(numBuf + " | ");
			timer = 0f;
			suggestionIndex = 0;
		}


//		totalNumsPlaced = 0;
			
	}
	
	// Update is called once per frame
	void Update () {
		timer = timer + Time.deltaTime;
		if (suggesting) {
			if (timer > refreshTime || suggestionIndex >= suggestions.Length) {
				for (int i = suggestionIndex; i < suggestions.Length; i++) {
					pool.Add (suggestions [i]);
				}
				Debug.Log ("GENERATING NEW SET");
				suggestions = generateNums ();
				string numBuf = "";
				for (int i = 0; i < suggestions.Length; i++) {
					numBuf = numBuf + " | " + suggestions [i];
				}
				Debug.Log(numBuf + " | ");
				timer = 0f;
				suggestionIndex = 0;
			}
		}


//		grid [1, 2].active = true;
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
//				Debug.Log ("(" + i + ", " + j + ")");
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

	int[] generateNums() {
//		Debug.Log (pool.Count);
//		if (pool.Count < 10) {
//			string acc = "";
//			for (int i = 0; i < pool.Count; i++) {
//				acc = acc + ", " + pool [i]; 
//			}
//			Debug.Log (acc);
//		}
		int[] nums = new int[Mathf.Min(numSuggestions, pool.Count)];
//		string indacc = " | ";
		int pc = pool.Count;
		for (int i = 0; i < Mathf.Min(numSuggestions, pc) ; i++) {
			int ind = (int)(Random.value * pool.Count);
//			Debug.Log(ind);
//			while (counter [tempNum] >= 9) {
//				tempNum = (int)(Random.value * 9f) + 1;
//				Debug.Log(tempNum);
//			}
//			indacc = indacc + ind + " | ";
			nums [i] = (int) pool[ind];
			pool.RemoveAt (ind);
//			counter [nums [i]]++;
		}
//		Debug.Log (indacc);
		return nums;
	}

	public int useSuggestion() {
		suggestionIndex++;
//		totalNumsPlaced++;
//		pool.Remove (suggestions [suggestionIndex - 1]);
//		counter [suggestions[suggestionIndex - 1]]++;
		return suggestions [suggestionIndex - 1];
	}
}
