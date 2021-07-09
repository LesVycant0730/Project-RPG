using AnimationTypes;
using GameInfo;
using RPG_Data;
using UnityEngine;

public class DescriptionInfoLibrary : BaseLibrary
{
    public static DescriptionInfoLibrary Instance { get; private set; }

    [SerializeField] private InfoDescription descriptionInfoSO;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        SetupLibrary();
    }

    protected override void SetupLibrary()
	{
        base.SetupLibrary();
	}

    public static string GetDescription(Combat_Action _type)
    {
        if (Instance)
		{
            return Instance.descriptionInfoSO.GetDescription(_type);
		}

        return string.Empty;
    }

    public static string GetDescription(CombatAnimationStatus _type)
    {
        if (Instance)
        {
            return Instance.descriptionInfoSO.GetDescription(_type);
        }

        return string.Empty;
    }

    public static string GetCombatLog(CombatLogType _type, params object[] _params)
	{
        if (Instance)
        {
            return Instance.descriptionInfoSO.GetLog(_type, _params);
        }

        return string.Empty;
    }
}
