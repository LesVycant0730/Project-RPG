using UnityEngine;
using System.IO;

public class BaseLibrary : MonoBehaviour
{
	private static GameObject libraryHolder;

	[SerializeField] protected string libraryFolderDirectory;
	[SerializeField] protected string libraryFileType;

	protected virtual void Awake()
	{
		if (libraryHolder == null)
		{
			libraryHolder = new GameObject() { name = "Library" };
			DontDestroyOnLoad(libraryHolder);
		}

		transform.SetParent(libraryHolder.transform);
	}

	protected string[] GetLibraryAssets()
	{
		return Directory.GetFiles(libraryFolderDirectory, $"*{libraryFileType}");
	}

	protected virtual void SetupLibrary() { }
}
