using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryForHelp : MonoBehaviour {

	public delegate void MyDelegate();
	public static event MyDelegate OnCryOut;
	private MeshRenderer mRenderer;
	private bool cryingOut = false;

	// Use this for initialization
	void Start () {
		mRenderer = gameObject.GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			CryOut ();
		}
	}

	IEnumerator ColorLerp()
	{
		cryingOut = true;
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime;
			mRenderer.material.color = Color.Lerp (Color.white, Color.red, t);
			yield return null;
		}
		cryingOut = false;
	}

	void CryOut()
	{
		if (!cryingOut) {
			if (OnCryOut != null) {
				OnCryOut ();
			}
			StartCoroutine (ColorLerp());
		}
	}
}
