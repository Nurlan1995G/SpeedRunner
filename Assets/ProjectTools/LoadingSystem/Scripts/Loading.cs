using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Loading : MonoBehaviour
{
    private AsyncOperation _asyncOperation;

    private void Start() => StartCoroutine(LoadSceneAsync());

    private IEnumerator LoadSceneAsync()
    {
        yield return new WaitUntil(() => YandexGame.SDKEnabled);

        _asyncOperation = SceneManager.LoadSceneAsync(1);
        _asyncOperation.allowSceneActivation = false;

        yield return new WaitUntil(() => _asyncOperation.progress >= .9f);

        _asyncOperation.allowSceneActivation = true;
    }
}