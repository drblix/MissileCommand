using System.Collections;
using UnityEngine;

public class UFOScript : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyMissile;
    [SerializeField]
    private GameObject _ufoExplosion;
    [SerializeField]
    private float _ufoSpeed;

    private GameObject _missileSpawn;

    [SerializeField]
    private Vector2 _startingPos;
    [SerializeField]
    private Vector2 _endingPos;
    private Vector2 _currentTrans;

    private float _waitTime;

    private void Awake()
    {
        _missileSpawn = this.transform.Find("MissileSpawn").gameObject;
        transform.position = _startingPos;
        StartCoroutine(SpawnMissle());
    }

    private void Update()
    {
        _currentTrans = transform.position;

        transform.position = Vector2.MoveTowards(transform.position, _endingPos, _ufoSpeed * Time.deltaTime);

        if (_currentTrans == _endingPos)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator SpawnMissle()
    {
        while (true)
        {
            _waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(_waitTime);

            Instantiate(_enemyMissile, _missileSpawn.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMissle"))
        {
            GameObject _newExplosion = Instantiate(_ufoExplosion, transform.position, Quaternion.identity);
            _newExplosion.transform.localScale = new Vector3(2f, 2f, 2f);
            Destroy(this.gameObject);
        }
    }
}
