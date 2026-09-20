using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;



public class PlayerControls : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float turnSpeed = 100;
    [Header("Animation")]
    public Animator animation;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0f, 1f)]
    [SerializeField] public float fireRate = 0.5f;
    private float nextFire = 0f;
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip shootSound;

    //Hi, it's me Ethan 
    // variable to store the currency for upgrades
    public Upgrades upgrade;

    [Header("shop")]
    public int gold;
    public GameObject loseScreen;
    

    void Start()
    {
        currentHealth = maxHealth;
    }

  
    void Update()
    {
        HandleShooting();
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0,  -turnSpeed * Time.deltaTime, 0);        
        }
        //transform.Rotate(Vector3.right * 100 * Time.deltaTime);

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFire)
        {
            Shoot();
            audioSource.PlayOneShot(shootSound);
            nextFire = Time.time + fireRate;

            animation.SetBool("Fire", true);
        }
        else
        {
            animation.SetBool("Fire", false);
        }
    }

    private void Shoot()
    {
        
        if (upgrade.multishot == true)
        {
            
            GameObject bottle = Instantiate(bulletPrefab, firingPoint.position , firingPoint.rotation);
            Bullet b = bottle.GetComponent<Bullet>();
            Vector2 aimDir = transform.right;
            b.SetDirection(aimDir);

            
           // GameObject bottle = Instantiate(bulletPrefab, firingPoint.position , firingPoint.rotation);
           // Bullet b = bottle.GetComponent<Bullet>();
           // Vector2 aimDir = transform.right;
           // b.SetDirection(aimDir);
        }
        
        
        else{
            GameObject bottle = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            Bullet b = bottle.GetComponent<Bullet>();
            Vector2 aimDir = transform.right;
            b.SetDirection(aimDir);
        }
    }
    void Die()
    {
        if (loseScreen != null)
        { 
            loseScreen.SetActive(true);
            Time.timeScale = 0f;
            
        }
        Time.timeScale = 1;
    }
}
