using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildVersionUpdater : MonoBehaviour
{
    public Text versiondisplaytxt;
    public TextAsset versionFile; //인스펙터에 추가
    public string[] buildDate;
    void Start()
    {   //buildDate= System.IO.File.ReadAllLines(Application.persistentDataPath + "BuildDate.txt");

        if (versionFile != null)
        {
            versiondisplaytxt.text = "v " + Application.version + " / " + versionFile.text;
        }
        else if (versionFile == null)
        {
            Debug.Log("Version file does not exists. Build again or assign at inspector");
        }
    }
}

