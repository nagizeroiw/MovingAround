using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Man : MonoBehaviour {

	public float maxV = 8f;
	public int number;

	private Animator animator;
	
	private Rigidbody2D rb2D;
	
	private Text mySkillBoard;
	private static string[] textBoardName = { "", "Dash1", "Dash2" };
	
	private Skill[] skills;

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

		// get skill board
		mySkillBoard = GameObject.Find(textBoardName[number]).GetComponent<Text>();

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
			Skills();
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
	private void Skills() {
		int sklCmd = cmdHolder.GetSkillCmd();

		// activate the skill if avalible
		if (sklCmd != -1 && skills[sklCmd].Avalible()) {
			animator.SetInteger("skillState", sklCmd);
			skills[sklCmd].Use();
		}

		AdjustSkillBoard();
	}

	// update skills: cd and time
	private void UpdateSkills() {
		// update dash
		foreach (Skill skl in skills) {
			
			// if an skill ends, turn the animator to normal state
			if (skl.Update())
				animator.SetInteger("skillState", -1);
		}
		AdjustSkillBoard();
	}
	
	// adjust display on skill board
	private void AdjustSkillBoard() {
		mySkillBoard.text = "";
        foreach (Skill skl in skills) {
			mySkillBoard.text += skl._display + "\n";
		}
	}

	// handle move commands
	private void Move() {

		// dash, maybe it`s better to pull this out?
		float useV = maxV;
		if (skills[0].IsOn()) // dash
			useV *= 2f;

		// get ax and ay
		Vector2 axis = cmdHolder.GetAxisCmd();
		float horizontal = axis.x, vertical = axis.y;

		// give force
		rb2D.AddForce(new Vector2(horizontal * 30f, vertical * 30f), ForceMode2D.Force);

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

		// UnityEngine.Debug.Log("Collision!");
		
		// can eat at any time

		if (c2D.gameObject.CompareTag("Food")) { // eat food when not so big

			state = STATE.EATING;

			// UnityEngine.Debug.Log(lastSize.ToString());
			if (transform.localScale.x < 1.5f) { // if not so strong

				Destroy(c2D.gameObject);

				Vector3 lastSize = transform.localScale;
				
				lastSize.x += c2D.gameObject.transform.localScale.x / 2;
				lastSize.y += c2D.gameObject.transform.localScale.y / 2;
				lastSize.z += c2D.gameObject.transform.localScale.z / 2;

				rb2D.mass += rb2D.mass * (c2D.gameObject.transform.localScale.z / lastSize.z);
				maxV -= maxV * (c2D.gameObject.transform.localScale.z / lastSize.z) / 3;
				// UnityEngine.Debug.Log(lastSize.ToString());

				transform.localScale = lastSize;
			}

			state = STATE.NORMAL;
			
		}
	}
}
