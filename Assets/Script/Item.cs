using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public string itemName;

	private float life;

	public enum STATE {
		ON_GROUND,
		ABOUT_TO_DISAPPER
	};

	public Item() {
		itemName = "Unknown";
		life = 0f;
	}
	
	public Item(string name) {
		itemName = name;
		life = 20f;
	}

	public void Update() {
		life -= Time.deltaTime;
		if (life <= 0f) {
			End();
		}
	}

	protected void End() {
		Destroy(this);
	}

}
