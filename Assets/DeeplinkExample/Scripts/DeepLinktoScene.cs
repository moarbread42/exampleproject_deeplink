using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class DeepLinktoScene : MonoBehaviour
{
    public enum BuildMode
    {
        Test,
        Release
    }
    public BuildMode buildMode;
    public static DeepLinktoScene Instance { get; private set; }
    public string deeplinkURL;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SelectBuildMode();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SelectBuildMode()
    {
        switch (buildMode)
        {
            case BuildMode.Test:
                onDeepLinkActivated("unitydl://"+Application.productName+"?otp=");  //AndroidManifest에 있는 액티비티 명: unitydl://  //맨 처음에 빌드되는 씬: ?otp=
                break;
            case BuildMode.Release:
                Application.deepLinkActivated += onDeepLinkActivated;
                if (!String.IsNullOrEmpty(Application.absoluteURL))
                {
                    onDeepLinkActivated(Application.absoluteURL);
                }
                else
                {
                    deeplinkURL = "unitydl://" + Application.productName + "?otp=";
                }
                break;
        }
    }
    private void onDeepLinkActivated(string url)
    {
        deeplinkURL = url;
        string sceneName = url.Split("?"[0])[1];
        bool validScene;
        switch (sceneName)
        {
            case "otp=":
                validScene = true;
                break;
            default:
                validScene = false;
                break;
        }
        if (validScene) SceneManager.LoadScene(sceneName);
    }


}
