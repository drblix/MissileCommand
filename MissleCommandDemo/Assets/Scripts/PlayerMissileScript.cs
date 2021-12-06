using System.Collections;
using UnityEngine;

public class PlayerMissileScript : MonoBehaviour
{
    [SerializeField] 
    private GameObject _missileTrail;
    [SerializeField] 
    private GameObject _playerMissileExplosion;

    private GameObject _playerCamera;

    [SerializeField] AudioClip[] _explosionClips;

    SpriteRenderer _playerRenderer;

    public float missleSpeed;
    public Vector2 targetPosition;

    private void Start()
    {
        _playerCamera = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        transform.position = new Vector2(-0.29f, -3.41f);
        _playerRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeColor());
    }
    
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, missleSpeed * Time.deltaTime);
        if ((Vector2)transform.position == targetPosition)
        {
            int clipNum = Random.Range(0, _explosionClips.Length);
            AudioSource.PlayClipAtPoint(_explosionClips[clipNum], _playerCamera.transform.position);
            AfterMissleDeath();
        }
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            _playerRenderer.color = new Color(255f, 255f, 255f);

            yield return new WaitForSeconds(0.5f);

            _playerRenderer.color = new Color(0f, 0f, 255f);

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void AfterMissleDeath()
    {
        Instantiate(_playerMissileExplosion, transform.position, Quaternion.identity);
        _missileTrail.transform.parent = null;
        _missileTrail.GetComponent<TrailRenderer>().autodestruct = true;
        Destroy(this.gameObject);
    }
}
