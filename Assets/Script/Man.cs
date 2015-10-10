using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Man : MonoBehaviour {

	public float maxV = 8f;
	public int number;

	private Animator animator;
	
	private Rigidbody2D rb2D;
	
	private Text mySkillBoard;

	private static KeyCode[] axis1 = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow };
	private static KeyCode[] axis2 = { KeyCode.W,       KeyCode.S,         KeyCode.D,          KeyCode.A         };
	private static KeyCode[][] axis = { new KeyCode[]{ }, axis1, axis2 };

	// 0: Dash 1: magnet
	private Skill[] skills;
	private static KeyCode[][] skillTile = { new KeyCode[] { KeyCode.L, KeyCode.Comma,  KeyCode.X }, // Dash 
											 new KeyCode[] { KeyCode.L, KeyCode.Period, KeyCode.C } }; // Magnet
	private static string[] textBoardName = { "", "Dash1", "Dash2"};

	private enum STATE {
		NORMAL,
		MOVING,
		EATING,
		WAITING
	};
	private STATE state;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rb2D = this.GetComponent<Rigidbody2D>();
		state = STATE.NORMAL;

		Skill dash = new Skill("Dash", 30f, 5f);
		Skill magnet = new MagnetSkill();
		skills = new Skill[] { dash, magnet};

		mySkillBoard = GameObject.Find(textBoardName[number]).GetComponent<Text>(); ;

		foreach(Skill skl in skills) {
			skl.Init();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (state != STATE.WAITING) { // can`t move when waiting
			UpdateSkills(); // when waiting, skill parameters freeze
			Move();
			Skills();
		}
	}

	public void Wait() {
		state = STATE.WAITING;
	}

	public void Wake() {
		if (state == STATE.WAITING)
			state = STATE.NORMAL;
	}

	private void Skills() {
		for (int i = 0; i < skills.Length; i++) {
			if (Input.GetKey(skillTile[i][number]) && skills[i].Avalible()) {
				// activate
				skills[i].Use(number);
				animator.SetInteger("skillState", i);
			}
		}
		AdjustSkillBoard();
	}

	private void UpdateSkills() {
		// update dash
		foreach (Skill skl in skills) {
			if (skl.Update())
				animator.SetInteger("skillState", -1);
		}
		AdjustSkillBoard();
	}
	
	private void AdjustSkillBoard() {
		mySkillBoard.text = "";
        foreach (Skill skl in skills) {
			mySkillBoard.text += skl._display + "\n";
		}
	}

	private void Move() {

		// dash

		float useV = maxV;
		if (skills[0].IsOn()) // dash
			useV *= 2f;

		// get ax and ay

		int horizontal = 0, vertical = 0;

		if (Input.GetKey(axis[number][0]))
			vertical = 1;
		else if (Input.GetKey(axis[number][1]))
			vertical = -1;
		else
			vertical = 0;

		if (Input.GetKey(axis[number][2]))
			horizontal = 1;
		else if (Input.GetKey(axis[number][3]))
			horizontal = -1;
		else
			horizontal = 0;

		rb2D.AddForce(new Vector2(horizontal * 30f, vertical * 30f), ForceMode2D.Force);

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
