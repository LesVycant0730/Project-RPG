using RoboRyanTron.SearchableEnum;
using RPG_Data;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class Character
{
    [SerializeField, SearchableEnum] private Character_ID id = Character_ID.NULL;
    [SerializeField, SearchableEnum] private RPG_Party party = RPG_Party.Ally;

    // The base or root object of the character holding everything
    [SerializeField] private GameObject baseObject;
    // The character model renderer
    [SerializeField] private SkinnedMeshRenderer model;
    // Is animator
    [SerializeField] private Animator anim;
    // The center position of the model for spawning VFX
    [SerializeField] private Vector3 modelCenter;
    // Is the character using in any place
    [SerializeField] private bool isUsing = true;
    // Character controller for running action, will only exist through AddComponent when constructing
    [SerializeField] private CharacterActionController controller;

    public Character_ID ID => id;
    public RPG_Party Party => party;
    public GameObject BaseObject => baseObject;
    public SkinnedMeshRenderer Model => model;
    public Animator Anim => anim;
    public Vector3 ModelCenter => modelCenter;
    public bool IsUsing => isUsing;

    public Character(Character_ID _id = Character_ID.NULL, RPG_Party _party = RPG_Party.Neutral)
    {
        id = _id;
        party = _party;
        model = null;
        anim = null;
        isUsing = true;
    }

    public Character(Character_ID _id, RPG_Party _party, GameObject _go)
    {
        id = _id;
        party = _party;
        baseObject = _go;
        model = _go.GetComponentInChildren<SkinnedMeshRenderer>();
		isUsing = true;
        anim = _go.GetComponent<Animator>();
        modelCenter = model.transform.position;

		// Add character action controller
        controller = _go.GetComponent<CharacterActionController>();
        if (controller == null)
            controller = _go.AddComponent<CharacterActionController>();

        controller.Setup(this);
    }

    public bool IsSameCharacter(Character_ID _id)
    {
        return id == _id;
    }

    public bool IsRegistered()
	{
        return model != null && IsUsing;
	}

    public void Enable()
    {
        isUsing = true;
    }

    public void Disable()
    {
        isUsing = false;
    }

    public void CombatAnimate(AnimationTypes.CombatAnimationStatus _status)
	{
        if (anim)
		{
            CombatAnimationManager.UpdateAnimation(anim, _status);
		}
	}

    public void Resume()
    {
        if (anim)
        {
            anim.speed = 1.0f;
        }
    }

    public void Pause()
    {
        if (anim)
        {
            anim.speed = 0;
        }
    }

    public void Default()
    {
        isUsing = false;
        anim = null;
        model = null;
        controller = null;
        Destroy();
    }

    private void Destroy()
    {
        if (baseObject)
        {
            Addressables.ReleaseInstance(baseObject);
            baseObject = null;
        }
    }
}
