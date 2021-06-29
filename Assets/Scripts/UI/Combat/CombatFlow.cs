using RPG_Data;
using System.Linq;
using UnityEngine;
using RoboRyanTron.SearchableEnum;

[CreateAssetMenu(fileName = "Combat Flow", menuName = "ScriptableObjects/Combat/Flow", order = 1)]
public sealed class CombatFlow : ScriptableObject
{
    [SerializeField, ConditionalHide(false)] private CombatActionFlow[] actionFlows;

    public bool HasActionFlow => actionFlows != null;

	public Combat_Action GetNextAction(Combat_Action _action, out bool _hasNextAction)
	{
        var nextAction = actionFlows.Single(x => x.GetCurrent() == _action).GetNext();

        _hasNextAction = nextAction != Combat_Action.NONE || nextAction == _action;

        return nextAction;
	}

    public Combat_Action GetPrevAction(Combat_Action _action, out bool _hasPrevAction)
    {
        var prevAction = actionFlows.Single(x => x.GetCurrent() == _action).GetPrevious();

        _hasPrevAction = prevAction != Combat_Action.NONE || prevAction == _action;

        return prevAction;
    }

    public void ToDefault()
	{
#if UNITY_EDITOR
        var actions = Util.GetTypeElements<Combat_Action>();

        actionFlows = new CombatActionFlow[actions.Count()];

        for (int i = 0; i < actionFlows.Length; i++)
		{
            actionFlows[i] = new CombatActionFlow(actions.ElementAt(i));
		}
#endif
    }

    public void Clear()
	{
#if UNITY_EDITOR
        actionFlows = null;
#endif
    }
}

[System.Serializable]
public sealed class CombatActionFlow
{
    [SerializeField, SearchableEnum] private Combat_Action current;
    [SerializeField, SearchableEnum] private Combat_Action next = Combat_Action.NONE;
    [SerializeField, SearchableEnum] private Combat_Action previous = Combat_Action.Default;

    public Combat_Action GetCurrent() => current;
    public Combat_Action GetNext() => next;
    public Combat_Action GetPrevious() => previous;

    public CombatActionFlow(Combat_Action _default)
	{
        current = _default;
	}
}
