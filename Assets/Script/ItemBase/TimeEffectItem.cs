using UnityEngine;
using System.Collections;

public abstract class TimeEffectItem : Item {

	protected float effectTime;
	protected bool onEffect;

	public TimeEffectItem() {
		effectTime = 10f;
		onEffect = false;
	}

	protected virtual void StartEffect() {
		if (onEffect == false) {
			onEffect = true;
			Debug.Log("start effect");
		}
	}

	public override void Update() {
		base.Update();
		if (onEffect) {
			effectTime -= Time.deltaTime;
			if (effectTime <= 0f) {
				EndEffect();
			}
			Debug.Log(effectTime.ToString());
		}
	}

	protected virtual void EndEffect() {
		onEffect = false;
		Destroy(this.gameObject);
	}
}
