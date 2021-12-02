using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissleManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerMissle;
    [SerializeField] private GameObject _missleSpawningArea;
    [SerializeField] private AudioClip _launchSFX;
    [SerializeField] private SpriteRenderer _plrAmmoDisplay;

    private GameObject _playerCamera;
    

    [SerializeField] private Sprite[] _ammoSprites;

    [SerializeField]
    private AudioClip _lowAmmo;
    
    private int _currentAmmo = 12;

    [SerializeField] private float _missleSpeed;

    private bool _weaponUsable = true;

    GameObject _missleClone;

    private void Start()
    {
        _playerCamera = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        _plrAmmoDisplay.sprite = _ammoSprites[11];
        _currentAmmo = 12;
    }

    public void KeyTriggered(Vector2 targetPos)
    {
        if (_weaponUsable && FindObjectOfType<PlayerMissleManager>()) // && _currentAmmo > 0)
        {
            _weaponUsable = false;
            // _currentAmmo--;

            /*
            if (_currentAmmo != 0)
            {
                _plrAmmoDisplay.sprite = _ammoSprites[_currentAmmo - 1];
            }
            else
            {
                _plrAmmoDisplay.sprite = null;
            }
             */

            StartCoroutine(FireMissle(targetPos));
        }
    }

    private IEnumerator FireMissle(Vector2 targetPos)
    {   
        _missleClone = Instantiate(_playerMissle, _missleSpawningArea.transform.position, Quaternion.identity);
        _missleClone.GetComponent<PlayerMissleScript>().targetPosition = targetPos;
        _missleClone.GetComponent<PlayerMissleScript>().missleSpeed = _missleSpeed;
        AudioSource.PlayClipAtPoint(_launchSFX, _playerCamera.transform.position);

        /*
        if (_currentAmmo <= 2)
        {
            AudioSource.PlayClipAtPoint(_lowAmmo, _playerCamera.transform.position);
        }
        */

        yield return new WaitForSeconds(2f);
        _weaponUsable = true;
    }
}
