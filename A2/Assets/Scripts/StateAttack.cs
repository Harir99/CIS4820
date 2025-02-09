using UnityEngine;
using System.Collections;

public class StateAttack : State
{
    //private float attackCooldown = 1.0f;
    private float lastAttackTime = 0f;

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
        if (distance > character.attackDistance)
        {
            character.ChangeState(new StateWalk());
        }
        else
        {
            character.GetAnimation().CrossFade("attack", 0.1f);
            //if (Time.time - lastAttackTime >= attackCooldown)
            //{
            //    character.GetPlayerStatus().ApplyDamage(character.attackValue);
            //    lastAttackTime = Time.time;
            //}
            if (Time.time - lastAttackTime >= character.attackSpeed)
            {
                character.GetPlayerStatus().ApplyDamage(character.attackValue);
                lastAttackTime = Time.time;
            }
        }
    }
}
