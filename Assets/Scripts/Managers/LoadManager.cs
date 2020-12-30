using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;
    
    [SerializeField]
    private ScriptableFloatValue _loadProggers;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        LoadMenu();
    }

    public void LoadMenu() {
        //TODO: animation for load
        StartCoroutine(LoadSceneCoroutine("Menu"));
    }

    private IEnumerator LoadSceneCoroutine(string name) {
        var asyncOperation = SceneManager.LoadSceneAsync(name);
        while (!asyncOperation.isDone) {
            _loadProggers.value = asyncOperation.progress / .95f;
            yield return null;
        }
    }

}
