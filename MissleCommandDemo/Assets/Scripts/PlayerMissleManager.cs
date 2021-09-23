using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissleManager : MonoBehaviour
{
    [SerializeField] GameObject _playerMissle;
    [SerializeField] GameObject _missleSpawningArea;
    [SerializeField] AudioClip _launchSFX;
    [SerializeField] GameObject _playerCamera;

    [SerializeField] private float _missleSpeed;

    private bool _weaponUsable = true;

    GameObject _missleClone;

    public void KeyTriggered(Vector2 targetPos)
    {
        if (_weaponUsable)
        {
            StartCoroutine(FireMissle(targetPos));
        }
    }

    private IEnumerator FireMissle(Vector2 targetPos)
    {
        _weaponUsable = false;
        print(targetPos);

        _missleClone = Instantiate(_playerMissle, _missleSpawningArea.transform.position, Quaternion.identity);
        _missleClone.GetComponent<PlayerMissleScript>().targetPosition = targetPos;
        _missleClone.GetComponent<PlayerMissleScript>().missleSpeed = _missleSpeed;
        AudioSource.PlayClipAtPoint(_launchSFX, _playerCamera.transform.position);

        yield return new WaitForSeconds(2f);
        _weaponUsable = true;
    }
}
