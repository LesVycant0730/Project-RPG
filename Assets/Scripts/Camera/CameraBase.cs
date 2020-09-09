using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
	private Camera baseCamera;
	[SerializeField] protected Transform currentTarget;
	[SerializeField] protected Vector3 defaultCameraPosOffset = Vector3.zero;
	[SerializeField] protected Vector3 defaultCameraEulerOffset = Vector3.zero;

	public virtual void Init()
	{
		baseCamera = GetComponent<Camera>();

		if (baseCamera == null)
		{
			Debug.LogError("Missing error component: " + gameObject.name);
		}
	}

	public virtual void Enable()
	{
		baseCamera.enabled = true;
	}

	public virtual void Disable()
	{
		baseCamera.enabled = false;
	}

	public virtual void SetCameraTarget(Transform _transform)
	{
		if (currentTarget == _transform)
		{
			Debug.Log("Camera target is the same, no need to set again.");
			return;
		}

		currentTarget = _transform;
	}
}
