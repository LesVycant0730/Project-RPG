using System.Text;
using UnityEngine;

namespace TextExtension
{
    public enum LogColor
    {
        Black,
        White,
        Green,
        Yellow,
        Blue,
        Red,
    }

    public static class TextModifier
	{
        public static string SpaceByUppercase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "";
            }

            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);

            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
				{
                    newText.Append(' ');
                }

                newText.Append(text[i]);
            }

            return newText.ToString();
        }
    }

    public static class EditorLog
    {
        private static string ConvertColor(LogColor _color)
		{
            return _color.ToString().ToLower();
		}

        public static string ColorLog(object message, LogColor _color)
		{
            return "<color=" + ConvertColor(_color) + ">" + message.ToString() + "</color>";
        }
    }
}

public class CustomLog : MonoBehaviour
{
    public static CustomLog Instance { get; private set; }

    [SerializeField] private bool isLoggingEnabled = true;

	private void Awake()
	{
        Instance = this;
	}

	public static void Log(string _log)
	{
        if (Instance && Instance.isLoggingEnabled)
		{
            Debug.Log(_log);
		}
	}
}
