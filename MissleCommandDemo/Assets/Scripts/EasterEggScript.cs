using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EasterEggScript : MonoBehaviour
{
    private GameObject _mainCanvas;
    private GameObject _playerCamera;

    private GameObject _whatYouDoing;
    private GameObject _isThisSerious;
    private GameObject _thinkOfEverything;
    private GameObject _thisIsAGame;
    private GameObject _butWhatIf;
    private GameObject _whatIfTheWorld;
    private GameObject _itsGoodToAsk;
    private GameObject _byTheWay;
    private GameObject _justKidding;
    private GameObject _soInstead;
    private GameObject _bestChoice;

    [SerializeField]
    private AudioClip _robotMonologue;


    private void Awake()
    {
        _mainCanvas = transform.gameObject;
        _playerCamera = GameObject.Find("LowResSetup").transform.Find("Camera").gameObject;

        _whatYouDoing = _mainCanvas.transform.Find("WhatYouDoing").gameObject;
        _isThisSerious = _mainCanvas.transform.Find("IsThisSerious").gameObject;
        _thinkOfEverything = _mainCanvas.transform.Find("ThinkOfEverything").gameObject;
        _thisIsAGame = _mainCanvas.transform.Find("ThisIsAGame").gameObject;
        _butWhatIf = _mainCanvas.transform.Find("ButWhatIf").gameObject;
        _whatIfTheWorld = _mainCanvas.transform.Find("WhatIfTheWorld").gameObject;
        _itsGoodToAsk = _mainCanvas.transform.Find("ItsGoodToAsk").gameObject;
        _byTheWay = _mainCanvas.transform.Find("ByTheWay").gameObject;
        _soInstead = _mainCanvas.transform.Find("SoInstead").gameObject;
        _bestChoice = _mainCanvas.transform.Find("BestChoice").gameObject;

        StartCoroutine(StartMonologue());
    }

    private IEnumerator StartMonologue()
    {
        yield return new WaitForSeconds(1.5f);
        AudioSource.PlayClipAtPoint(_robotMonologue, _playerCamera.transform.position);

        _whatYouDoing.SetActive(true);
        yield return new WaitForSeconds(3f);
        _whatYouDoing.SetActive(false);
        _isThisSerious.SetActive(true);
        yield return new WaitForSeconds(8f);
        _isThisSerious.SetActive(false);
        _thinkOfEverything.SetActive(true);
        yield return new WaitForSeconds(9f);
        _thinkOfEverything.SetActive(false);
        _thisIsAGame.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        _thisIsAGame.SetActive(false);
        _butWhatIf.SetActive(true);
        yield return new WaitForSeconds(3f);
        _butWhatIf.SetActive(false);
        _whatIfTheWorld.SetActive(true);
        yield return new WaitForSeconds(6f);
        _whatIfTheWorld.SetActive(false);
        _itsGoodToAsk.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        _itsGoodToAsk.SetActive(false);
        _byTheWay.SetActive(true);
        yield return new WaitForSeconds(4f);
        _byTheWay.SetActive(false);
        _justKidding.SetActive(true);
        yield return new WaitForSeconds(3f);
        _justKidding.SetActive(false);
        _soInstead.SetActive(true);
        yield return new WaitForSeconds(7f);
        _soInstead.SetActive(false);
        _bestChoice.SetActive(true);
        yield return new WaitForSeconds(5f);
        _bestChoice.SetActive(false);
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }
}
