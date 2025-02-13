using UnityEngine;
using System.Collections;

public class StateRunAway : State
{
    public override void Execute(AIController character)
    {
        if (character.IsDead)
        {
            character.ChangeState(new StateDead());
            return;
        }

        AIStatus status = character.GetComponent<AIStatus>();
        if (status != null && status.GetHealth() >= 10)
        {
            character.ChangeState(new StateIdle());
            return;
        }

        character.BeRunAway();  
    }
}
