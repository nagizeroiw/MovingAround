using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Man : MonoBehaviour {

	public float maxV = 8f;
	public float A = 30f;
	public int number;

	private Animator animator;
	
	private Rigidbody2D rb2D;
	
	private Text myTextBoard;
	private static string[] textBoardName = { "", "Dash1", "Dash2" };
	
	private Skill[] skills;

	private Item item;

	private CommandHolder cmdHolder;

	private enum STATE {
		NORMAL,
		MOVING,
		EATING,
		WAITING
	};
	private STATE state;

	// Use this for initialization
	void Start () {
		
		// get animator, rb
		animator = GetComponent<Animator>();
		rb2D = this.GetComponent<Rigidbody2D>();

		// init state
		state = STATE.NORMAL;

		// init skills
		Skill dash = new DashSkill();
		Skill magnet = new MagnetSkill(number);
		skills = new Skill[] { dash, magnet };

		foreach (Skill skl in skills) {
			skl.Init();
		}

		item = null;

		// get skill board
		myTextBoard = GameObject.Find(textBoardName[number]).GetComponent<Text>();

		// init cmdHolder
		cmdHolder = new CommandHolderKeyboard(number);

	}
	
	// Update is called once per frame
	void Update () {
		
		if (state != STATE.WAITING) { // can`t move when waiting

			// get commands from player
			cmdHolder.GiveAllCommands();
			
			// get all skills updated: cd and time
			UpdateSkills(); 
			
			// handle move and skill commands
			Move();
			Skill();
			Item();
		}
	}

	// set state to waiting
	public void Wait() {
		state = STATE.WAITING;
	}

	// set state to normal
	public void Wake() {
		if (state == STATE.WAITING)
			state = STATE.NORMAL;
	}

	// handle skill commands
	private void Skill() {
		int sklCmd = cmdHolder.GetSkillCmd();

		// activate the skill if avalible
		if (sklCmd != -1 && skills[sklCmd].Avalible()) {
			animator.SetInteger("skillState", sklCmd);
			skills[sklCmd].Use();
		}

		AdjustTextBoard();
	}

	private void Item() {

		// use item
		if (cmdHolder.GetItemCmd() && item != null) {
			item.Use();
			item = null;
		}
	}

	// update skills: cd and time
	private void UpdateSkills() {
		// update dash
		foreach (Skill skl in skills) {
			
			// if an skill ends, turn the animator to normal state
			if (skl.Update())
				animator.SetInteger("skillState", -1);
		}
		AdjustTextBoard();
	}
	
	// adjust display on skill board
	private void AdjustTextBoard() {
		myTextBoard.text = "";
        foreach (Skill skl in skills) {
			myTextBoard.text += skl._display + "\n";
		}
		myTextBoard.text += "\n\n";
        if (item != null) {
			myTextBoard.text += item.GetType().ToString();
        }
		else {
			myTextBoard.text += "No Item";
        }
	}

	// handle move commands
	private void Move() {

		// dash, maybe it`s better to pull this out?
		float useV = maxV;
		if (skills[0].IsOn()) // dash
			useV *= 1.5f;

		// get ax and ay
		Vector2 axis = cmdHolder.GetAxisCmd();
		float horizontal = axis.x, vertical = axis.y;

		// give force
		rb2D.AddForce(new Vector2(horizontal * A, vertical * A), ForceMode2D.Force);

		// limitation on speed
		if (rb2D.velocity.SqrMagnitude() > useV) {
			rb2D.velocity *= (useV / rb2D.velocity.SqrMagnitude());
        }

		// state detect
		if (rb2D.velocity.SqrMagnitude() == 0f) {
			state = STATE.NORMAL;
		}
		else {
			state = STATE.MOVING;
		}

		// end
	}

	// eat food. Now no use
	void OnCollisionEnter2D(Collision2D c2D) {

		// Debug.Log(c2D.gameObject.name +  " Collision!");

		// get item
		Item tmp = c2D.collider.gameObject.GetComponent<Item>();
        if (tmp != null) {
			tmp.GetComponent<Collider2D>().isTrigger = true;
			// can only hold one Item
			if (item == null) {
				item = tmp;
                item.SetOwner(this);
				AdjustTextBoard();
            }
		}
		
	}
}
