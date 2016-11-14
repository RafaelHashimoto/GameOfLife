using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	public Sprite[] status;

	public static int cellType;

	private int minNeighborsA;
	private int maxNeighborsA;

	private int minNeighborsB;
	private int maxNeighborsB;

	public bool alive;	
	public int currentCellType; 
	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer> ();

		alive = false;
		cellType = 0;
		currentCellType = cellType;

		minNeighborsA = 2;
		maxNeighborsA = 3;
		minNeighborsB = 2;
		maxNeighborsB = 6;
	}

	void OnMouseDown(){
		if (Input.GetMouseButtonDown(0)) {
			CellInstantiate ();
		} 	
	}

	void CellInstantiate(){
		this.GetComponent<SpriteRenderer> ().sprite = status[cellType];
		currentCellType = cellType;
		if (cellType == 0) {
			alive = false;
		} else {
			alive = true;
		}
	}

	public void CellUpdate(){
		this.GetComponent<SpriteRenderer> ().sprite = status[currentCellType];
		if (currentCellType == 0) {
			alive = false;
		} else {
			alive = true;
		}
	}

	public bool IsAlive(){
		return alive;
	}

	public int CellType(){
		return currentCellType;
	}

	public void CellJudgment(int neighbour){
		if (alive) {
			//Se for célula do tipo A
			Debug.Log("Celula do tipo: "+ currentCellType);
			Debug.Log ("Vizinhos: " + neighbour);
			if (currentCellType == 1) {
				if (neighbour < minNeighborsA || neighbour > maxNeighborsA) {
					Debug.Log ("mata verde");
					currentCellType = 0;
					CellUpdate ();
				}
			}
			//Se for célula do tipo B
			if (currentCellType == 2) {
				// Se não tiver 2 nem 3 vizinhos vivos então checa se há condições de vida 
				if (neighbour < minNeighborsB || neighbour > maxNeighborsB) {
					currentCellType = 0;
					Debug.Log ("mata azul");
					CellUpdate ();
				} 
			}
		} else {
			// Se tiver 3 vizinhos, se torna uma célula A
			if (neighbour == 3) {
				currentCellType = 1;
				CellUpdate ();
			} 
			// Se tiver 4 vizinhos, se torna uma célula B
			if (neighbour == 4) {
				currentCellType = 2;
				CellUpdate ();
			}
		}

	}
}
