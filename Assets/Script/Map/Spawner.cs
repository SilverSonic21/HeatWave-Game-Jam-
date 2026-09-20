using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{public float spawnRadius = 10f;
    public Transform spawnCenter;

    [Header("Wave Announcement")]
    public TMPro.TextMeshProUGUI waveAnnouncementText;
    public float fadeDuration = 1f;
    public float stayDuration = 1.5f;

    [System.Serializable]
    public class SpawnableEnemy
    {
        public string name;
        public GameObject enemyPrefab;
        public int count;
        public float interval = 1f;
        public int pointValue;
        public float minDistance = 5f;
    }

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<SpawnableEnemy> enemiesInWave = new List<SpawnableEnemy>();
    }

    public List<Wave> waves = new List<Wave>();

    public float timeBetweenWaves = 3f;

    [Header("Win Condition")]
    public GameObject winScreen;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool allWavesSpawned = false;

    private Transform player;

    void Start()
    {
        enemiesAlive = 0;
        allWavesSpawned = false;
        currentWaveIndex = 0;
        if (winScreen != null){
            winScreen.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];

        
            StartCoroutine(ShowWaveAnnouncement("Wave " + (currentWaveIndex + 1) + ": " + currentWave.waveName));

            // Wait before spawning
            yield return new WaitForSecondsRealtime(timeBetweenWaves);

            Debug.Log("Starting Wave " + (currentWaveIndex + 1) + ": " + currentWave.waveName);

            // Spawn the wave
            yield return StartCoroutine(SpawnWaveRandomized(currentWave));

            currentWaveIndex++;
        }

        allWavesSpawned = true;
        Debug.Log("All waves spawned.");
    }

    IEnumerator SpawnWaveRandomized(Wave wave)
    {
        List<SpawnableEnemy> spawnQueue = new List<SpawnableEnemy>();

        foreach (var enemy in wave.enemiesInWave)
        {
            for (int i = 0; i < enemy.count; i++)
            {
                spawnQueue.Add(enemy);
            }
        }

        Shuffle(spawnQueue);

        foreach (var enemy in spawnQueue)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSecondsRealtime(enemy.interval);
        }
    }

    void SpawnEnemy(SpawnableEnemy enemyData)
    {
        Vector3 spawnPos = GetRandomSpawnPosition();

        int attempts = 0;

        while (Vector3.Distance(player.position, spawnPos) < enemyData.minDistance && attempts < 20)
        {
            spawnPos = GetRandomSpawnPosition();
            attempts++;
        }

        GameObject spawnedEnemy = Instantiate(enemyData.enemyPrefab, spawnPos, Quaternion.identity);

        enemiesAlive++;

        DeathTracker tracker = spawnedEnemy.AddComponent<DeathTracker>();
        tracker.spawner = this;

        Debug.Log("Spawned enemy: " + enemyData.name + " at " + spawnPos);
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        Debug.Log("Enemies alive: " + enemiesAlive);

        if (allWavesSpawned && enemiesAlive <= 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");

        if (winScreen != null)
        { 
            winScreen.SetActive(true);
            Time.timeScale = 0f;
            
        }
        Time.timeScale = 1;

       
    }

    Vector3 GetRandomSpawnPosition()
    {
        if (!spawnCenter)
        {
            Debug.LogWarning("Spawn center must be assigned!");
            return Vector3.zero;
        }

        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = Mathf.Cos(angle) * spawnRadius;
        float y = Mathf.Sin(angle) * spawnRadius;

        return new Vector3(
            spawnCenter.position.x + x,
            spawnCenter.position.y + y,
            spawnCenter.position.z
        );
    }

    IEnumerator ShowWaveAnnouncement(string message)
    {
        if (waveAnnouncementText == null)
            yield break;

        waveAnnouncementText.text = message;

        Color c = waveAnnouncementText.color;
        c.a = 0f;
        waveAnnouncementText.color = c;
        waveAnnouncementText.gameObject.SetActive(true);

        // Fade in
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            c.a = alpha;
            waveAnnouncementText.color = c;
            yield return null;
        }

        // Stay visible
        yield return new WaitForSecondsRealtime(stayDuration);

        // Fade out
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            c.a = alpha;
            waveAnnouncementText.color = c;
            yield return null;
        }

        waveAnnouncementText.gameObject.SetActive(false);
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
