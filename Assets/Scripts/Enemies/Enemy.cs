using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    public float attackSpeed;
    public float attackRange;
    public float health;
    public float maxHealth;
    public float chasingTime;

    public bool agressive;

    protected abstract void UpdateAI();
    protected abstract void OnHit(GameObject player);
    protected abstract void OnLostTrack();
    protected abstract void OnDeath();

    public abstract void OnEnterWarningRange(GameObject player);
    public abstract void OnEnterReactionRange(GameObject player);
    public abstract void OnStayReactionRange(GameObject player);

    public abstract void OnExitWarningRange(GameObject player);
    public abstract void OnExitReactionRange(GameObject player);
}
