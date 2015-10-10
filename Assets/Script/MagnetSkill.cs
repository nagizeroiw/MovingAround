using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagnetSkill : Skill {
	
	private Magnet magInstance;
	private static string[] playerName = {"", "Player1", "Player2"};

	public MagnetSkill(): base("Magnet", 60f, 10f) {
		// mag = new Magnet();
	}

	public override void Use(int number) {
		base.Use(number);
		Vector3 manpos = GameObject.FindGameObjectWithTag(playerName[number]).transform.position;
		manpos.x += 0.4f * GameObject.FindGameObjectWithTag(playerName[number]).GetComponent<Rigidbody2D>().velocity.x;
		manpos.y += 0.4f * GameObject.FindGameObjectWithTag(playerName[number]).GetComponent<Rigidbody2D>().velocity.y;

		magInstance = GameObject.Instantiate(Main.instance.magnet, manpos, Quaternion.identity) as Magnet;
	}

	public override void End() {
		base.End();

		GameObject.Destroy(magInstance.gameObject);
	}

}
