using UnityEngine;

public class WallDamage : MonoBehaviour
{
    public float maxHealth = 50f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Wall took damage: " + amount);

        if (currentHealth <= 0)
        {
            Debug.Log("Wall destroyed!");
            Destroy(gameObject);
        }
    }
}
