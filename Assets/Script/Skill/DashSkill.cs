using UnityEngine;
using System.Collections;

public class DashSkill : Skill {

	public DashSkill() {
		_name = "Dash";
		_maxTime = 2f;
		_maxCD = 40f;
	}
}
