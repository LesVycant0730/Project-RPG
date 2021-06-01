using UnityEngine;

public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance = null;
    public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<T>();

				if (_instance == null)
				{
					GameObject newObject = new GameObject
					{
						name = typeof(T).ToString()
					};

					newObject.AddComponent<T>();
				}
			}

			return _instance;
		}
	}
}
