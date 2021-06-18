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
    [SerializeField] private GameObject model;
    [SerializeField] private Animator anim;
    [SerializeField] private bool isUsing = true;

    public Character_ID ID => id;
    public RPG_Party Party => party;
    public GameObject Model => model;
    public Animator Anim => anim;
    public bool IsUsing => isUsing;

    public Character(Character_ID _id = Character_ID.NULL, RPG_Party _party = RPG_Party.Neutral)
    {
        id = _id;
        party = _party;
        model = null;
        anim = null;
        isUsing = true;
    }

    public Character(Character_ID _id, RPG_Party _party, GameObject _model)
    {
        id = _id;
        party = _party;
        model = _model;
        anim = _model.GetComponent<Animator>();
        isUsing = true;
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
        Destroy();
    }

    private void Destroy()
    {
        if (model)
        {
            Addressables.ReleaseInstance(model);
            model = null;
        }
    }
}
