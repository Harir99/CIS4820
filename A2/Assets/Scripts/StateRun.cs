using UnityEngine;
using System.Collections;

public class StateRun : State
{
    public override void Execute(AIController character)
    {
        if (character.IsDead)
        {
            character.ChangeState(new StateDead());
            return;
        }

        Transform player = character.Target;
        

        float distance = Vector3.Distance(character.transform.position, player.position);
        Vector3 targetPosition = player.position;
        targetPosition.y = 0;

        if (distance <= character.attackDistance)
        {
            character.ChangeState(new StateAttack());
            return;
        }
        else if (distance <= character.walkDistance)
        {
            character.ChangeState(new StateWalk());
            return;
        }

        character.BeRun();
    }
}
