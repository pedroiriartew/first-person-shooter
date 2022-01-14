using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Normal : Enemy
{

    private int attackID = Animator.StringToHash("AttackTrigger");
    private int deathID = Animator.StringToHash("DeathTrigger");

    bool stopMovement;

    private void Awake()
    {
        hp = 200;
        speed = 4f;
        dmg = 50;

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
        if(collision.gameObject.tag == "Base")
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
        Destroy(gameObject, 1.3f);
    }

    public override void ReceiveDmg(int dmgReceived)
    {
        hp -= dmgReceived;        
        if(hp<=0)
        {
            referenciaGM.DeadEnemies();
            stopMovement = true;
            Die();
        }
    }

}
