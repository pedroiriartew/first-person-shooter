using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class Enemy : MonoBehaviour
{
    protected int hp, dmg;
    protected float speed;

    protected BaseScript referenciaBase = null;
    protected Game_Manager referenciaGM = null;

    protected Vector3 normalizedDirection;

    protected Animator animReference;
    protected AudioSource deathSound;

    public abstract void MoveToAttack();
    protected abstract void Die();
    public abstract void ReceiveDmg(int dmgReceived);
}