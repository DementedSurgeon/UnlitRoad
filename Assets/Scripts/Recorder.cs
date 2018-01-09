using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour {

	private Tapes[] recordingTapes;
	private int availableTape;
	private bool isRecording = false;
	public float recordTime;

	// Use this for initialization
	void Start () {
		recordingTapes = new Tapes[10];
		for (int i = 0; i < recordingTapes.Length; i++) {
			recordingTapes[i] = new Tapes();
		}
		CryForHelp.OnCryOut += Test;
		FindAvailableTape ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FindAvailableTape()
	{
		for (int i = 0; i < recordingTapes.Length; i++) {
			if (recordingTapes [i].IsRecordable ()) {
				availableTape = i;
				i = recordingTapes.Length;
			} else if (i == recordingTapes.Length - 1 && !recordingTapes [i].IsRecordable ()) {
				ShutDown ();
			}
		}
	}

	void ShutDown()
	{
		enabled = false;
	}

	void Test()
	{
		Debug.Log (availableTape);
	}

	IEnumerator Recording()
	{
		isRecording = true;
		yield return new WaitForSeconds (recordTime);
		isRecording = false;
	}
}
