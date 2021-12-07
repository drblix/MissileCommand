using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterMissileScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyMissileNorm;
    [SerializeField] 
    private GameObject _enemyExplosion;
    [SerializeField] 
    private AudioClip[] _explosionSFX;

    private GameObject _playerCamera;
    private GameObject _missileTrail;

    private SpriteRenderer _playerRenderer;
    public float missileSpeed;

    private readonly Vector2[] _friendlyCityLocations = new Vector2[]
    {
        new Vector2(3.26f, -4f), // City 01
        new Vector2(5.781f, -3.855f), // City 02
        new Vector2(-5.343f, -4.105f), // City 03
        new Vector2(-7.837f, -3.992f) // City 04
    };

    private Vector2 _cityTargetVector;
    private int _randomCityNum;

    // Variables ^^^

    private void Awake()
    {
        _playerCamera = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        _randomCityNum = Mathf.RoundToInt(Random.Range(0f, _friendlyCityLocations.Length - 1));
        _cityTargetVector = _friendlyCityLocations[_randomCityNum];
        _playerRenderer = GetComponent<SpriteRenderer>();
        _missileTrail = transform.Find("Trail").gameObject;

        if (missileSpeed <= 0f)
        {
            missileSpeed = 1f;
        }

        StartCoroutine(ChangeColor());
        StartCoroutine(WaitTillSplit());
    }


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _cityTargetVector, missileSpeed * Time.deltaTime);
    }

    public void DestroyMissile()
    {
        SpriteRenderer _tempRender = Instantiate(_enemyExplosion, transform.position, Quaternion.identity).GetComponent<SpriteRenderer>();
        _tempRender.color = new Color(255f, 255f, 60f);
        Destroy(gameObject);
    }

    private IEnumerator WaitTillSplit()
    {
        yield return new WaitForSeconds(Random.Range(5f, 7f));
        SplitMissile();
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            _playerRenderer.color = new Color(255f, 255f, 255f);

            yield return new WaitForSeconds(0.5f);

            _playerRenderer.color = new Color(255f, 255f, 60f);

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SplitMissile()
    {
        _missileTrail.transform.parent = null;
        _missileTrail.GetComponent<TrailRenderer>().autodestruct = true;
        Instantiate(_enemyMissileNorm, transform.position, Quaternion.identity);
        Instantiate(_enemyMissileNorm, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
