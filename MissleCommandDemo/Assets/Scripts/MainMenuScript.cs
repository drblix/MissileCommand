using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // UI stuff
    private GameObject _titleText;
    private GameObject _enterPlay;
    private GameObject _creditsPrompt;
    private GameObject _credits;
    private GameObject _getReady;
    private GameObject _mainCamera;


    [SerializeField]
    private AudioClip _selectSFX;
    [SerializeField]
    private AudioClip _alertSFX;

    private bool _canInteract = false;
    private bool _inCredits = false;
    private bool _easterEggTriggered = false;

    private int _cPressedAmount = 0;
    private int _cTriggerAmount = 15;

    private void Awake()
    {
        _titleText = transform.Find("TitleText").gameObject;
        _enterPlay = transform.Find("EnterPlay").gameObject;
        _creditsPrompt = transform.Find("CreditsPrompt").gameObject;
        _credits = transform.Find("Credits").gameObject;
        _getReady = transform.Find("GetReady").gameObject;
        _mainCamera = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;

        StartCoroutine(IntroSequence());
        StartCoroutine(ResetEasterEgg());
    }

    private void Update()
    {
        CheckInput();
        CheckEasterEgg();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Return) && _canInteract && !_inCredits)
        {
            _titleText.SetActive(false);
            _creditsPrompt.SetActive(false);
            _enterPlay.SetActive(false);

            StartCoroutine(StartGame());
        }

        if (Input.GetKeyDown(KeyCode.C) && _canInteract)
        {
            _cPressedAmount++;
            AudioSource.PlayClipAtPoint(_selectSFX, _mainCamera.transform.position);
            if (!_credits.activeInHierarchy)
            {
                _inCredits = true;

                _titleText.SetActive(false);
                _creditsPrompt.SetActive(false);
                _enterPlay.SetActive(false);
                _credits.SetActive(true);
            }
            else
            {
                _inCredits = false;

                _titleText.SetActive(true);
                _creditsPrompt.SetActive(true);
                _enterPlay.SetActive(true);
                _credits.SetActive(false);
            }
        }
    }

    private void CheckEasterEgg()
    {
        if (_cPressedAmount == _cTriggerAmount)
        {
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator IntroSequence()
    {
        while (_titleText.GetComponent<RectTransform>().transform.position.y >= 2.3f)
        {
            _titleText.GetComponent<RectTransform>().Translate(new Vector3(0f, -1.5f * Time.deltaTime, 0f));
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.5f);

        _creditsPrompt.SetActive(true);
        _enterPlay.SetActive(true);
        _canInteract = true;
    }

    private IEnumerator StartGame()
    {
        _canInteract = false;

        for (int i = 0; i < 3; i++)
        {
            _getReady.SetActive(true);
            AudioSource.PlayClipAtPoint(_alertSFX, _mainCamera.transform.position);
            yield return new WaitForSeconds(0.5f);
            _getReady.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        SceneManager.LoadScene(1);
    }

    private IEnumerator ResetEasterEgg()
    {
        while (!_easterEggTriggered)
        {
            yield return new WaitForSeconds(5f);
            _cPressedAmount = 0;
        }
    }
}
