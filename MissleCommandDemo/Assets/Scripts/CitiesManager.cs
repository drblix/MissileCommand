using UnityEngine;

public class CitiesManager : MonoBehaviour
{
    [SerializeField] GameObject[] _friendlyCities;
    [SerializeField] Sprite _destroyedCitySprite;

    [Header("City States")]
    public bool city01Alive = true;
    public bool city02Alive = true;
    public bool city03Alive = true;
    public bool city04Alive = true;

    private bool _gameOverCalled = false;
    
    void Update()
    {
        if (!city01Alive && !city02Alive && !city03Alive && !city04Alive && !_gameOverCalled)
        {
            _gameOverCalled = true;
            print("game over");
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    public void DestroyCity(int cityNum)
    {
        switch (cityNum)
        {
            case 1:
                if (!city01Alive)
                {
                    break;
                }
                _friendlyCities[0].GetComponent<SpriteRenderer>().sprite = _destroyedCitySprite;
                city01Alive = false;
                break;

            case 2:
                if (!city02Alive)
                {
                    break;
                }
                _friendlyCities[1].GetComponent<SpriteRenderer>().sprite = _destroyedCitySprite;
                city02Alive = false;
                break;

            case 3:
                if (!city03Alive)
                {
                    break;
                }
                _friendlyCities[2].GetComponent<SpriteRenderer>().sprite = _destroyedCitySprite;
                city03Alive = false;
                break;

            case 4:
                if (!city04Alive)
                {
                    break;
                }
                _friendlyCities[3].GetComponent<SpriteRenderer>().sprite = _destroyedCitySprite;
                city04Alive = false;
                break;
        }
    }

    public void RefreshCities()
    {
        if (!city01Alive)
        {
            _friendlyCities[0].GetComponent<SpriteRenderer>().sprite = _destroyedCitySprite;
        }

        if (!city02Alive)
        {
            _friendlyCities[1].GetComponent<SpriteRenderer>().sprite = _destroyedCitySprite;
        }

        if (!city03Alive)
        {
            _friendlyCities[2].GetComponent<SpriteRenderer>().sprite = _destroyedCitySprite;
        }

        if (!city04Alive)
        {
            _friendlyCities[3].GetComponent<SpriteRenderer>().sprite = _destroyedCitySprite;
        }
    }

    public bool CheckCity(int cityNum)
    {
        switch (cityNum)
        {
            case 1:
                return city01Alive;

            case 2:
                return city02Alive;

            case 3:
                return city03Alive;

            case 4:
                return city04Alive;

            default:
                return true;
        }
    }
}
