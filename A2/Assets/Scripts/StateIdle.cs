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

        // Check if health is low, run away
        AIStatus status = character.GetComponent<AIStatus>();
        if (status != null && status.GetHealth() < 10)
        {
            //Debug.Log("NPC health low: " + status.GetHealth() + ". Switching to RunAway!");
            character.ChangeState(new StateRunAway());
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