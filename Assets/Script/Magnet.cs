using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

	private GameObject ball;
	private Vector2 pos;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag("Ball");
		pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 ballpos = ball.transform.position;
		ball.GetComponent<Rigidbody2D>().AddForce(0.2f * (pos - ballpos), ForceMode2D.Force);
	}
}
