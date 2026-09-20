using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
public float maxHealth = 15f;
    public float damage = 5f;
    private float currentHealth;
    public float moveSpeed = 3f; 
    public Slider healthBar;  
    private Transform player;

    public int wallet = 5;
    public PlayerControls Player;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardPlayer();

        // Make the health bar face the camera (optional but looks good)
        if (healthBar != null)
        {
            healthBar.transform.rotation = Quaternion.identity;
        }
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
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Player.gold += wallet;
        Destroy(gameObject);
    }
}
