using UnityEngine;
using System.Collections;

public class StateIdle : State {

	public override void Execute(AIController character){
		//If see and in range, attack
		//if (character.EnemySeen() && character.EnemyInRange()){
		//	character.ChangeState(new StateAttack());
		////If see and out of range, approach
		//}else if(character.EnemySeen() && !character.EnemyInRange()){
		//	character.ChangeState(new StateApproach());
		////Otherwise, idle away
		//}else{
		if (character.IsDead)
        {
			character.ChangeState(new StateDead());
		}
        else { 
			character.BeIdle();
		}
	}
}
