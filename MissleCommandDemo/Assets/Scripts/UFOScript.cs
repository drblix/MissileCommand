using System.Collections;
using UnityEngine;

public class UFOScript : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyMissile;
    [SerializeField]
    private GameObject _idleSFXContainer;
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

    private GameObject _playerCamera;
    private GameObject _newContainer;

    private float _waitTime;

    private void Awake()
    {
        _playerCamera = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        _missileSpawn = this.transform.Find("MissileSpawn").gameObject;
        transform.position = _startingPos;

        _newContainer = Instantiate(_idleSFXContainer, _playerCamera.transform.position, Quaternion.identity);

        StartCoroutine(SpawnMissle());
    }

    private void Update()
    {
        _currentTrans = transform.position;

        transform.position = Vector2.MoveTowards(transform.position, _endingPos, _ufoSpeed * Time.deltaTime);

        if (_currentTrans == _endingPos)
        {
            Destroy(_newContainer);
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
            Destroy(_newContainer);
            Destroy(this.gameObject);
        }
    }
}
