using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance = null;
    private GameLoad gameLoad;
    private GameData gameData;
    private GameObject loadingPanel;

    private float time;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        gameLoad = transform.GetComponent<GameLoad>();
        gameData = gameLoad.Load("save.json");
        loadingPanel = GameObject.Find("Canvas").transform.Find("Loading").gameObject;
    }

    public void StartButton()
    {
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(gameData.sceneName);

        operation.allowSceneActivation = false;
        loadingPanel.SetActive(true); // �� �ε� �� ȭ�� ����

        while (!operation.isDone)
        {
            time += Time.deltaTime;
            if (time > 2)
            {
                operation.allowSceneActivation = true;
            }

            yield return null; // �� ������ �ѱ�
        }

        loadingPanel = GameObject.Find("Canvas").transform.Find("Loading").gameObject;
        loadingPanel.SetActive(false); // �� �ε� �� ȭ�� ����
    }

    public void DeadLoadingTrue()
    {
        loadingPanel.SetActive(true);
    }

    public void DeadLodingFalse()
    {
        loadingPanel.SetActive(false);
        SceneManager.LoadScene(gameData.sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
