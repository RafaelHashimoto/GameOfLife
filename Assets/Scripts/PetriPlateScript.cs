using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PetriPlateScript : MonoBehaviour {

	// as dimensões do array que representará o grid da placa de petri em x e y
	private int arrayDimensionX;
	private int arrayDimensionY;
	// Array de celulas que reprensentará a placa de petri
	public Cell[,] cells;
	// prefab de celula que será usado para instanciar as celulas na placa de petri
	public Cell cellPrefab;
	// posição tridimensional da placa de petri. O método Instantiate só aceita vetores de 3 dimensões
	private Vector3 cellPosition;

	private int neighbour;

	private int scanPlateLoop;

	private bool enableScanPlate;
	// Inicializando as variáveis e objetos

	public Text optionSelected;
	void Start () {

		// Placa de 20x20
		arrayDimensionX = 20;
		arrayDimensionY = 20;
		// Intanciando o array
		cells = new Cell[arrayDimensionY,arrayDimensionX];
		// Populando o array
		CreatePlate ();
		// Setando o Vector 3
		cellPosition.x = 0f;
		cellPosition.y = 0f;	
		cellPosition.z = 0f;

		neighbour = 0;
		scanPlateLoop = 1;
		enableScanPlate = false;


	}
	
	// Update is called once per frame
	void Update () {

		if (enableScanPlate) {
			if (scanPlateLoop > 0) {
				ScanPlate ();
				scanPlateLoop -= 1;
			} else {
				scanPlateLoop = 1;
				enableScanPlate = false;
			}
		}
		
	}
		
	/*IEnumerator TalkAsync(int y, int x){
		CheckNeighborhood (y, x);
		yield return new WaitForSeconds(100.0f);
	}*/

	void CreatePlate(){

		for (int y = 0; y < arrayDimensionY; y++){
			for (int x = 0; x < arrayDimensionX; x++) {
				cells[y,x] = Instantiate(cellPrefab, cellPosition, Quaternion.identity) as Cell;
				cellPosition.x += 1f;
			}
			cellPosition.x = 0;
			cellPosition.y += 1f;
		}

	}

	// Percorre o array de células checando seus vizinhos
	void ScanPlate(){
		for (int y = 0; y < arrayDimensionY; y++){
			for (int x = 0; x < arrayDimensionX; x++) {
				CheckNeighborhood (y, x);
				//StartCoroutine(TalkAsync (y, x));

			}
		}
	}

	// percorre os vizinhos da celula
	void CheckNeighborhood(int y, int x){
		// verifica os vizinhos começando de x - 1 e y -1 até x + 1 e y + 1
		// y => i
		// x => j
		//Time.timeScale = 0.1f;
		//Debug.Log("Célula na posição X:" + x + " Y: " + y);
		for (int i = y - 1; i <= y + 1; i++){
			for (int j = x - 1; j <= x + 1; j++) {
				// if para verificar se os index dos vizinhos estão dentro do array no eixo Y
				if (i >= 0 && i <= arrayDimensionY -1) {
					// if para verificar se os index dos vizinhos estão dentro do array no eixo X
					if (j >= 0 && j <= arrayDimensionX -1){
						
						// Se a celula vizinha estiver viva e garante que será comparado apenas com os seus vizinhos
						if (!(i == y && j == x ) && cells[i,j].IsAlive() ) {
							
							//Debug.Log ("Vizinho na posição X: " + j + " Y: " + i);
							neighbour += 1;
						}	
					}
				}
			}
		}
		Debug.Log ("Y:" + y + "X:" + x);
		cells [y,x].CellJudgment (neighbour);
		neighbour = 0;


	}

	public void DeadCell(){
		Cell.cellType = 0;
		optionSelected.text = "KILL";
	}

	public void CellTypeA(){
		Cell.cellType = 1;
		optionSelected.text = "CELL TYPE A";
	}

	public void CellTypeB(){
		Cell.cellType = 2;
		optionSelected.text = "CELL TYPE B";
	}

	public void StartScanPlate(){
		enableScanPlate = true;
		optionSelected.text = "SCAN";
	}

	public void Clear(){
		for (int y = 0; y < arrayDimensionY; y++){
			for (int x = 0; x < arrayDimensionX; x++) {
				cells [y, x].currentCellType = 0;
				cells [y, x].CellUpdate ();
			}
		}
	}

}

