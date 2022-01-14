using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Heavy : Enemy
{
    bool stopMovement;

    private int attackID = Animator.StringToHash("AttackTrigger");
    private int deathID = Animator.StringToHash("DeathTrigger");

    private void Awake()
    {
        hp = 300;
        speed = 3f;
        dmg = 100;

        deathSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        referenciaBase = FindObjectOfType<BaseScript>();
        referenciaGM = FindObjectOfType<Game_Manager>();
        stopMovement = false;


        normalizedDirection = (referenciaBase.transform.position - transform.position).normalized;

        animReference = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MoveToAttack();
    }

    public override void MoveToAttack()
    {
        if (!stopMovement)
        {
            transform.position += normalizedDirection * speed * Time.deltaTime;//Avanza en la dirección de referenciaBase (a una determinada velocidad) hasta que colisiona
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            stopMovement = true;
            animReference.SetTrigger(attackID);
        }
    }

    public void DealDamage()
    {
        referenciaBase.ReceiveDamage(dmg);
    }

    protected override void Die()
    {
        animReference.SetTrigger(deathID);
        deathSound.Play();
        Destroy(gameObject, 1f);
    }

    public override void ReceiveDmg(int dmgReceived)
    {
        hp -= dmgReceived;
        if (hp <= 0)
        {
            referenciaGM.DeadEnemies();
            stopMovement = true;
            Die();
        }
    }
}
