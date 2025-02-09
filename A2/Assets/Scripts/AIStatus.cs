using UnityEngine;
using System.Collections;

public class AIStatus : MonoBehaviour {
	
	private float	health = 10.0f;

	//Uncomment if we need to apply a power-up to the AI
	//private float	maxHealth = 10.0f;

	private bool dead = false;
	private AIController aiController;
	
	void Start(){
		aiController = GetComponent< AIController>();
	}
	
	public bool isAlive() {return !dead;}	
	
	public void ApplyDamage(float damage){
		//Debug.Log("Enemy NPC damage " + damage);
		health -= damage;
		if (health <= 0 && !aiController.IsDead)
		{
			dead = true;
			health = 0;
			print("***********Dead!*************");
			aiController.IsDead = true;

		}
	}
}
