using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class MaterialBatcher : EditorWindow
{
	[MenuItem("Tools/Batch Create Materials")]
	public static void ShowWindow()
	{
		GetWindow<MaterialBatcher>("Batch Create Materials");
	}

	private void OnGUI()
	{
		if (GUILayout.Button("Create Materials for Selected Textures Recursively"))
		{
			CreateMaterialsRecursively();
		}
	}

	private static void CreateMaterialsRecursively()
	{
		string[] allTexturePaths = GetAllTexturePaths(Selection.objects);

		foreach (string texturePath in allTexturePaths)
		{
			Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);

			if (texture != null)
			{
				// Create a new material
				Material newMaterial = new Material(Shader.Find("Standard"));

				// Assign the texture to the material's main texture property
				newMaterial.mainTexture = texture;

				// Create "Materials" folder under the texture's folder if it doesn't exist
				string textureDirectory = Path.GetDirectoryName(texturePath);
				string materialsFolder = Path.Combine(textureDirectory, "Materials");

				if (!AssetDatabase.IsValidFolder(materialsFolder))
				{
					AssetDatabase.CreateFolder(textureDirectory, "Materials");
				}

				// Save the material in the "Materials" folder
				string materialPath = Path.Combine(materialsFolder, texture.name + ".mat");
				AssetDatabase.CreateAsset(newMaterial, materialPath);

				Debug.Log("Created material for: " + texture.name + " at " + materialPath);
			}
		}
		AssetDatabase.SaveAssets();
	}

	private static string[] GetAllTexturePaths(Object[] selectedObjects)
	{
		List<string> texturePaths = new List<string>();

		foreach (Object obj in selectedObjects)
		{
			string path = AssetDatabase.GetAssetPath(obj);

			if (Directory.Exists(path))
			{
				// If it's a folder, search recursively inside it for textures
				texturePaths.AddRange(Directory.GetFiles(path, "*.png", SearchOption.AllDirectories));
				texturePaths.AddRange(Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories));
				texturePaths.AddRange(Directory.GetFiles(path, "*.jpeg", SearchOption.AllDirectories));
			}
			else if (obj is Texture2D)
			{
				// If it's a texture, just add the path
				texturePaths.Add(path);
			}
		}

		return texturePaths.ToArray();
	}
}
