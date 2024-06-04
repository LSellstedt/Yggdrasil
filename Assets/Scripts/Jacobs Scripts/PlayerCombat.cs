using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int enemyAttackDamage = 1;

    public float attackRate;
    private float nextAttackTime = 0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Ensure you have an Animator component on the player
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerAttack, this.transform.position);
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Determine the direction of the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Default to facing right if no input
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            // Horizontal attack
            if (horizontal > 0)
            {
                animator.SetTrigger("AttackRight");
            }
            else
            {
                animator.SetTrigger("AttackLeft");
            }
        }
        else
        {
            // Vertical attack
            if (vertical > 0)
            {
                animator.SetTrigger("AttackUp");
            }
            else
            {
                animator.SetTrigger("AttackDown");
            }
        }

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); 

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy_Damage>().EnemyTakeDamage(enemyAttackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}