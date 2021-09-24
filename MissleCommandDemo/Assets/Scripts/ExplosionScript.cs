using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] AudioClip[] _explosionEffects;
    [SerializeField] GameObject _playerCamera;

    private void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerMissle"))
        {
            int randomNum = Random.Range(0, _explosionEffects.Length);
            AudioSource.PlayClipAtPoint(_explosionEffects[randomNum], _playerCamera.transform.position);
            if (collision.GetComponent<EnemyMissleScript>())
            {
                collision.GetComponent<EnemyMissleScript>().DestroyMissle(false);
            }
            else
            {
                Debug.LogWarning("EnemyMissleScript not found!");
            }
        }
    }
}
