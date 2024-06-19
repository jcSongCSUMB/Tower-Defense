using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    
    //public Transform enemyPrefab;
    public Wave[] waves;
    
    public Transform spawnPoint;
    
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;
    
    private int waveNumber = 0;
    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }
        
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];
        
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.rate);
        }
        
        waveNumber++;

        if (waveNumber == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
        
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
