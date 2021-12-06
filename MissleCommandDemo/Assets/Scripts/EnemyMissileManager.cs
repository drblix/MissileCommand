using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enemyMissile;
    [SerializeField] 
    private GameObject _splitterMissile;

    public int missilesToSpawn = 6;

    private float _enemyMissileSpeed = 1f;

    [Header("Spawn Config")]
    [SerializeField] [Tooltip("Y coordinate that missles spawn at")] private float _yCoordinateSpawn;
    [SerializeField] [Tooltip("Max X coordinate value for spawning")] private float _maxXRange;
    [SerializeField] [Tooltip("Min X coordinate value for spawning")] private float _minXRange;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyMissiles());
    }

    private IEnumerator SpawnEnemyMissiles()
    {
        for (int i = 0; i < missilesToSpawn; i++)
        {
            int _spawnSplitterInt = Mathf.RoundToInt(Random.Range(1f, 4f)); // 25% chance of splitter spawning

            Debug.Log(i);
            yield return new WaitForSeconds(Random.Range(2.5f, 5f));

            if (_spawnSplitterInt == 4)
            {
                Debug.Log("spawned splitter");
                Instantiate(_splitterMissile, new Vector2(Random.Range(_minXRange, _maxXRange), _yCoordinateSpawn), Quaternion.identity);
            }
            else
            {
                Instantiate(_enemyMissile, new Vector2(Random.Range(_minXRange, _maxXRange), _yCoordinateSpawn), Quaternion.identity);
            }
        }


    }
}
