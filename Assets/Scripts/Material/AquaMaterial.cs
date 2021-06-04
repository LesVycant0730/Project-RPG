using System.Collections;
using UnityEngine;

public class AquaMaterial : MonoBehaviour
{
    private MeshRenderer mesh;

	[Header ("Setting")]
	[SerializeField] private float offset;
	[SerializeField] private float updateDelay = 1.0f;
	[SerializeField] private Vector2 initialOffset;
	[SerializeField] private Vector2 maxOffset;
	[SerializeField] private AnimationCurve updateCurve;

	private void Awake()
	{
		mesh = GetComponent<MeshRenderer>();
		Vector2 tempOffset = mesh.material.GetTextureOffset("_MainTex");
		initialOffset = tempOffset - new Vector2(offset, offset);
		maxOffset = tempOffset + new Vector2(offset, offset);
	}

	IEnumerator Start()
	{
		while (true)
		{
			mesh.material.SetTextureOffset("_MainTex", Vector2.Lerp(initialOffset, maxOffset, updateCurve.Evaluate(Time.time / updateDelay)));
			yield return null;
		}
	}
}
