using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	public static class CreateConsoleProGameObject
	{
		[MenuItem("GameObject/ConsolePro/Instantiate console prefab", false, 0)]
		public static void InstantiateConsole()
		{
			var assets = AssetDatabase.FindAssets("DebugConsole");
			string path;
			foreach (var asset in assets)
			{
				path = AssetDatabase.GUIDToAssetPath(asset);
				string extension = Path.GetExtension(path);
				if (extension.Equals(".prefab"))
				{
					GameObject prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
					PrefabUtility.InstantiatePrefab(prefab);
				}
			}
		}
	}
}
