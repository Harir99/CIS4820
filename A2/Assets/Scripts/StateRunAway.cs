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

        Transform target = character.GetTarget();
        if (target == null) return;

        // Check if NPC health is below 10%
        if (character.GetComponent<AIStatus>().GetHealth() < 10)
        {
            character.GetAnimation().CrossFade("run", 0.2f);

            // Run away from the player
            Vector3 runDirection = (character.transform.position - target.position).normalized;
            character.MoveTowards(character.transform.position + runDirection * character.runSpeed, character.runSpeed);
        }
        else
        {
            character.ChangeState(new StateIdle()); // if not running away, revert to idle state
        }
    }
}
