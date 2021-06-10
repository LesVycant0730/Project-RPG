using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using System.Text;

public static class Utility
{
    /// <summary>
    /// Return the length from the Enum.
    /// </summary>
    public static int GetEnumLength<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Length;
    }

    /// <summary>
    /// Return all elements from the Enum inside IEnumerable.
    /// </summary>
    public static IEnumerable<T> GetTypeElements<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static bool IsArrayContainAllEnumElements<T>(T[] _enumArr, out List<T> _missingElements) where T : Enum
	{
        // Prepare new list for storing new elements
        _missingElements = new List<T>();

        // Prepare string builder to debug log
        StringBuilder builder = new StringBuilder();

        // Get all enum elements
        IEnumerable<T> enums = GetTypeElements<T>();
        
        // Check sequence
        builder.Append(Enumerable.SequenceEqual(enums, _enumArr) ? $"Correct element sequence for {typeof(T)}\n" : $"Wrong sequence order for type: {typeof(T)}\n");

        bool isArrayCorrect = true;

        foreach (var value in enums)
		{
            // Find all matching elements
            T[] temp = Array.FindAll(_enumArr, x => x.Equals(value));

            // Add element to the list if the array do not have that element
            if (IsNullOrEmpty(temp))
			{
                _missingElements.Add(value);
				isArrayCorrect = false;
                builder.Append($"Added {value}\n");
			}

            // Found duplicated value
            else if (temp.Length > 1)
            {
                isArrayCorrect = false;
                builder.Append($"Found Duplicated value: {value}\n");
            }
        }

        Debug.Log(builder.ToString());

        return isArrayCorrect;
	}

    public static void SetActive(this Transform _trans, bool _active)
	{
        if (_trans != null)
		{
            _trans.gameObject.SetActive(_active);
		}
	}

    public static bool IsNullOrEmpty(this Array _array)
	{
        return _array == null || _array.Length == 0;
	}

    public static IEnumerator WaitForAnimation(this Animator _anim, string _clipName, Action _action)
	{
        yield return new WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).IsName(_clipName));
        yield return new WaitUntil(() => !_anim.GetCurrentAnimatorStateInfo(0).IsName(_clipName));

        _action?.Invoke();
	}
}
