using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandHolderKeyboard : CommandHolder {

	private int _number;

	private static KeyCode[] axis1 = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow };
	private static KeyCode[] axis2 = { KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.A };
	private static KeyCode[][] axis = { new KeyCode[] { }, axis1, axis2 };

	// 0: Dash 1: magnet
	private static KeyCode[][] skillTile = { new KeyCode[] { },
											 new KeyCode[] { KeyCode.Comma, KeyCode.Period  }, // player 1
											 new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2 }}; // player 2

	private static KeyCode[] useItem = { KeyCode.P, KeyCode.Slash, KeyCode.Alpha3 };

	private static COMMAND[] skillCommands = { COMMAND.SKILL_0, COMMAND.SKILL_1 };


	public CommandHolderKeyboard(int number) {
		_number = number;
	}

	public override void GiveAllCommands() {

		// move
		if (Input.GetKey(axis[_number][0]))
			GiveCommand(COMMAND.MOVE_UP);
		else if (Input.GetKey(axis[_number][1]))
			GiveCommand(COMMAND.MOVE_DOWN);

		if (Input.GetKey(axis[_number][2]))
			GiveCommand(COMMAND.MOVE_RIGHT);
		else if (Input.GetKey(axis[_number][3]))
			GiveCommand(COMMAND.MOVE_LEFT);

		// skills
		// expand foreach for efficiency
		if (Input.GetKey(skillTile[_number][0])) {
			// activate
			GiveCommand(skillCommands[0]);
		}
		if (Input.GetKey(skillTile[_number][1])) {
			// activate
			GiveCommand(skillCommands[1]);
		}

		// use item
		if (Input.GetKey(useItem[_number])) {
			GiveCommand(COMMAND.USE_ITEM);
		}
	}
}