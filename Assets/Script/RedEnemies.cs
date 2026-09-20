using UnityEngine;
using UnityEngine.UI;

public class RedEnemies : MonoBehaviour
{
    public float maxHealth = 15f;
    public float damage = 5f;
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

        private void OnTriggerEnter2D(Collider2D collision)
    {
        WallDamage wall = collision.GetComponent<WallDamage>();

        if (wall != null)
        {
            wall.TakeDamage(damage);
            Die();
        }

        PlayerControls player = collision.GetComponent<PlayerControls>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Die(); 
        }
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
