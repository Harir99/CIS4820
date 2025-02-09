using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public float			attackDistance = 2.0f;

	private float 			attackSpeed = 4.0f;
	private float 			gravity = 50.0f;
	private float			attackValue = 5.0f;
	
	private CharacterController controller;
	private PlayerStatus	playerStatus;
	private Transform		target;
	private Vector3			moveDirection = new Vector3(0,0,0);
	private State			currentState;
	private Animation		anim;

	private bool			isControllable = true;
	private bool			isDead = false;

	//This is a hack for legacy animation - we will do this properly later
	private bool			deathStarted = false; 


	public bool 	IsControllable {
		get {return isControllable;}
		set {isControllable = value;}
	}

	public bool IsDead
	{
		get { return isDead; }
		set { isDead = value; }
	}

	// Use this for initialization
	void Start () {
		controller = GetComponent< CharacterController>();
		anim = GetComponent<Animation>();
		GameObject tmp = GameObject.FindWithTag("Player");
		if (tmp != null){
			target=tmp.transform;
			playerStatus = tmp.GetComponent< PlayerStatus>();
		}

		ChangeState(new StateIdle());
	}
	
	public void ChangeState(State newState){
		currentState = newState;
	}
	
	public void BeDead(){
		//This is a hack for legacy animation - we will do this properly later
		if (!deathStarted)
		{
			anim.CrossFade("die", 0.1f);
			deathStarted = true;
			CharacterController controller = GetComponent<CharacterController>();
			controller.enabled = false;

		}
		moveDirection = new Vector3(0,0,0);

		if (!anim.isPlaying)
        {
			gameObject.SetActive(false);
			this.IsControllable = false;
		}
	}
	
	public void BeIdle(){
		anim.CrossFade("idle", 0.2f);	
		moveDirection = new Vector3(0,0,0);
	}
	
	void Update () {
		
		if (!isControllable)
			return;
		
		currentState.Execute(this);	
		moveDirection.y -= gravity*Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void OnDisable()
	{
		/*
		 * If you uncomment this, you need to somehow tell the PlayerController to update
		 * the enemies array by calling GameObject.FindGameObjectsWithTag("Enemy").
		 * Otherwise the reference to a desgtroyed GameObejct will still be in the enemies 
		 * array and you will get null pointer exceptions if you try to access it
		 */

		//Destroy(gameObject);

	}
}