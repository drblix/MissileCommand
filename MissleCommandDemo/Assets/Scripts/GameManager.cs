using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // UI stuff

    [Header("UI elements")]
    [SerializeField] 
    private Canvas _mainGameCanvas;
    [SerializeField] 
    private Text _gameOverText;
    [SerializeField] 
    private Text _scoreText;

    private GameObject _playerObject;

    [Header("Prefabs")]
    [SerializeField] 
    private GameObject _ufoEnemy;
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

    [Header("Game Stats")]
    public int currentMissiles = 6;
    public int currentLevel = 1;

    public bool gameOver = false;

    [Header("Level customizations")]
    [SerializeField]
    private Color _5background;
    [SerializeField]
    private Color _5ground;

    // Misc variables
    private bool _city01LastState;
    private bool _city02LastState;
    private bool _city03LastState;
    private bool _city04LastState;


    /*
     * levels 5+: background = (94, 4, 82) ground = (103, 200, 127)
     */

    private void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length >= 2)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        _playerObject = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        _missileLayer = LayerMask.GetMask("Missiles");
        _enemyMissileManagerScene = GameObject.Find("EnemyMissileManager");
        _playerMissileManagerScene = GameObject.Find("PlayerMissileManager");

        StartCoroutine(LoadNextLevel());
    }

    private void Update()
    {
        QuitGame();
        CheckVariables();
        CheckScene();
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

    private void CheckVariables()
    {
        if (_mainGameCanvas == null)
        {
            _mainGameCanvas = GameObject.Find("MainGameCanvas").GetComponent<Canvas>();
        }

        if (_gameOverText == null)
        {
            _gameOverText = GameObject.Find("MainGameCanvas").transform.Find("GameOverText").GetComponent<Text>();
        }

        if (_scoreText == null)
        {
            _scoreText = GameObject.Find("MainGameCanvas").transform.Find("Score").GetComponent<Text>();
        }

        if (_playerObject == null)
        {
            _playerObject = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        }
    }

    private void CheckScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitUntil(() => currentMissiles == 0); // Waits until currentmissiles is equal to 0
        Debug.Log("load next level");

        GameObject enemyMissileManager = FindObjectOfType<EnemyMissileManager>().gameObject;
        CitiesManager citiesManager = FindObjectOfType<CitiesManager>();

        enemyMissileManager.SetActive(false);

        int amountOfEnemysLeft = FindObjectsOfType<EnemyMissileScript>().Length + FindObjectsOfType<UFOScript>().Length + FindObjectsOfType<SplitterMissileScript>().Length;

        while (amountOfEnemysLeft > 0) // Waits for all enemys to be destroyed before proceeding
        {
            yield return new WaitForSeconds(0.1f);
            amountOfEnemysLeft = FindObjectsOfType<EnemyMissileScript>().Length + FindObjectsOfType<UFOScript>().Length + FindObjectsOfType<SplitterMissileScript>().Length;
        }

        yield return new WaitForSeconds(4f);

        _city01LastState = citiesManager.city01Alive;
        _city02LastState = citiesManager.city02Alive;
        _city03LastState = citiesManager.city03Alive;
        _city04LastState = citiesManager.city04Alive;

        currentLevel += 1;

        Vector2 lastCursorPos = FindObjectOfType<PlayerCursor>().gameObject.transform.position;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return new WaitUntil(() => FindObjectOfType<EnemyMissileManager>());

        switch (currentLevel)
        {
            case var exp when currentLevel >= 5:
                _playerObject.GetComponent<Camera>().backgroundColor = _5background;
                GameObject terrain = GameObject.Find("Terrain");
                terrain.GetComponent<SpriteRenderer>().color = _5ground;
                break;
        }

        _playerObject = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;
        _enemyMissileManagerScene = FindObjectOfType<EnemyMissileManager>().gameObject;
        _playerMissileManagerScene = FindObjectOfType<PlayerMissileManager>().gameObject;
        _scoreText = GameObject.Find("MainGameCanvas").transform.Find("Score").GetComponent<Text>();

        currentMissiles = currentLevel + 6;        

        _enemyMissileManagerScene.GetComponent<EnemyMissileManager>().missilesToSpawn = currentMissiles;
        _scoreText.text = System.Convert.ToString(_scoreTotal);

        yield return new WaitUntil(() => FindObjectOfType<PlayerCursor>());

        FindObjectOfType<PlayerCursor>().gameObject.transform.position = lastCursorPos; // Sets cursor to where it was previously before loading
        
        Debug.Log("Level switch done");

        TransferCityStates();
        StartCoroutine(LoadNextLevel());
    }

    private void TransferCityStates()
    {
        CitiesManager citiesManager = FindObjectOfType<CitiesManager>();

        citiesManager.city01Alive = _city01LastState;
        citiesManager.city02Alive = _city02LastState;
        citiesManager.city03Alive = _city03LastState;
        citiesManager.city04Alive = _city04LastState;

        citiesManager.RefreshCities();
    }
}
