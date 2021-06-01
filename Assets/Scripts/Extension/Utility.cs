using System;
using System.Collections.Generic;
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

    public static bool IsArrayContainAllTypeElements<T>(T[] _enumArr, out List<T> _missingElements) where T : Enum
	{
        _missingElements = new List<T>();

        IEnumerable<T> enums = GetTypeElements<T>();

        StringBuilder builder = new StringBuilder();
        
        // Check sequence
        builder.Append(Enumerable.SequenceEqual(enums, _enumArr) ? "Correct element sequence" : $"Wrong sequence order for type: {typeof(T)}");

        foreach(var value in enums)
		{
            // Fix this SINGLE
            builder.Append(_enumArr.Single(x => _enumArr.Contains(value)) != null ? "No Duplication" : $"Found Duplicated value");
        }

        return false;
	}

    public static void SetActive(this Transform _trans, bool _active)
	{
        if (_trans != null)
		{
            _trans.gameObject.SetActive(_active);
		}
	}
}
