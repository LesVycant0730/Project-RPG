using System.Collections;
using UnityEngine;

public class AquaMaterial : MonoBehaviour
{
    private Renderer mesh;

	[Header ("Setting (Update)")]
	[SerializeField] private float updateDelay = 1.0f;
	[SerializeField] private AnimationCurve updateCurve;

	[Header("Setting (Offset)")]
	[SerializeField] private Vector2 offsetMin;
	[SerializeField] private Vector2 offsetMax;

	private Coroutine AquaUpdate = null;

	private void Awake()
	{
		mesh = GetComponent<Renderer>();
	}

	private void OnEnable()
	{
		if (AquaUpdate != null)
		{
			StopCoroutine(AquaUpdate);
		}

		AquaUpdate = StartCoroutine(AquaMatUpdate());
	}

	IEnumerator AquaMatUpdate()
	{
		while (true)
		{
			mesh.material.SetTextureOffset("_MainTex", Vector2.Lerp(offsetMin, offsetMax, updateCurve.Evaluate(Time.time / updateDelay)));
			yield return null;
		}
	}
}
