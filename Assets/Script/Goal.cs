using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public int number;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D c2D) {
		if (c2D.gameObject.CompareTag("Ball"))
			Main.instance.lose(number);
	}
}
