using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RPGCharacterManager : MonoBehaviour
{
    [SerializeField]
    private RPGParty[] rpgParty = new RPGParty[]
    {
        new RPGParty(RPGParty.RPGPartyType.Ally),
        new RPGParty(RPGParty.RPGPartyType.Enemy),
        new RPGParty(RPGParty.RPGPartyType.Neutral)
    };

    public RPGParty CurrentParty { get; private set; }

    public RPGParty GetParty(RPGParty.RPGPartyType _type)
	{
        return rpgParty.SingleOrDefault(rpgParty => rpgParty.PartyType == _type);
	}

    public void AddActionToParty(RPGParty.RPGPartyType _type, Action _action)
	{
        RPGParty party = GetParty(_type);

        if (party != null)
        {
            party.AddActionToParty(_action);
        }
    }

    public void ActivateParty(RPGParty.RPGPartyType _type)
	{
        RPGParty party = GetParty(_type);

        if (party != null)
		{
            CurrentParty = party;

            CurrentParty.SetPartyActive();
		} 
	}
}
