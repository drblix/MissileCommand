using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissleScript : MonoBehaviour
{
    [SerializeField] GameObject _enemyExplosion;
    [SerializeField] AudioClip[] _explosionSFX;
    [SerializeField] GameObject _playerCamera;

    SpriteRenderer _playerRenderer;
    public float _missleSpeed;

    private readonly Vector2[] _friendlyCityLocations = new Vector2[]
    {
        new Vector2(3.26f, -4f), // City 01
        new Vector2(5.781f, -3.855f), // City 02
        new Vector2(-5.343f, -4.105f), // City 03
        new Vector2(-7.837f, -3.992f) // City 04
    };

    private Vector2 _cityTargetVector;
    private int randomCityNum;
    

    private void Start()
    {
        randomCityNum = Random.Range(0, _friendlyCityLocations.Length);
        _cityTargetVector = _friendlyCityLocations[randomCityNum];
        _playerRenderer = GetComponent<SpriteRenderer>();
        if (_missleSpeed <= 0f)
        {
            _missleSpeed = 1f;
        }
        StartCoroutine(ChangeColor());
    }

    
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _cityTargetVector, _missleSpeed * Time.deltaTime);
    }

    public void DestroyMissle(bool cityHit)
    {
        Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
        if (cityHit)
        {
            AudioSource.PlayClipAtPoint(_explosionSFX[Random.Range(0, _explosionSFX.Length)], _playerCamera.transform.position);
        }
        Destroy(gameObject);
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            _playerRenderer.color = new Color(255f, 255f, 255f);

            yield return new WaitForSeconds(0.5f);

            _playerRenderer.color = new Color(255f, 0f, 0f);

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("City"))
        {
            int cityNum = 0;
            switch (collision.gameObject.name)
            {
                case "FriendlyCity01":
                    cityNum = 1;
                    break;

                case "FriendlyCity02":
                    cityNum = 2;
                    break;

                case "FriendlyCity03":
                    cityNum = 3;
                    break;

                case "FriendlyCity04":
                    cityNum = 4;
                    break;
            }

            FindObjectOfType<CitiesManager>().DestroyCity(cityNum);
            DestroyMissle(true);
        }
    }
}
