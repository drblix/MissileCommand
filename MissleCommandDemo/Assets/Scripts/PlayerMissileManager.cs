using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _playerMissile;
    [SerializeField] 
    private GameObject _missileSpawningArea;
    [SerializeField] 
    private AudioClip _launchSFX;
    [SerializeField] 
    private SpriteRenderer _plrAmmoDisplay;
    [SerializeField] 
    private GameObject _lowAmmoNotification;

    private GameObject _playerCamera;

    [SerializeField] private Sprite[] _ammoSprites;

    [SerializeField]
    private AudioClip _lowAmmo;
    
    private int _currentAmmo = 12;

    [SerializeField] 
    private float _missleSpeed;
    [SerializeField]
    private float _reloadTime;

    private bool _weaponUsable = true;

    private GameObject _missileClone;

    private void Awake()
    {
        _playerCamera = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        _missileSpawningArea = GameObject.Find("Player").transform.Find("LaunchTransform").gameObject;
        _plrAmmoDisplay = GameObject.Find("Player").transform.Find("AmmoDisplay").GetComponent<SpriteRenderer>();
        _lowAmmoNotification = 

        _plrAmmoDisplay.sprite = _ammoSprites[11];
        _currentAmmo = 12;
    }

    public void KeyTriggered(Vector2 targetPos)
    {
        if (_weaponUsable && FindObjectOfType<PlayerMissileManager>() && _currentAmmo > 0)
        {
            _weaponUsable = false;
            _currentAmmo--;

            
            if (_currentAmmo != 0)
            {
                _plrAmmoDisplay.sprite = _ammoSprites[_currentAmmo - 1];
            }
            else
            {
                _plrAmmoDisplay.sprite = null;
            }
            

            StartCoroutine(FireMissle(targetPos));
        }
    }

    private IEnumerator FireMissle(Vector2 targetPos)
    {   
        _missileClone = Instantiate(_playerMissile, _missileSpawningArea.transform.position, Quaternion.identity);
        _missileClone.GetComponent<PlayerMissileScript>().targetPosition = targetPos;
        _missileClone.GetComponent<PlayerMissileScript>().missleSpeed = _missleSpeed;
        AudioSource.PlayClipAtPoint(_launchSFX, _playerCamera.transform.position);

        
        if (_currentAmmo <= 2)
        {
            _lowAmmoNotification.SetActive(true);
            AudioSource.PlayClipAtPoint(_lowAmmo, _playerCamera.transform.position);
        }
        

        yield return new WaitForSeconds(_reloadTime);
        _lowAmmoNotification.SetActive(false);
        _weaponUsable = true;
    }



    public void Reset()
    {
        _currentAmmo = 12;
        _plrAmmoDisplay.sprite = _ammoSprites[11];
        _lowAmmoNotification.SetActive(false);
    }
}
