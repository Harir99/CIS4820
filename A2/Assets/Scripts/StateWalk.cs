using UnityEngine;
using System.Collections;

public class StateWalk : State
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

        float distance = Vector3.Distance(character.transform.position, target.position);
        if (distance <= character.attackDistance)
        {
            character.ChangeState(new StateAttack());
        }
        else if (distance > character.GetWalkRadius())
        {
            character.ChangeState(new StateRun());
        }
        else
        {
            character.GetAnimation().CrossFade("walk", 0.2f);
            character.MoveTowards(target.position, character.walkSpeed);
        }
    }
}

