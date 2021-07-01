using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class VFX_Mesh : VFX_Base
{
	private struct MeshUpdateJob : IJobParallelFor
	{
		// Mesh reference
		public NativeArray<Vector3> verticesCurr;

		// Target vertices
		public NativeArray<Vector3> verticesTar;

		public void Execute(int index)
		{
			verticesCurr[index] = verticesTar[index];

			// Update vertices here
		}
	}

	[SerializeField] private SkinnedMeshRenderer skin;

	// Job System
	private NativeArray<Vector3> verticesCurrent, verticesTarget;
	private Mesh meshReference, meshTarget;

	private JobHandle meshUpdateJobHandle;
	private MeshUpdateJob meshUpdateJob;

	private bool isJobInitialized = false;

	// Particle Property
	[SerializeField] private int id_Release;

	// Start is called before the first frame update
	protected override void Awake()
    {
        base.Awake();

		id_Release = Shader.PropertyToID("ReleaseEffect");

		meshReference = new Mesh();
		meshReference.MarkDynamic();

		// Set the updated mesh
		meshTarget = new Mesh();
		meshTarget.MarkDynamic();

		vfx.SetMesh("Mesh", meshTarget);
	}

	private void OnDestroy()
	{
		Dispose();
	}

	public void SetMesh(SkinnedMeshRenderer _mesh)
	{
		skin = _mesh;
	}

	private void MeshUpdate()
	{
		// Dispose before setup
		Dispose();

		// Bake the mesh from skinned reference
		skin.BakeMesh(meshReference);

		meshTarget.vertices = new Vector3[meshReference.vertices.Length];

		// Set vertices target based on mesh reference
		verticesTarget = new NativeArray<Vector3>(meshReference.vertices, Allocator.Persistent);

		// Set current vertices based on target mesh 
		verticesCurrent = new NativeArray<Vector3>(meshTarget.vertices, Allocator.Persistent);
	}

	protected override void Update()
	{
		base.Update();

		if (currentTime > 0.5f)
		{
			vfx.SetBool("ReleaseEffect", true);
		}

		// Bake Mesh and set vertices array
		MeshUpdate();

		// Create new mesh job
		meshUpdateJob = new MeshUpdateJob()
		{
			verticesCurr = verticesCurrent,
			verticesTar = verticesTarget
		};

		// Schedule the job and divide it to batches
		meshUpdateJobHandle = meshUpdateJob.Schedule(verticesTarget.Length, 64);

		// TEMPORARY SOLUTION!!!
		// Not sure why LateUpdate would run first before Update when instantiated
		// Update job status to allow job reference to be accessed in LateUpdate
		isJobInitialized = true;
	}

	private void LateUpdate()
	{
		if (isJobInitialized)
		{
			// Complete the job handle
			meshUpdateJobHandle.Complete();

			// Set vertices
			meshTarget.SetVertices(meshUpdateJob.verticesTar);

			// Dispose
			if (verticesCurrent.IsCreated) verticesCurrent.Dispose();
			if (verticesTarget.IsCreated) verticesTarget.Dispose();
		}

		isJobInitialized = false;
	}

	// Dispose all native arrays to prevent memory leak
	private void Dispose()
	{
		// Complete the job handle
		meshUpdateJobHandle.Complete();

		if (verticesCurrent.IsCreated) verticesCurrent.Dispose();
		if (verticesTarget.IsCreated) verticesTarget.Dispose();
	}
}
