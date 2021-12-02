using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    private GameObject _titleText;
    private GameObject _enterPlay;
    private GameObject _creditsPrompt;
    private GameObject _credits;

    private bool _canInteract = false;

    private void Awake()
    {
        _titleText = transform.Find("TitleText").gameObject;
        _enterPlay = transform.Find("EnterPlay").gameObject;
        _creditsPrompt = transform.Find("CreditsPrompt").gameObject;
        _credits = transform.Find("Credits").gameObject;
        StartCoroutine(IntroSequence());
    }

    private void Update()
    {
        Debug.Log(_titleText.transform.position.y);
    }

    private IEnumerator IntroSequence()
    {
        while (_titleText.transform.position.y < 3.6f)
        {
            _titleText.transform.Translate(new Vector3(0f, -0.5f * Time.deltaTime, 0f));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
