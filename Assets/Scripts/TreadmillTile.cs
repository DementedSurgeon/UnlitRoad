using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillTile : MonoBehaviour {

	public bool isActiveTile = false;

	public delegate void TileDelegate ();

	public TileDelegate OnPlayerExit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider col)
	{
		if (col.transform.tag == "Player") {
			isActiveTile = false;
			if (OnPlayerExit != null) {
				Debug.Log ("Delegate called!", gameObject);
				OnPlayerExit();
			}

		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.transform.tag == "Player") {
			isActiveTile = true;
		}
	}

	public bool GetIsActiveTile()
	{
		return isActiveTile;
	}
}
