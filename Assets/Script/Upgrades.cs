using Unity.VisualScripting;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public Bullet Bull;
    public PlayerControls Player;
    public RedEnemies Enemies;
    public bool multishot = false;
    public bool piercing = false;
    [SerializeField] private int cost = 1;
    
    [SerializeField] private float dmgUP = 2;
    [SerializeField] private int turnUP = 5;
    [SerializeField] private int payUP = 2;
    [SerializeField] private float RoFUP = .1f;


    //Basic Upgrades
    private void damageUp()
    {

        Bull.damage += dmgUP;
        Player.gold -= cost;
    }

    private void turnSpeedUP()
    {

        Player.turnSpeed += turnUP;
        Player.gold -= cost;
    }

    private void FireRateUP()
    {

        Player.fireRate -= RoFUP;
        Player.gold -= cost;
    }

    //Advanced
    private void MultiShot()
    {

        multishot = true;
        Player.gold -= cost;
    }

    private void CashUP()
    {

        Enemies.wallet *= payUP;
        Player.gold -= cost;
    }

    private void Piercer()
    {

        piercing = true;
        Player.gold -= cost;
    }

}
