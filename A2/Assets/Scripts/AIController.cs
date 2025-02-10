using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
    public float fieldOfView = 180.0f;

    public float attackDistance = 2.0f;
    public float walkDistance = 5.0f;
    public float sightRadius = 20.0f;

    private float attackSpeed = 4.0f;
    private float gravity = 50.0f;
    private float attackValue = 5.0f;
    private float lastAttackTime = 0f;

    private CharacterController controller;
    public PlayerStatus playerStatus;
    public Transform Target { get; private set; }
    private Vector3 moveDirection = Vector3.zero;
    private State currentState;
    public Animation anim;

    private bool isControllable = true;
    private bool isDead = false;
    private bool deathStarted = false;

    public bool IsControllable
    {
        get { return isControllable; }
        set { isControllable = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animation>();
        GameObject tmp = GameObject.FindWithTag("Player");
        if (tmp != null)
        {
            Target = tmp.transform;
            playerStatus = tmp.GetComponent<PlayerStatus>();
        }
        ChangeState(new StateIdle());
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    public void BeDead()
    {
        if (!deathStarted)
        {
            anim.CrossFade("die", 0.1f);
            deathStarted = true;
            controller.enabled = false;
        }
        moveDirection = Vector3.zero;
        if (!anim.isPlaying)
        {
            gameObject.SetActive(false);
            IsControllable = false;
        }
    }
    public bool EnemySeen()
    {
        if (Target == null) return false;

        Vector3 directionToPlayer = (Target.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < fieldOfView * 0.5f)
        {
            if (Vector3.Distance(transform.position, Target.position) <= sightRadius)
            {
                return true;
            }
        }
        return false;
    }

    public void BeIdle()
    {
        anim.CrossFade("idle", 0.2f);
        moveDirection = Vector3.zero;
    }

    public void BeWalk()
    {
        anim.CrossFade("walk", 0.2f);
        Vector3 direction = (Target.position - transform.position).normalized;
        Move(direction, 2.0f);
    }

    public void BeRun()
    {
        anim.CrossFade("run", 0.2f);
        Vector3 direction = (Target.position - transform.position).normalized;
        Move(direction, 4.0f);
    }
    
    public void BeAttack()
    {
        anim.CrossFade("attack", 0.2f);
        if (Time.time - lastAttackTime >= 1.0f / attackSpeed)
        {
            playerStatus.ApplyDamage(attackValue);
            lastAttackTime = Time.time;
        }
    }

    public void BeRunAway()
    {
        anim.CrossFade("run", 0.2f);
        Vector3 direction = (transform.position - Target.position).normalized;
        direction += new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)); 
        Move(direction, 2.5f); 
    }

    public void Move(Vector3 direction, float speed)
    {

        moveDirection = direction * speed;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        // Force the y component of the position to 0
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;  // Force y to 0
        transform.position = currentPosition;

    }

    void Update()
    {
        if (!isControllable) return;
        AIStatus status = GetComponent<AIStatus>();
        if (status != null && status.GetHealth() < 10f)
        {
            //Debug.Log("NPC health low: " + status.GetHealth() + ". Switching to RunAway!");
            ChangeState(new StateRunAway());
        }

        currentState.Execute(this);
        
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