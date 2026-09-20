using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 5;
    public float speed = 5f;
    public float life = 1f;
    public GameObject particles;
    public GameObject particles2;
    private bool hasHit = false;
    private Rigidbody2D rb;
    public float rotationSpeed = 500f;
    public AudioSource audioSource;
    public AudioClip breakSound;


    public Upgrades upgrade;
    


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, life);
    }
    
    private void Update()
    {
       // transform.position += Vector3.right * direction * speed * Time.deltaTime;
        
    }

    public void SetDirection(Vector2 dir)
    {
    
        rb.linearVelocity = dir * speed;    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            

            RedEnemies enemy = collision.gameObject.GetComponent<RedEnemies>();
            if (enemy != null)
            {
                hasHit = true;
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
            Boss boss = collision.GetComponent<Boss>();
            if (boss != null)
            {
                hasHit = true;
                boss.TakeDamage(damage);
                Destroy(gameObject);
            }
        
          

        
            //GameObject p1 = Instantiate(particles, transform.position, Quaternion.identity);
            //GameObject p2 = Instantiate(particles2, transform.position, Quaternion.identity);

            //p1.GetComponent<ParticleSystem>().Play();
            //p2.GetComponent<ParticleSystem>().Play();

            bool destroyed = false;
            int count = 0;
            while(destroyed == false)
            {
                if(count < 1 && upgrade.piercing)
                {
                  count += 1;
                  continue;
                }
                else
                {
                  destroyed = true;
                  Destroy(gameObject);
                }
                
            }
            
        } 
        //Debug.Log("Hit");
        
    }
}
