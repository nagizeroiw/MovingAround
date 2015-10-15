using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour {

	[HideInInspector]
	static public Main instance = null;

	// for skill
	public Magnet magnet;

	// for food (now useless)
	public GameObject foodTile;
	public int newFoodTime;

	// for item
	public Item[] ItemTile;
	public int newItemTime;

	[HideInInspector]
	public int score1, score2;
	
	// find some units of the game
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

		if (newItemTime != 0) {
			StartCoroutine(CreateItem());
		}
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

	// when goal
	private IEnumerator Countdown(int losenumber) {

		// let players wait
		foreach (Man player in players) {
			player.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
			player.Wait();
		}

		// remove the ball
		ball.SetActive(false);

		// count down when pausing
		text.text = score2.ToString() + " : " + score1.ToString() + "\n3";
		yield return new WaitForSeconds(1f);
		text.text = score2.ToString() + " : " + score1.ToString() + "\n2";
		yield return new WaitForSeconds(1f);
		text.text = score2.ToString() + " : " + score1.ToString() + "\n1";
		yield return new WaitForSeconds(1f);
		text.text = score2.ToString() + " : " + score1.ToString();

		// push back the ball with velocity
		ball.SetActive(true);
		if (losenumber == 1) {
			ball.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.5f, 0f, 0f);
		}
		else if (losenumber == 2) {
			ball.GetComponent<Rigidbody2D>().velocity = new Vector3(1.5f, 0f, 0f);
		}

		// wake up players
		foreach (Man player in players) {
			player.Wake();
		}

		yield return null;
	}

	IEnumerator CreateItem() {
		while (true) {
			int i = Random.Range(0, ItemTile.Length - 1);

			Instantiate(ItemTile[i], new Vector3(Random.Range(-9, 9), Random.Range(-1, 1), 0f), Quaternion.identity);

			yield return new WaitForSeconds(newItemTime);
		}
	}
}
