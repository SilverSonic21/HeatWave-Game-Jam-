using UnityEngine;

public class RedEnemies : MonoBehaviour
{
    public float maxHealth = 15f;
    private float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
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
        
        Destroy(gameObject);
        
    }
}
