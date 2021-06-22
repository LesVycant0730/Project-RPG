using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX_Base : MonoBehaviour
{
    [SerializeField] protected VisualEffect vfx;

    protected virtual void Awake()
    {
        vfx = GetComponent<VisualEffect>();
    }

    protected virtual void OnEnable()
	{
        VFXManager.AddVFX(gameObject);
	}

    protected virtual void OnDisable()
	{
        //VFXManager.addvfx
	}

    void Update()
    {
        // Kill
        //if (vfx.aliveParticleCount == 0)
        //{
        //    vfx.Stop();
        //}
    }
}
