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

    [SerializeField] private GameObject _ufoEnemy;

    [SerializeField] GameObject _playerObject;
    [SerializeField] AudioClip _explosionSFX;
    [SerializeField] GameObject _enemyMissileManager;
    [SerializeField] GameObject _playerMissileManager;
    [SerializeField] GameObject _playerMissileExplosion;

    private int _scoreTotal = 0;
    public bool gameOver = false;

    private void Start()
    {
        DontDestroyOnLoad(_mainGameCanvas);
        StartCoroutine(SpawnUFO());
    }

    private void Update()
    {
        QuitGame();
        RestartGame();
        if (gameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        // Y pos 0.4
        _gameOverText.gameObject.SetActive(true);
        _gameOverText.gameObject.GetComponent<RectTransform>().position = Vector2.MoveTowards(_gameOverText.transform.position, new Vector2(0f, 1.25f), 4f * Time.deltaTime);

        if (_playerMissileManager || _enemyMissileManager || _playerObject)
        {
            Destroy(_playerMissileManager);
            Destroy(_enemyMissileManager);
            Instantiate(_playerMissileExplosion, _playerObject.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_explosionSFX, new Vector3(0f, 0.19f, -7.1f));
            Destroy(_playerObject);

            foreach (GameObject missile in FindObjectsOfType<GameObject>())
            {
                if (missile.name == "PlayerMissle(Clone)" || missile.name == "EnemyMissle(Clone)")
                {
                    Destroy(missile);
                }
            }
        }
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

    private void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("restart called");
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator SpawnUFO()
    {
        while (!gameOver)
        {
            float num = Mathf.RoundToInt(Random.Range(1f, 2f)); // generates random number

            yield return new WaitForSeconds(Mathf.RoundToInt(Random.Range(20f, 30f))); // waits between 20 or 30 seconds (random)

            if (num == 2) // instantiates ufo if num == 2
            {
                Instantiate(_ufoEnemy, Vector2.zero, Quaternion.identity);
            }
        }
    }
}
