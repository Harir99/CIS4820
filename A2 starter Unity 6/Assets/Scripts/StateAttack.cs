using UnityEngine;
using System.Collections;

public class StateAttack : State
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

        if (distance > character.attackDistance)
        {
            character.ChangeState(new StateWalk());
            return;
        }

        character.BeAttack();
    }
}
