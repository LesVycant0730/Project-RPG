using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX_Base : MonoBehaviour
{
    [SerializeField] protected VisualEffect vfx;

    protected virtual void Awake()
    {
        //vfx = GetComponent<VisualEffect>();
    }

    void Update()
    {
        // Kill
        if (vfx.aliveParticleCount == 0)
        {
            vfx.Stop();
        }
    }
}
