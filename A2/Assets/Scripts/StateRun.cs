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

        Transform target = character.GetTarget();
        if (target == null) return;

        float distance = Vector3.Distance(character.transform.position, target.position);
        if (distance <= character.GetWalkRadius())
        {
            character.ChangeState(new StateWalk());
        }
        else
        {
            character.GetAnimation().CrossFade("run", 0.2f);
            character.MoveTowards(target.position, character.runSpeed);
        }
    }
}