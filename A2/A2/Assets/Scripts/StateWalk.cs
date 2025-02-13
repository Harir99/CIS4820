using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

public class StateWalk : State
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


        if (distance <= character.attackDistance)
        {
            character.ChangeState(new StateAttack());
            return;
        }

        character.BeWalk();

        //Vector3 currentPosition = character.transform.position;
        //currentPosition.y = 0;  // Force y to 0
        //character.transform.position = currentPosition;
    }
}
