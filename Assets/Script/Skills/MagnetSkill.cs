using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagnetSkill : Skill {
	
	private Magnet magInstance;
	private int _number;
	private static string[] playerName = {"", "Player1", "Player2"};

	public MagnetSkill(int number): base("Magnet", 60f, 10f) {
		_number = number;
		// mag = new Magnet();
	}

	public override void Use() {
		base.Use();
		Vector3 manpos = GameObject.FindGameObjectWithTag(playerName[_number]).transform.position;
		manpos.x += 0.4f * GameObject.FindGameObjectWithTag(playerName[_number]).GetComponent<Rigidbody2D>().velocity.x;
		manpos.y += 0.4f * GameObject.FindGameObjectWithTag(playerName[_number]).GetComponent<Rigidbody2D>().velocity.y;

		magInstance = GameObject.Instantiate(Main.instance.magnet, manpos, Quaternion.identity) as Magnet;
	}

	public override void End() {
		base.End();

		GameObject.Destroy(magInstance.gameObject);
	}

}
