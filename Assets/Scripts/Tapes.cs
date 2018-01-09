using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapes {

	private Vector3[] playerPositions;
	private List<float> playerVocalizations;
	private int counter = 0;
	private bool canRecord = true;

	public Tapes()
	{
		playerPositions = new Vector3[10];
		playerVocalizations = new List<float> ();
	}

	public void RecordPosition (Vector3 position)
	{
		playerPositions [counter] = position;
		counter++;
		if (counter == playerPositions.Length) {
			canRecord = false;
		}
	}

	public void RecordVoice (float timeStamp)
	{
		playerVocalizations.Add (timeStamp);
	}

	public bool IsRecordable()
	{
		return canRecord;
	}
}
