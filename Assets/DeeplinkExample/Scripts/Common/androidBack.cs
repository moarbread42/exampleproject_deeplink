using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class androidBack : MonoBehaviour
{

    void Update()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))  //기기버튼이 눌렸을 때 이벤트 , 포커스 이동했을 때
        {

            if (Input.GetKey(KeyCode.Home))
            {
                Debug.Log("Android Home button pressed");
                OnApplicationLeave(); 
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("Android Escape button pressed");
                OnApplicationLeave(); 
                
            }
            else if (Input.GetKey(KeyCode.Menu))
            {
                Debug.Log("Android Menu button pressed");
            }
        }
    }

    public void OnApplicationLeave()  //스티커판이 추가되면 저장여부 체크&다른 메뉴 제한 
    {
        Application.Quit();
    }
    public void PrevScene()
    {
        var index = GetCurrentLevel();

        if (index >= 0)
        {
            if (--index < 0)
            {
                index = GetLevelCount() - 1;
            }
            StartCoroutine(ResetWait(index));

        }
    }
    public void BackButton(string whichAction)
    {
        // UnityPlayer.UnitySendMessage("AndroidBack", "BackButton", "Activate");   //뒤로가기 또는 유니티 액티비티 종료

        Scene thisScene = SceneManager.GetActiveScene();
        if (whichAction.Equals("Activate"))
        {
            Application.Quit();
        }
    }

    private static int GetCurrentLevel()
    {
        var scene = SceneManager.GetActiveScene();
        var index = scene.buildIndex;

        if (index >= 0)
        {
            if (SceneManager.GetSceneByBuildIndex(index).path != scene.path)
            {
                return -1;
            }
        }
        return index;
    }
    private static int GetLevelCount()
    {
        return SceneManager.sceneCountInBuildSettings;
    }

    private static void LoadLevel(int index)
    {

        SceneManager.LoadScene(index);
    }
    IEnumerator ResetWait(int index)
    {
        yield return new WaitForSeconds(0.5f);
        LoadLevel(index);
    }

}
