using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillManager : MonoBehaviour {

	public int tileMatrixSize = 0;
	private int controlTileMatrixSize = 3;
	public GameObject[,] visualTiles;
	public TreadmillTile[,] controlTiles;
	public GameObject player;
	public GameObject visualTile;
	public GameObject visualTile2;
	public GameObject visualTile3;
	public GameObject visualTile4;
	public GameObject fire;
	public TreadmillTile controlTile;
	public int centralTileX = 0;
	public int centralTileZ = 0;
	public int activatedTileX;
	public int activatedTileZ;
	//public GameObject[,] tempMatrix;

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () {
		visualTiles = new GameObject[tileMatrixSize, tileMatrixSize];
		//tempMatrix  = new GameObject[tileMatrixSize, tileMatrixSize];
		controlTiles = new TreadmillTile[controlTileMatrixSize,controlTileMatrixSize];
		controlTile.GetComponent<BoxCollider> ().size = new Vector3 (visualTile.GetComponent<MeshRenderer> ().bounds.size.x, 10, visualTile.GetComponent<MeshRenderer> ().bounds.size.z);
		for (int i = 0; i < visualTiles.GetLength (0); i++) {
			for (int j = 0; j < visualTiles.GetLength (1); j++) {
				if (j % 2 == 0) {
					if (i % 2 == 0) {
						visualTiles [i, j] = Instantiate (visualTile3);
					} else {
						visualTiles [i, j] = Instantiate (visualTile4);
					}
				} else {
					if (i % 2 == 0) {
						visualTiles [i, j] = Instantiate (visualTile);
					} else {
						visualTiles [i, j] = Instantiate (visualTile2);
					}
				}
				if (i == 0 & j == 0) {
					fire.transform.position = visualTiles [i, j].transform.position;
					fire.transform.parent = visualTiles [i, j].transform;
				}
				//visualTiles [i, j] = Instantiate (visualTile, new Vector3 (visualTile.GetComponent<MeshRenderer>().bounds.size.x * j, 0, visualTile.GetComponent<MeshRenderer>().bounds.size.z * -i), Quaternion.identity).gameObject;
				visualTiles [i, j].transform.parent = transform;
				visualTiles [i, j].name = "VisualTile [" + i + "," + j + "]";
				/*if ((i + j) % 2 == 0) {
					visualTiles [i, j].GetComponent<MeshRenderer> ().material.color = Color.red;
				} else {
					visualTiles [i, j].GetComponent<MeshRenderer> ().material.color = Color.white;
				} */
			}
		}
		RepositionTiles ();
		for (int i = 0; i < controlTiles.GetLength(0); i++)	{
			for (int j = 0; j < controlTiles.GetLength (1); j++) {
				controlTiles [i, j] = Instantiate (controlTile, new Vector3 (controlTile.GetComponent<BoxCollider>().size.x * ((visualTiles.GetLength (0))/2 -1 + j), 0, controlTile.GetComponent<BoxCollider>().size.z * (-(visualTiles.GetLength (1))/2 + 1 -i)), Quaternion.identity).GetComponent<TreadmillTile> ();
				controlTiles [i, j].transform.parent = transform;
				controlTiles [i,j].name = "ControlTile [" + i + "," + j + "]";
			}
		}
		centralTileX = 1;
		centralTileZ = 1;
		player.transform.position = new Vector3 (visualTiles [visualTiles.GetLength(0)/2, visualTiles.GetLength(1)/2].transform.position.x, 2, visualTiles [visualTiles.GetLength(0)/2, visualTiles.GetLength(1)/2].transform.position.z);
		controlTiles [centralTileX, centralTileZ].OnPlayerExit += TreadmillCycle;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TreadmillCycle()
	{
		FindNewActiveTile ();
		UpdateMatrix ();
		RepositionTiles ();
	}

	void FindNewActiveTile()
	{
		for (int i = 0; i < controlTiles.GetLength (0); i++) {
			for (int j = 0; j < controlTiles.GetLength (1); j++) {
				if (controlTiles [i, j].GetIsActiveTile ()) {
					activatedTileX = j;
					activatedTileZ = i;
					}
			}
		}
	}

	void UpdateMatrix()
	{
		GameObject[,] tempMatrix = new GameObject[tileMatrixSize, tileMatrixSize];
		if (activatedTileZ != centralTileZ) {
			if (activatedTileZ < centralTileZ) {
				for (int i = 0; i < visualTiles.GetLength (0); i++) {
					for (int j = 0; j < visualTiles.GetLength (1); j++) {
						int offsetNumber = i;
						offsetNumber--;
						if (offsetNumber < 0) {
							offsetNumber = visualTiles.GetLength (0) - 1;
						}
						tempMatrix [i, j] = visualTiles [offsetNumber, j];
					}
				}
				/*tempMatrix [0, 1] = visualTiles [2, 1];
				tempMatrix [0, 2] = visualTiles [2, 2];
				tempMatrix [1, 0] = visualTiles [0, 0];
				tempMatrix [1, 1] = visualTiles [0, 1];
				tempMatrix [1, 2] = visualTiles [0, 2];
				tempMatrix [2, 0] = visualTiles [1, 0];
				tempMatrix [2, 1] = visualTiles [1, 1];
				tempMatrix [2, 2] = visualTiles [1, 2];*/
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - visualTile.GetComponent<MeshRenderer>().bounds.size.z);
			} else if (activatedTileZ > centralTileZ) {
				for (int i = 0; i < visualTiles.GetLength (0); i++) {
					for (int j = 0; j < visualTiles.GetLength (1); j++) {
						int offsetNumber = i;
						offsetNumber++;
						if (offsetNumber > visualTiles.GetLength (0) - 1) {
							offsetNumber = 0;
						}
						tempMatrix [i, j] = visualTiles [offsetNumber, j];
					}
				}
				/*tempMatrix [0, 0] = visualTiles [0, 1];
				tempMatrix [0, 1] = visualTiles [0, 2];
				tempMatrix [0, 2] = visualTiles [0, 0];
				tempMatrix [1, 0] = visualTiles [1, 1];
				tempMatrix [1, 1] = visualTiles [1, 2];
				tempMatrix [1, 2] = visualTiles [1, 0];
				tempMatrix [2, 0] = visualTiles [2, 1];
				tempMatrix [2, 1] = visualTiles [2, 2];
				tempMatrix [2, 2] = visualTiles [2, 0];*/
				/*tempMatrix [0, 0] = visualTiles [1, 0];
				tempMatrix [0, 1] = visualTiles [1, 1];
				tempMatrix [0, 2] = visualTiles [1, 2];
				tempMatrix [1, 0] = visualTiles [2, 0];
				tempMatrix [1, 1] = visualTiles [2, 1];
				tempMatrix [1, 2] = visualTiles [2, 2];
				tempMatrix [2, 0] = visualTiles [0, 0];
				tempMatrix [2, 1] = visualTiles [0, 1];
				tempMatrix [2, 2] = visualTiles [0, 2];*/
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + visualTile.GetComponent<MeshRenderer>().bounds.size.z);
			}
		} else if (activatedTileZ == centralTileZ) {
			if (activatedTileX < centralTileX) {
				for (int i = 0; i < visualTiles.GetLength (0); i++) {
					for (int j = 0; j < visualTiles.GetLength (1); j++) {
						int offsetNumber = j;
						offsetNumber--;
						if (offsetNumber < 0) {
							offsetNumber = visualTiles.GetLength (1) - 1;
						}
						tempMatrix [i, j] = visualTiles [i, offsetNumber];
					}
				}
				player.transform.position = new Vector3 (player.transform.position.x + visualTile.GetComponent<MeshRenderer>().bounds.size.x, player.transform.position.y, player.transform.position.z);
			} else if (activatedTileX > centralTileX) {
				for (int i = 0; i < visualTiles.GetLength (0); i++) {
					for (int j = 0; j < visualTiles.GetLength (1); j++) {
						int offsetNumber = j;
						offsetNumber++;
						if (offsetNumber > visualTiles.GetLength (1) - 1) {
							offsetNumber = 0;
						}
						tempMatrix [i, j] = visualTiles [i, offsetNumber];
					}
				}
				player.transform.position = new Vector3 (player.transform.position.x - visualTile.GetComponent<MeshRenderer>().bounds.size.x, player.transform.position.y, player.transform.position.z);
			}
		}
		visualTiles = tempMatrix;
	}

	void RepositionTiles()
	{
		for (int i = 0; i < visualTiles.GetLength (0); i++) {
			for (int j = 0; j < visualTiles.GetLength (1); j++) {
				visualTiles [i, j].transform.position = new Vector3 (visualTiles [i, j].GetComponent<MeshRenderer>().bounds.size.x * j, 0, visualTiles [i, j].GetComponent<MeshRenderer>().bounds.size.z * -i);
			}
		}	
	}

}
