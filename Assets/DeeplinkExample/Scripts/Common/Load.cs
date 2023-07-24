using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public string SceneString;  //딥링크로 연결된 다음으로 처음 진입하는 씬의 이름-인스펙터에서 추가하거나 스크립트로 추가합니다.
    public static bool IsLoading;

    public void SelectUnityScene(string SceneName) 
    {

        SceneString = SceneName;
        StartCoroutine(ChangeScene(SceneString));
    }
    public void Start()
    {
        SelectUnityScene(SceneString);
    }
    public static IEnumerator ChangeScene(string SceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}
