using UnityEngine;
using System.Collections;

// this works! but why?
[RequireComponent(typeof(Item))]
public abstract class Item : MonoBehaviour {

	public enum STATE {
		ON_GROUND,
		ABOUT_TO_DISAPPER,
		IN_HAND,
		USED
	}
	
	[HideInInspector]
	public STATE state;

	// when on ground
	protected float _maxLife;
	protected float _life;

	protected Man _owner;

	public Item() {
		
		_life = _maxLife = 20f;
		state = STATE.ON_GROUND;
		_owner = null;

		state = STATE.ON_GROUND;
	}

	public void SetOwner(Man owner) {
		_owner = owner;
		state = STATE.IN_HAND;

		// hide
		gameObject.GetComponent<Collider2D>().isTrigger = true;
		gameObject.transform.position = new Vector3(0f, 0f, -100f);
	}

	// ! could activate only when in hand!
	public abstract void Use();

	public virtual void Update() {
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

	// call base.End() at last to destroy the item
	protected virtual void End() {
		Destroy(this.gameObject);
	}

}
