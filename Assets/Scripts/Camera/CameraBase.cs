using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
	private Camera cam;
	private Transform camTransform;

	[Header ("Camera Update Settings")]
	[SerializeField] private bool isUpdating = false;
	[SerializeField] protected Transform current, target;
	[SerializeField] private Transform cameraCenter;

	[Header("Camera Position Settings")]
	[SerializeField] private bool posUpdate = true;
	[SerializeField] private bool posLerp = false;
	[SerializeField] protected Vector3 camPosDefault;
	[SerializeField] protected Vector3 camPosOffset = Vector3.zero;
	[SerializeField] private Vector3 currentCamVel, targetCamVel = Vector3.zero;
	[SerializeField, Range(0.0f, 1.0f)] 
	protected float camPosSmooth = 0.15f;

	[Header("Camera Rotation Settings")]
	[SerializeField] private bool rotUpdate = true;
	[SerializeField] private bool rotLerp = false;
	[SerializeField] protected Quaternion camRotDefault;
	[SerializeField] protected float camRotOffset;
	[SerializeField, Range(0.0f, 1.0f)]
	protected float camRotSmooth = 0.15f;

	protected virtual void Awake()
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

	protected virtual void LateUpdate()
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

	public virtual void SetCameraCurrent(Transform _transform)
	{
		current = _transform;
	}

	public virtual void SetCameraTarget(Transform _transform)
	{
		target = _transform;
	}

	[ContextMenu ("Look at Target")]
	public void LookAtTarget()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying && cam == null)
		{
			Awake();
		}
#endif

		// Update Position to look at target object
		if (target)
		{
			// Update Position
			if (posUpdate)
			{
				Vector3 pos = target.position + camPosOffset;
				Vector3 lerpPos = Vector3.SmoothDamp(camTransform.position, pos, ref targetCamVel, camPosSmooth);

				camTransform.LookAt(lerpPos);
			}

			// Update Rotation
			if (rotUpdate)
			{
				Quaternion targetRot = Quaternion.LookRotation(target.position) * Quaternion.AngleAxis(camRotOffset, Vector3.forward);
				camTransform.rotation = rotLerp ? targetRot : Quaternion.Lerp(camTransform.rotation, targetRot, camRotSmooth);
			}
		}
		// Update Position to current focused object without target
		else if (current)
		{
			// Update Position
			if (posUpdate)
			{
				Vector3 pos = current.position + camPosDefault;
				Vector3 lerpPos = Vector3.SmoothDamp(camTransform.position, pos, ref currentCamVel, camPosSmooth);

				camTransform.position = posLerp ? lerpPos : pos;
			}

			// Reset back to default rotation
			camTransform.rotation = camRotDefault;
		}
		else
		{
			if (posUpdate)
			{
				// Set camera to default position and rotation
				camTransform.SetPositionAndRotation(camPosDefault, camRotDefault);
			}
			
			// Reset camera velocity
			currentCamVel = Vector3.zero;
			targetCamVel = Vector3.zero;
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

	[ContextMenu ("Set Default Position and Rotation")]
	private void SetDefaultPositionAndRotation()
	{
		camPosDefault = transform.position;
		camRotDefault = transform.rotation;
	}
#endif
}
