using UnityEngine;
using System.Collections;

public class StateIdle : State {

	public override void Execute(AIController character){
		if (character.IsDead)
        {
			character.ChangeState(new StateDead());
		}
        else { 
			character.BeIdle();
		}
	}
}
