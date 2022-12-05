using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour 
{
    
    public enum SpawnState{spawning, waiting, counting};


    //allows us to cahnge values of instances of this class in the unity inspector
    [System.Serializable]
    public class Wave
    {
        public string name;
        //public GameObject enemyPrefab;
        public Transform enemyPrefab;

        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    private SpawnState state = SpawnState.counting;

    public float timeBetweenWaves = 5.0f;
    public float waveCountdown = 0f;

    private float searchTimer = 1f;

    public TextMeshProUGUI waveCountdownText;
    public Transform spawner;
    void Start()
    {
        waveCountdown = timeBetweenWaves;

    }

    void Update()
    {
        if(state == SpawnState.waiting)
        {
            if(EnemyIsAlive() == false)
            {
                //Begin new wave
                Debug.Log("wave completed");
                return;
            }
            else
            {
                return;
            }
        }
        if(waveCountdown <= 0)
        {
            if( state != SpawnState.spawning)
            {
                //start spawning

                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }

        else
        {
            waveCountdown -= Time.deltaTime;
            waveCountdownText.text = "WaveCountdown: " + Mathf.Round(waveCountdown).ToString();
        }
    }
    
    //checks if enemy is alive
    //could be inefficient depending on number of enemies though
    bool EnemyIsAlive()
    {
        searchTimer -= Time.deltaTime;
        if(searchTimer <= 0f)
        {
            searchTimer = 1f;
          if(GameObject.FindGameObjectWithTag("Enemy") == null)
          {
            return false;
          }
        }

        return true;
    }

    // uses to spawn waves
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("spawning wave: " + _wave.name);
        // changes the state of 'state' to 'spawning'
        state = SpawnState.spawning;
        
        //takes waves array and loops through it
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds( 1f/_wave.rate);
        }

        // changes the state to waiting for the player to kill the enemies in the wave
        state = SpawnState.waiting;

        // closes the Enumerator(stops enemies spawning)
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //spawn ENEMY
        Instantiate(_enemy, spawner.transform.position, spawner.transform.rotation);
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }
}