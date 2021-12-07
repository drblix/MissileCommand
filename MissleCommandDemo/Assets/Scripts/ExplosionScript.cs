using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] AudioClip[] _explosionEffects;

    private GameObject _playerCamera;

    private void Start()
    {
        _playerCamera = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerMissle"))
        {
            int randomNum = Random.Range(0, _explosionEffects.Length);
            AudioSource.PlayClipAtPoint(_explosionEffects[randomNum], _playerCamera.transform.position);
            FindObjectOfType<GameManager>().AddToScore(100);

            if (collision.GetComponent<EnemyMissileScript>())
            {
                collision.GetComponent<EnemyMissileScript>().DestroyMissile(false);
            }
            else if (collision.GetComponent<SplitterMissileScript>())
            {
                collision.GetComponent<SplitterMissileScript>().DestroyMissile();
            }
            else
            {
                Debug.LogWarning("EnemyMissleScript not found!");
            }

            if (FindObjectOfType<EnemyMissileManager>() != null)
            {
                FindObjectOfType<EnemyMissileManager>().EnemyMissileDestroyed();
            }
        }
    }
}
