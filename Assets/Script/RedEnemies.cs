using UnityEngine;

public class RedEnemies : MonoBehaviour
{
    public float maxHealth = 15f;
    private float currentHealth;
    public float moveSpeed = 3f;   
    private Transform player;

    //Things i've added to the enemies (Ethan)
    public int wallet = 5;
    public PlayerControls Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        MoveTowardPlayer();
    }
    void MoveTowardPlayer()
    {
        if (player == null) return;

        
        Vector3 direction = (player.position - transform.position).normalized;

        
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Debug.Log("Died");
            Die();
            
        }
    }

    void Die()
    {
        Player.gold += wallet;
        Destroy(gameObject);
        
    }
}
