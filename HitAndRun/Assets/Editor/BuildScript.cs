using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class BuildScript 
{

	[PostProcessBuild]
	private static void onPostProcessBuildPlayer(BuildTarget target, String pathToBuiltProject)
	{
		UnityEngine.Debug.Log(pathToBuiltProject);
		UnityEngine.Debug.Log(target);
	}
}
