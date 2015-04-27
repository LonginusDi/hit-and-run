using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Diagnostics;

public static class BuildScript 
{

	[PostProcessBuild]
	private static void onPostProcessBuildPlayer(BuildTarget target, String pathToBuiltProject)
	{
		UnityEngine.Debug.Log(pathToBuiltProject);
		UnityEngine.Debug.Log(target);
//		string objCPath = Application.dataPath + "/ObjC";
		Process myProcess = new Process();
		try
		{
			myProcess.StartInfo.FileName = "python";
			myProcess.StartInfo.Arguments = String.Format( "Assets/Editor/crashlytics.py \"{0}\"", pathToBuiltProject);
			myProcess.StartInfo.UseShellExecute = false;
			myProcess.StartInfo.CreateNoWindow = true;
			myProcess.Start();
			myProcess.WaitForExit();
			UnityEngine.Debug.Log("python" + " " + String.Format( "Assets/Editor/crashlytics.py \"{0}\"", pathToBuiltProject) + " exited with code " + myProcess.ExitCode);
		}
		catch (Exception e)
		{
			UnityEngine.Debug.LogException(e);
		}
	}
}
