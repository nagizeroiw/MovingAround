using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		float size = Random.Range(1, 5) * 0.02f;
		transform.localScale = new Vector3(size, size, size);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
