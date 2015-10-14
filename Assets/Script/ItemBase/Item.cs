using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

	public enum STATE {
		ON_GROUND,
		ABOUT_TO_DISAPPER,
		IN_HAND,
		USED
	}

	public string itemName;
	public STATE state;

	// when on ground
	private float _maxLife;
	private float _life;

	private Man _owner;

	public Item() {
		itemName = "Unknown";
		_life = _maxLife = 0f;
		state = STATE.ON_GROUND;
		_owner = null;
	}
	
	public Item(Man owner, string name) {

		itemName = name;
		_maxLife = 20f;
		_life = _maxLife;
		_owner = owner;

		state = STATE.ON_GROUND;
	}

	public abstract void Use();

	public void Update() {
		
		// has risk to disappear when laid on the grounds
		if (state == STATE.ON_GROUND || state == STATE.ABOUT_TO_DISAPPER) {
			_life -= Time.deltaTime;

			// change state to ABOUT_TO_DISAPPEAR when life is about to over
			if (_life <= _maxLife / 3f) {
				state = STATE.ABOUT_TO_DISAPPER;
			}
			if (_life <= 0f) {
				End();
			}
		}
	}

	protected void End() {
		Destroy(this);
	}

}
