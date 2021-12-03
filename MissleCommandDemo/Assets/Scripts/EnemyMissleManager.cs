using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissleManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyMissile;

    public int missilesToSpawn = 8;

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
            yield return new WaitForSeconds(Random.Range(2.5f, 5f));

            Instantiate(_enemyMissile, new Vector2(Random.Range(_minXRange, _maxXRange), _yCoordinateSpawn), Quaternion.identity);
        }
    }
}
