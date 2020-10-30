using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Enemy : MonoBehaviour
{
    public float speed;
    public float attackSpeed;
    public float health;
    public float maxHealth;

    protected abstract void UpdateAI();
    protected abstract void OnHit(GameObject player);
    protected abstract void OnLostTrack();
    protected abstract void OnDeath();
}
