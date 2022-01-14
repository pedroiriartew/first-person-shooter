using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Explosive : Enemy
{

    private int deathID = Animator.StringToHash("DeathTrigger");//Esto sirve para que el código no esté evaluando todo el tiempo si el string es el correcto.

    private bool stopMovement;

    private void Awake()
    {
        hp = 100;
        speed = 5f;
        dmg = 200;
    }

    private void Start()
    {
        referenciaBase = FindObjectOfType<BaseScript>();
        referenciaGM = FindObjectOfType<Game_Manager>();

        stopMovement = false;

        normalizedDirection = (referenciaBase.transform.position - transform.position).normalized;

        animReference = GetComponent<Animator>();

        deathSound = GetComponent<AudioSource>();
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
            animReference.SetTrigger(deathID);
            Audio_Manager._instance.PlayExplosionSound();
            referenciaBase.ReceiveDamage(dmg);
            Destroy(gameObject, 0.3f);
        }
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

    protected override void Die()
    {
        animReference.SetTrigger(deathID);
        deathSound.Play();
        Destroy(gameObject, 1f);
    }
}
