using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiMovement : MonoBehaviour
{
    Transform player;
    public float moveSpeed;

    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    
    void Update()
    {
        // Debug.Log("enemy healt is" +  currentHealth);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
