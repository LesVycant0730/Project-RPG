//#define VfxDebug
using UnityEngine;
using UnityEngine.VFX;

public enum VFX_Type
{
    Standard, Mesh
}

[DisallowMultipleComponent]
public class VFX_Base : MonoBehaviour
{
    [SerializeField] protected VisualEffect vfx;
    [SerializeField] private int id_Lifetime;
    protected float currentTime;

    protected virtual void Awake()
    {
        vfx = GetComponent<VisualEffect>();
        id_Lifetime = Shader.PropertyToID("Total Lifetime");
    }

    protected virtual void OnEnable()
	{
        currentTime = 0.0f;
	}

    protected virtual void Update()
    {
#if VfxDebug
#else
        // Disable when expired and zero particle count
        if (currentTime > vfx.GetFloat(id_Lifetime) && vfx.aliveParticleCount == 0)
        {
            Destroy(gameObject);
        }
        else
            currentTime += Time.deltaTime;
#endif
    }

}
