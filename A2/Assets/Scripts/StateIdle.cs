using UnityEngine;
public class StateIdle : State
{
    public override void Execute(AIController character)
    {
        if (character.IsDead)
        {
            character.ChangeState(new StateDead());
            return;
        }

        if (character.EnemySeen())
        {
            float distance = Vector3.Distance(character.transform.position, character.Target.position);

            if (distance <= character.attackDistance)
            {
                character.ChangeState(new StateAttack());
            }
            else if (distance <= character.walkDistance)
            {
                character.ChangeState(new StateWalk());
            }
            else
            {
                character.ChangeState(new StateRun());
            }
            return;
        }

        character.BeIdle();
    }
}