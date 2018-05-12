using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class BuildScript : EditorWindow
{
    [MenuItem("Spacetronaut/Build All and Deploy")]
    public static void BuildAll()
    {
        DateTime currentDate = DateTime.Now;

        BuildLinuxServer();
        BuildWebGl();

        CopyToServer();
        RunWebBuild();
    }

    [MenuItem("Spacetronaut/Build Linux")]
    public static void BuildLinuxServer()
    {
        // the scenes we want to include in the build
        EditorBuildSettingsScene[] s = EditorBuildSettings.scenes;

        string[] scenes = new string[s.Length];

        for (int i = 0; i < s.Length; i++)
        {
            scenes[i] = s[i].path;
        }

        string buildName = "Linux/";
        // build for windows stand alone
        string linuxStandAloneBuildName = buildName + "StandAlone.x86_64";
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
        BuildPipeline.BuildPlayer(scenes, "Builds/" + linuxStandAloneBuildName, BuildTarget.StandaloneLinux64, BuildOptions.EnableHeadlessMode);
    }

    [MenuItem("Spacetronaut/Build Windows")]
    public static void BuildWindows()
    {
        // the scenes we want to include in the build
        EditorBuildSettingsScene[] s = EditorBuildSettings.scenes;

        string[] scenes = new string[s.Length];

        for (int i = 0; i < s.Length; i++)
        {
            scenes[i] = s[i].path;
        }

        string buildName = "Windows/";
        // build for windows stand alone
        string windowsStandAloneBuildName = buildName + "StandAlone.exe";
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        BuildPipeline.BuildPlayer(scenes, "Builds/" + windowsStandAloneBuildName, BuildTarget.StandaloneWindows64, BuildOptions.None);
        PlayBuild("Builds/" + windowsStandAloneBuildName);
    }

    [MenuItem("Spacetronaut/Build WebGL")]
    public static void BuildWebGl()
    {
        // the scenes we want to include in the build
        EditorBuildSettingsScene[] s = EditorBuildSettings.scenes;

        string[] scenes = new string[s.Length];

        for (int i = 0; i < s.Length; i++)
        {
            scenes[i] = s[i].path;
        }

        // build for web player
        string webplayerBuildName = "WebGL/";
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
        BuildPipeline.BuildPlayer(scenes, "Builds/WebGL/", BuildTarget.WebGL, BuildOptions.None);
    }

    private static void PlayBuild(string s)
    {
        var processInfo = new ProcessStartInfo(@s);
        processInfo.WindowStyle = ProcessWindowStyle.Normal;
        processInfo.CreateNoWindow = false;
        processInfo.UseShellExecute = false;

        var process = Process.Start(processInfo);

        process.WaitForExit();
    }
    
    private static void CopyToServer()
    {
        string path = System.IO.Path.GetDirectoryName(Application.dataPath + "..") + "/copytoserver.bat";
        var processInfo = new ProcessStartInfo(path);
        processInfo.WindowStyle = ProcessWindowStyle.Normal;
        processInfo.CreateNoWindow = false;
        processInfo.UseShellExecute = true;
        
        var process = Process.Start(processInfo);
    }

    private static void RunWebBuild()
    {
        string path = System.IO.Path.GetDirectoryName(Application.dataPath + "..") + @"/webserver.bat";
        var processInfo = new ProcessStartInfo(@path);
        processInfo.WindowStyle = ProcessWindowStyle.Normal;
        processInfo.CreateNoWindow = false;
        processInfo.UseShellExecute = true;

        var process = Process.Start(processInfo);
    }
}
