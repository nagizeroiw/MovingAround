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
		magInstance = GameObject.Instantiate(Main.instance.magnet, manpos, Quaternion.identity) as Magnet;
	}

	public override void End() {
		base.End();

		GameObject.Destroy(magInstance.gameObject);
	}

}
