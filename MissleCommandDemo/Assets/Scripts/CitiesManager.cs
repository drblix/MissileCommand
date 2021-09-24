using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitiesManager : MonoBehaviour
{
    [SerializeField] GameObject[] _friendlyCities;
    [SerializeField] Sprite _destroyedCitySprite;

    private bool city01Alive = true;
    private bool city02Alive = true;
    private bool city03Alive = true;
    private bool city04Alive = true;

    
    void Update()
    {
        if (!city01Alive && !city02Alive && !city03Alive && !city04Alive)
        {
            print("game over");
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
}
