using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skill {
	public string _name;
	public float _CD;
	public float _time;
	protected float _maxCD;
	protected float _maxTime;
	public string _display;

	public Skill() {
		_name = "void";
		_maxTime = 0f;
		_maxCD = 0f;
	}

	public Skill(string name, float maxCD, float maxTime) {
		_name = name;
		_maxCD = maxCD;
		_maxTime = maxTime;
	}

	public virtual bool IsOn() {
		return _time > 0f;
	}

	public virtual bool Avalible() {
		return _CD == 0f;
	}

	public virtual void Init() {
		SetCD(0f);
		SetTime(0f);
	}

	public virtual void Use() {
		SetCD(_maxCD);
		SetTime(_maxTime);
	}

	public virtual void End() {

	}

	public virtual bool Update() {
		float fakeTime = _time;
		SetCD(_CD - Time.deltaTime);
		SetTime(_time - Time.deltaTime);
		if (fakeTime > 0f && _time == 0f) {
			End();
			return true;
		}
		else
			return false;
	}

	private void SetCD(float value) {
		_CD = value;
		if (_CD < 0f)
			_CD = 0f;
		AdjustDisplay();
	}

	private void SetTime(float value) {
		_time = value;
		if (_time < 0f)
			_time = 0f;
		AdjustDisplay();
	}

	protected virtual void AdjustDisplay() {
		_display = _name + "(" + Mathf.CeilToInt(_CD) + "): " + Mathf.CeilToInt(_time);
	}
}