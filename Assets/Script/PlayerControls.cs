using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;



public class PlayerControls : MonoBehaviour
{
    public float turnSpeed = 100;
    [Header("Animation")]
    public Animator animation;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
    private float nextFire = 0f;

    void Start()
    {
        
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
            transform.Rotate(0, 0,  -turnSpeed * Time.deltaTime, 0);        }
        //transform.Rotate(Vector3.right * 100 * Time.deltaTime);

    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;

            //animation.SetBool("Throw", true);
        }
        else
        {
            //animation.SetBool("Throw", false);
        }
    }

    private void Shoot()
    {
        GameObject bottle = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        Bullet b = bottle.GetComponent<Bullet>();
        Vector2 aimDir = transform.right;
        b.SetDirection(aimDir);
        
    }
}
