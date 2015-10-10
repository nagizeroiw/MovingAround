using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour {

	[HideInInspector]
	static public Main instance = null;

	public Magnet magnet;
	public GameObject foodTile;
	public int newFoodTime;

	[HideInInspector]
	public int score1, score2;

	private int frameNumber;
	private GameObject ball;
	private Text text;
	private Man[] players;
	

	// Use this for initialization
	void Start() {
		if (instance == null)
			instance = this;
		else
			Destroy(this);

		score1 = 0;
		score2 = 0;
		ball = GameObject.FindGameObjectWithTag("Ball");
		text = GameObject.Find("Text").GetComponent<Text>();
		players = FindObjectsOfType<Man>();
	}

	// when (1 - number) scored
	public void lose(int number) {
		if (number == 1)
			score2++;
		else if (number == 2)
			score1++;
		// Debug.Log(score2.ToString() + ":" + score1.ToString());
		text.text = score2.ToString() + " : " + score1.ToString();

		ball.transform.position = new Vector3(0f, 0f, 0f);
		ball.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
		
		StartCoroutine(Countdown(number));
	}

	private IEnumerator Countdown(int losenumber) {

		foreach (Man player in players) {
			player.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
			player.Wait();
		}

		ball.SetActive(false);
		text.text = "3";
		yield return new WaitForSeconds(1f);
		text.text = "2";
		yield return new WaitForSeconds(1f);
		text.text = "1";
		yield return new WaitForSeconds(1f);
		text.text = score2.ToString() + " : " + score1.ToString();
		ball.SetActive(true);
		if (losenumber == 1) {
			ball.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.5f, 0f, 0f);
		}
		else if (losenumber == 2) {
			ball.GetComponent<Rigidbody2D>().velocity = new Vector3(1.5f, 0f, 0f);
		}
		foreach (Man player in players) {
			player.Wake();
		}

		yield return null;
	}

	// Update is called once per frame
	void Update() {
		if (newFoodTime != 0) {
			frameNumber++;
			if (frameNumber % newFoodTime == 0) {
				
				Instantiate(foodTile, new Vector3(Random.Range(-9, 9), Random.Range(4, 5), 0f), Quaternion.identity);
				Instantiate(foodTile, new Vector3(Random.Range(-9, 9), Random.Range(-5, -4), 0f), Quaternion.identity);
			}
		}
	}
	
}
