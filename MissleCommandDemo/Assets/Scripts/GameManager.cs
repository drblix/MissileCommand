using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // UI stuff
    [SerializeField] Canvas _mainGameCanvas;
    [SerializeField] Text _gameOverText;
    [SerializeField] Text _scoreText;

    [SerializeField] 
    private GameObject _ufoEnemy;

    private GameObject _playerObject;

    [SerializeField] 
    private AudioClip _explosionSFX;
    [SerializeField] 
    private GameObject _enemyMissileManagerPrefab;
    [SerializeField] 
    private GameObject _playerMissileManagerPrefab;
    [SerializeField] 
    private GameObject _playerMissileExplosion;

    private GameObject _playerMissileManagerScene;
    private GameObject _enemyMissileManagerScene;

    private LayerMask _missileLayer;

    private int _scoreTotal = 0;

    public int currentMissiles = 6;
    public int currentLevel = 1;

    /*
     * levels 5+: background = (94, 4, 82) ground = (103, 200, 127)
     */

    public bool gameOver = false;

    private void Awake()
    {
        _playerObject = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        _missileLayer = LayerMask.GetMask("Missiles");
        _enemyMissileManagerScene = GameObject.Find("EnemyMissileManager");
        _playerMissileManagerScene = GameObject.Find("PlayerMissileManager");

        StartCoroutine(LoadNextLevel());
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        QuitGame();
    }

    public void GameOver()
    {
        gameOver = true;

        _gameOverText.gameObject.SetActive(true);

        if (_playerMissileManagerScene || _enemyMissileManagerScene || _playerObject)
        {
            Destroy(_playerMissileManagerScene);
            Destroy(_enemyMissileManagerScene);
            Instantiate(_playerMissileExplosion, _playerObject.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_explosionSFX, new Vector3(0f, 0.19f, -7.1f));

            foreach (GameObject missile in FindObjectsOfType<GameObject>())
            {
                if (missile.layer == _missileLayer)
                {
                    Destroy(missile);
                }
            }

            if (GameObject.Find("IdleSFXContainer(Clone)"))
            {
                GameObject container = GameObject.Find("IdleSFXContainer(Clone)");
                Destroy(container);
            }
        }

        StartCoroutine(ReturnToMain());
    }

    public void AddToScore(int scoreAmount)
    {
        _scoreTotal += scoreAmount;
        _scoreText.text = System.Convert.ToString(_scoreTotal);
    }

    private void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("quitting game");
            Application.Quit();
        }
    }

    private IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitUntil(() => currentMissiles == 0);
        Debug.Log("load next level");

        GameObject enemyMissileManager = FindObjectOfType<EnemyMissileManager>().gameObject;

        Destroy(enemyMissileManager);

        int amountOfEnemysLeft = FindObjectsOfType<EnemyMissileScript>().Length + FindObjectsOfType<UFOScript>().Length + FindObjectsOfType<SplitterMissileScript>().Length;

        while (amountOfEnemysLeft > 0)
        {
            Debug.Log(amountOfEnemysLeft);
            yield return new WaitForSeconds(0.1f);
            amountOfEnemysLeft = FindObjectsOfType<EnemyMissileScript>().Length + FindObjectsOfType<UFOScript>().Length + FindObjectsOfType<SplitterMissileScript>().Length;
        }

        yield return new WaitForSeconds(4f);

        currentLevel += 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        switch (currentLevel)
        {
            case var exp when currentLevel >= 5:
                _playerObject.GetComponent<Camera>().backgroundColor = new Color(94f, 4f, 82f);
                GameObject terrain = GameObject.Find("Terrain");
                terrain.GetComponent<SpriteRenderer>().color = new Color(103f, 200f, 127f);
                break;
        }

        StartCoroutine(LoadNextLevel());
    }
}
