using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class VFX_Mesh : VFX_Base
{
    [SerializeField] private SkinnedMeshRenderer skin;
    private CancellationTokenSource source;
    private CancellationToken token;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

	private void OnEnable()
	{
        MeshUpdate();
	}

	private void OnDisable()
	{
        source?.Cancel();
        source?.Dispose();
	}

	private async void MeshUpdate()
	{
		source = new CancellationTokenSource();
		token = source.Token;

		while (!token.IsCancellationRequested && gameObject.activeSelf && skin != null)
		{
			// Bake new mesh
			Mesh newMesh = new Mesh();
			skin.BakeMesh(newMesh);

			// Clone a set of vertices to a new mesh and set it to vfx graph
			vfx.SetMesh("Mesh", new Mesh() { vertices = newMesh.vertices });

			// Update delay
			await Task.Delay(200);
		}

		source.Dispose();
	}

	public void SetMesh(SkinnedMeshRenderer _mesh)
	{
        skin = _mesh;
	}

}
