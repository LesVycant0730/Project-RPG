using UnityEngine;
using UnityEngine.VFX;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;

public class VFX_Mesh : VFX_Base
{
    [SerializeField] private SkinnedMeshRenderer skin;
    private CancellationTokenSource source;
    private CancellationToken token;

    private Mesh meshTemp;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

	private void OnEnable()
	{
        StartCoroutine(MeshUpdate());
	}

	private void OnDisable()
	{
        source?.Cancel();
        source?.Dispose();
	}

	//private async Task MeshUpdate()
	//{
 //       source = new CancellationTokenSource();
 //       token = source.Token;

 //       while (!token.IsCancellationRequested && gameObject.activeSelf /*&& vfx.aliveParticleCount > 0*/ && skin != null)
	//	{
 //           Mesh newMesh = new Mesh();
 //           skin.BakeMesh(newMesh);

 //           Vector3[] vertices = newMesh.vertices;
 //           Mesh clone = new Mesh();
 //           clone.vertices = vertices;

 //           // Clone a set of vertices and set it to vfx graph
 //           vfx.SetMesh("Mesh", clone);

 //           print("yay");

 //           // Update delay
 //           await Task.Delay(1000);
 //       }

 //       print("Dispose");
 //       source.Dispose();
 //   }

    private IEnumerator MeshUpdate()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);

		while (gameObject.activeSelf /*&& vfx.aliveParticleCount > 0*/)
		{
			Mesh newMesh = new Mesh();
            skin.BakeMesh(newMesh, true);

			// Clone a set of vertices and set it to vfx
			vfx.SetMesh("Mesh", new Mesh() { vertices = newMesh.vertices });

			// Update delay
			yield return delay;
		}

		yield return null;
    }

	public void SetMesh(SkinnedMeshRenderer _mesh)
	{
        skin = _mesh;
	}

}
