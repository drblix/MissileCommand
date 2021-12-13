using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileScript : MonoBehaviour
{
    [SerializeField] GameObject _enemyExplosion;
    [SerializeField] AudioClip[] _explosionSFX;
    private GameObject _playerCamera;

    SpriteRenderer _playerRenderer;
    public float missileSpeed;

    private readonly Vector2[] _friendlyCityLocations = new Vector2[]
    {
        new Vector2(3.26f, -4f), // City 01
        new Vector2(5.781f, -3.855f), // City 02
        new Vector2(-5.343f, -4.105f), // City 03
        new Vector2(-7.837f, -3.992f) // City 04
    };

    private Vector2 _cityTargetVector;
    

    private void Awake()
    {

        _cityTargetVector = ChooseTarget();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _playerCamera = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;

        if (missileSpeed <= 0f)
        {
            missileSpeed = 1f;
        }
        StartCoroutine(ChangeColor());
    }

    
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _cityTargetVector, missileSpeed * Time.deltaTime);
    }

    public void DestroyMissile(bool cityHit)
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

            if (FindObjectOfType<EnemyMissileManager>() != null)
            {
                FindObjectOfType<EnemyMissileManager>().EnemyMissileDestroyed();
            }

            FindObjectOfType<CitiesManager>().DestroyCity(cityNum);
            DestroyMissile(true);
        }
    }

    private Vector2 ChooseTarget() // Picks city target that isn't already destroyed
    {
        int randomCityNum = Mathf.RoundToInt(Random.Range(1, 5));
        bool canContinue = false;
        CitiesManager citiesManager = FindObjectOfType<CitiesManager>();

        while (!canContinue)
        {
            Debug.Log(randomCityNum);
            if (citiesManager.CheckCity(randomCityNum))
            {
                canContinue = true;
                break;
            }
            else
            {
                randomCityNum = Mathf.RoundToInt(Random.Range(1, 5));
            }
        }

        return randomCityNum switch // Returns thing
        {
            1 => _friendlyCityLocations[0],
            2 => _friendlyCityLocations[1],
            3 => _friendlyCityLocations[2],
            4 => _friendlyCityLocations[3],
            _ => _friendlyCityLocations[0],
        };
    }
}
