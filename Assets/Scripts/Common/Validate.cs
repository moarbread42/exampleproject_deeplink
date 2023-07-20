using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Validate : MonoBehaviour
{
    public Load load;
    public string tokenValue;
    public bool IsTokenValid;
    void Start()
    {
        CompareServerToken();
    }

    private void CompareServerToken()
    {
        tokenValue = "validvalue"; //***compare token
        onTokenCheck(tokenValue);
    }

    private void onTokenCheck(string tokenValue)
    {
        if (tokenValue != null)
        {
            switch (tokenValue)
            {
                case "validvalue":
                    Debug.Log("token is valid");
                    IsTokenValid = true;
                    load.SelectUnityScene(load.SceneString);
                    break;
                default:
                    Debug.Log("잘못된 인증값입니다.");
                    IsTokenValid = false;
                    break;
            }
        }
        else
        {
            Debug.Log("입력값이 비었습니다.");
            IsTokenValid = false;
        }

    }
}
