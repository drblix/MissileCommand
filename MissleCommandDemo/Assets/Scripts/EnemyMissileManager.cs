using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enemyMissile;
    [SerializeField] 
    private GameObject _splitterMissile;
    [SerializeField]
    private GameObject _enemyUFO;

    public int missilesToSpawn = 6;

    private float _enemyMissileSpeed = 1f;

    public bool gameOver = false;

    [Header("Spawn Config")]
    [SerializeField] [Tooltip("Y coordinate that missles spawn at")] private float _yCoordinateSpawn;
    [SerializeField] [Tooltip("Max X coordinate value for spawning")] private float _maxXRange;
    [SerializeField] [Tooltip("Min X coordinate value for spawning")] private float _minXRange;


    private void Awake()
    {
        StartCoroutine(SpawnEnemyMissiles());
        StartCoroutine(SpawnUFO());
    }

    private IEnumerator SpawnEnemyMissiles()
    {
        for (int i = 0; i < missilesToSpawn; i++)
        {
            int _spawnSplitterInt = Mathf.RoundToInt(Random.Range(1f, 4f)); // 25% chance of splitter spawning

            yield return new WaitForSeconds(Random.Range(2.5f, 5f));

            if (_spawnSplitterInt == 4)
            {
                GameObject newMissle = Instantiate(_splitterMissile, new Vector2(Random.Range(_minXRange, _maxXRange), _yCoordinateSpawn), Quaternion.identity);
                newMissle.GetComponent<SplitterMissileScript>().missileSpeed = _enemyMissileSpeed;
            }
            else
            {
                GameObject newMissle = Instantiate(_enemyMissile, new Vector2(Random.Range(_minXRange, _maxXRange), _yCoordinateSpawn), Quaternion.identity);
                newMissle.GetComponent<EnemyMissileScript>().missileSpeed = _enemyMissileSpeed;
            }
        }


    }

    private IEnumerator SpawnUFO()
    {
        while (true)
        {
            int num = Mathf.RoundToInt(Random.Range(1f, 2f)); // generates random number

            yield return new WaitForSeconds(Mathf.RoundToInt(Random.Range(12f, 25f))); // waits between 20 or 30 seconds (random)

            if (num == 2) // instantiates ufo if num == 2
            {
                Instantiate(_enemyUFO, Vector2.zero, Quaternion.identity);
            }
        }
    }

    public void EnemyMissileDestroyed()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.currentMissiles -= 1;
    }
}
