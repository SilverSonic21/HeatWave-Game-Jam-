using UnityEngine;

public class DeathTracker : MonoBehaviour
{
   public Spawner spawner;

    void OnDestroy()
    {
        DeathTracker tracker = GetComponent<DeathTracker>();
        if (spawner != null)
        {
            spawner.EnemyDied();
        }
    }
}
        

