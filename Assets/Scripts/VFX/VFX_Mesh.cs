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

	// Unused, ori
    //private CancellationTokenSource source;
    //private CancellationToken token;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

		meshReference = new Mesh();
		meshReference.MarkDynamic();

		// Set the updated mesh
		meshTarget = new Mesh();
		meshTarget.MarkDynamic();

		vfx.SetMesh("Mesh", meshTarget);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		//if (source != null)
		//{
		//	source.Cancel();
		//	source.Dispose();
		//}

		// Dispose all native arrays to prevent memory leak
		Dispose();
	}

	public void SetMesh(SkinnedMeshRenderer _mesh)
	{
		skin = _mesh;
	}

	private void SetMesh()
	{
		// Dispose before setup
		Dispose();

		// Bake the mesh from skinned reference
		if (skin != null)
			skin.BakeMesh(meshReference);

		if (meshTarget.vertices.Length != meshReference.vertices.Length)
			meshTarget.vertices = new Vector3[meshReference.vertices.Length];

		// Set vertices target based on mesh reference
		verticesTarget = new NativeArray<Vector3>(meshReference.vertices, Allocator.Persistent);

		// Set current vertices based on target mesh 
		verticesCurrent = new NativeArray<Vector3>(meshTarget.vertices, Allocator.Persistent);
	}

	protected override void Update()
	{
		base.Update();

		// Bake Mesh and set vertices array
		SetMesh();

		// Create new mesh job
		meshUpdateJob = new MeshUpdateJob()
		{
			verticesCurr = verticesCurrent,
			verticesTar = verticesTarget
		};

		// Schedule the job and divide it to batches
		meshUpdateJobHandle = meshUpdateJob.Schedule(verticesTarget.Length, 64);
	}

	private void LateUpdate()
	{
		// Complete the job handle, must run in at LateUpdate
		meshUpdateJobHandle.Complete();

		// Set vertices
		meshTarget.SetVertices(meshUpdateJob.verticesTar);

		Dispose();
	}

	private void Dispose()
	{
		if (verticesCurrent.IsCreated) verticesCurrent.Dispose();
		if (verticesTarget.IsCreated) verticesTarget.Dispose();
	}

	// Original async code
	//private async void MeshUpdate()
	//{
	//	source = new CancellationTokenSource();
	//	token = source.Token;

	//	while (!token.IsCancellationRequested && gameObject.activeSelf && skin != null)
	//	{
	//		// Bake new mesh
	//		Mesh newMesh = new Mesh();
	//		skin.BakeMesh(newMesh);

	//		// Clone a set of vertices to a new mesh and set it to vfx graph
	//		vfx.SetMesh("Mesh", new Mesh() { vertices = newMesh.vertices });

	//		// Update delay
	//		await Task.Delay(200);
	//	}

	//	source.Dispose();
	//}
}
