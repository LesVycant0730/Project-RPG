using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class VFX_Mesh : VFX_Base
{
	private struct MeshUpdateJob : IJobParallelFor
	{
		// Mesh reference
		public NativeArray<Vector3> meshVertices;

		// Target vertices
		public NativeArray<Vector3> targetVertices;

		public void Execute(int index)
		{
			meshVertices[index] = targetVertices[index];

			// Update vertices here
		}
	}

	[SerializeField] private SkinnedMeshRenderer skin;

	// Job System
	private NativeArray<Vector3> meshVertices, targetVertices;
	private Mesh cloneMesh;

	private JobHandle meshUpdateJobHandle;
	private MeshUpdateJob meshUpdateJob;

	// Unused, ori
    //private CancellationTokenSource source;
    //private CancellationToken token;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

		// Set the updated mesh
		cloneMesh = new Mesh();
		cloneMesh.MarkDynamic();
	}

	private void OnEnable()
	{
		vfx.SetMesh("Mesh", cloneMesh);
	}

	private void OnDisable()
	{
		//if (source != null)
		//{
		//	source.Cancel();
		//	source.Dispose();
		//}

		// Dispose all native arrays to prevent memory leak
		if (meshVertices.IsCreated) meshVertices.Dispose();
		if (targetVertices.IsCreated) targetVertices.Dispose();
	}

	public void SetMesh(SkinnedMeshRenderer _mesh)
	{
		skin = _mesh;
	}

	private void SetMesh()
	{
		Mesh newMesh = new Mesh();

		// Mark Dynamic the mesh
		newMesh.MarkDynamic();

		// Bake the mesh from skinned reference
		skin.BakeMesh(newMesh);

		if (cloneMesh.vertices.Length != newMesh.vertices.Length)
			cloneMesh.vertices = new Vector3[newMesh.vertices.Length];

		targetVertices = new NativeArray<Vector3>(newMesh.vertices, Allocator.TempJob);
		meshVertices = new NativeArray<Vector3>(cloneMesh.vertices, Allocator.TempJob);
	}

	private void Update()
	{
		// Bake Mesh and set vertices array
		SetMesh();

		// Create new mesh job
		meshUpdateJob = new MeshUpdateJob()
		{
			meshVertices = meshVertices,
			targetVertices = targetVertices
		};

		// Schedule the job and divide it to batches
		meshUpdateJobHandle = meshUpdateJob.Schedule(targetVertices.Length, 64);
	}

	private void LateUpdate()
	{
		// Complete the job handle, must run in at LateUpdate
		meshUpdateJobHandle.Complete();

		// Set vertices
		cloneMesh.SetVertices(meshUpdateJob.targetVertices);

		targetVertices.Dispose();
		meshVertices.Dispose();
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
