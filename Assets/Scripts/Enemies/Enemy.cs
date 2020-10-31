using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    public float attackDuration;
    public float attackDelay;
    public float attackRange;
    public float health;
    public float maxHealth;
    public float damage;
    public float chasingTime;

    protected AttackDirection direction;

    public bool agressive;

    protected abstract void UpdateAI();
    protected abstract void OnLostTrack();
    protected abstract void OnDeath();

    public abstract void OnHit(GameObject player, float damage);

    public abstract void OnEnterWarningRange(GameObject player);
    public abstract void OnEnterReactionRange(GameObject player);
    public abstract void OnStayReactionRange(GameObject player);

    public abstract void OnExitWarningRange(GameObject player);
    public abstract void OnExitReactionRange(GameObject player);
    public abstract void OnEnterAttackTrigger(AttackDirection playerDirection);
    public abstract void OnExitAttackTrigger(AttackDirection playerDirection);
}
