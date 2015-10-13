using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandHolder {
	public enum COMMAND {
		MOVE_UP,
		MOVE_DOWN,
		MOVE_RIGHT,
		MOVE_LEFT,
		SKILL_0, // DASH
		SKILL_1 // MAGNET
	}
	public static COMMAND[] CommandList = {
		COMMAND.MOVE_UP,
		COMMAND.MOVE_DOWN,
		COMMAND.MOVE_RIGHT,
		COMMAND.MOVE_LEFT,
		COMMAND.SKILL_0, // DASH
		COMMAND.SKILL_1 // MAGNET};
	};
    protected Dictionary<COMMAND, bool> cmdPannel;

	public CommandHolder() {
		Init();
	}

	public void Init() {
		cmdPannel = new Dictionary<COMMAND, bool>();

		foreach(COMMAND cmd in CommandList) {
			cmdPannel.Add(cmd, false);
		}

	}

	public virtual void GiveAllCommands() {
		// does nothing
	}

	protected void GiveCommand(COMMAND c) {
		cmdPannel[c] = true;
	}

	public int GetSkillCmd() {
		if (HasCommand(COMMAND.SKILL_0))
			return 0;
		else if (HasCommand(COMMAND.SKILL_1))
			return 1;
		else
			return -1;
	}
	
	public Vector2 GetAxisCmd() {
		int dx = 0, dy = 0;

		if (HasCommand(COMMAND.MOVE_UP))
			dy = 1;
		else if (HasCommand(COMMAND.MOVE_DOWN))
			dy = -1;

		if (HasCommand(COMMAND.MOVE_RIGHT))
			dx = 1;
		else if (HasCommand(COMMAND.MOVE_LEFT))
			dx = -1;

		return new Vector2(dx, dy);
	}

	// will clear the command when asked
	public bool HasCommand(COMMAND c) {
		if (cmdPannel[c]) {
			cmdPannel[c] = false;
			return true;
		}
		else
			return false;
	}
}
