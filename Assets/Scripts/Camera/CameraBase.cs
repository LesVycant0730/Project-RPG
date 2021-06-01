using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
	private Camera cam;
	private Transform camTransform;

	[Header ("Camera Update Settings")]
	[SerializeField] private bool isUpdating = false;
	[SerializeField] protected Transform currentTarget;
	[SerializeField] private Transform cameraCenter;

	[Header("Camera Position Settings")]
	[SerializeField] private bool posUpdate = true;
	[SerializeField] private bool posLerp = false;
	[SerializeField] protected Vector3 camPosOffset = Vector3.zero;
	[SerializeField] private Vector3 currentCamVel = Vector3.zero;

	[SerializeField, Range(0.0f, 1.0f)] 
	protected float camPosSmooth = 0.15f;

	[Header("Camera Rotation Settings")]
	[SerializeField] private bool rotUpdate = true;
	[SerializeField] private bool rotLerp = false;
	[SerializeField] protected float camRotOffset;

	[SerializeField, Range(0.0f, 1.0f)]
	protected float camRotSmooth = 0.15f;

	public virtual void Start()
	{
		if (cam == null)
		{
			cam = GetComponent<Camera>();
			camTransform = transform;
			
			if (cam == null)
			{
				Debug.LogError("Missing error component: " + gameObject.name);
			}
		}
	}

	public virtual void LateUpdate()
	{
		if (isUpdating)
		{
			LookAtTarget();
		}
	}

	public void SetCamera(Camera _camera)
	{
		cam = _camera;
		camTransform = _camera.transform;
	}

	public virtual void Enable()
	{
		cam.enabled = true;
	}

	public virtual void Disable()
	{
		cam.enabled = false;
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

	[ContextMenu ("Look at Target")]
	public void LookAtTarget()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying && cam == null)
		{
			Start();
		}
#endif

		if (currentTarget && cam)
		{
			// Update Position
			if (posUpdate)
			{
				Vector3 pos = currentTarget.position + camPosOffset;
				Vector3 lerpPos = Vector3.SmoothDamp(camTransform.position, pos, ref currentCamVel, camPosSmooth);
				camTransform.position = posLerp ? lerpPos : pos;
			} 

			// Update Rotation
			if (rotUpdate)
			{
				Quaternion targetRot = Quaternion.LookRotation(currentTarget.position) * Quaternion.AngleAxis(camRotOffset, Vector3.forward);
				camTransform.rotation = rotLerp ? targetRot : Quaternion.Lerp(camTransform.rotation, targetRot, camRotSmooth);
			}
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		if (cameraCenter && camTransform)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(camTransform.position, cameraCenter.position);
		}
	}
#endif
}
