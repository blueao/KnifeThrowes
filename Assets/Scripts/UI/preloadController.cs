using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class preloadController : MonoBehaviour
{
    public GameObject LoadingScenes;
    void Start()
    {
        StartCoroutine(LoadSCene());

    }
    IEnumerator LoadSCene()
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation levelLoader = SceneManager.LoadSceneAsync("MainScenes", LoadSceneMode.Additive);
        SceneManager.UnloadScene("preload");
        yield return new WaitUntil(() => levelLoader.isDone);
        LoadingScenes.SetActive(false);
    }
}
