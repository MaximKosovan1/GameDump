using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

public class Logger : MonoBehaviour
{
    string filename = "";

    private void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    private void Start()
    {
        filename = Application.dataPath + "/LogFile.txt";
    }

    public void Log([CanBeNull] string logString, string stackTrace, LogType type)
    {
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine(type + " - " + "[" + System.DateTime.Now + "]" + logString);
        tw.Close();
    }
}
