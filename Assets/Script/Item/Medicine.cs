using UnityEngine;
using System.Collections;

public class Medicine : TimeEffectItem {

	public Medicine() {
		effectTime = 2f;
		_life = _maxLife = 7f;
	}

	public override void Use() {
		if (state == STATE.IN_HAND) {
			state = STATE.USED;
		}
		// for sure
		if (_owner != null) {

			_owner.maxV *= 1.5f;
			_owner.A *= 5f;

			StartEffect();
		}
	}

	protected override void EndEffect() {

		Debug.Log("End");

		// give back effect
		if (_owner != null) {
			_owner.maxV /= 1.55f;
			_owner.A /= 5f;
		}
		base.EndEffect();
	}
}
