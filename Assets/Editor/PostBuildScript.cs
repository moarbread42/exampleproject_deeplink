using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.IO;

public class BuildScript
{
    //Script is called after the building process, BuildTarget and string pathToBuiltProject are mandatory arguments
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        //Give the user a file folder pop-up asking for the location you wish to save the file to
        //string path = EditorUtility.SaveFolderPanel("Save location", "", "");
        //Alternative you can also just hardcode the path..
        string pathalt = Application.persistentDataPath;
        //Get the current datetime and convert it to a string with some explanatory text
        string date = string.Format("Last Build date: {0}", DateTime.Now.ToString("yyyyMMdd H:mm")+ "//Build version: " + Application.version);

        //Write the date to a text file called "BuildDate.txt" at the selected location
        File.WriteAllText(pathalt + "BuildDate.txt", date);
        File.Copy(pathalt + "BuildDate.txt", Application.dataPath+@"\Scripts\BuildDate.txt", true);
    }
}