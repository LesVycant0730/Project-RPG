using RPG_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CombatUICanvas : MonoBehaviour
{
    // Only can set in inspector
    [Serializable]
    protected class CombatUIHolder
    {
        [SerializeField] protected Combat_Action actionType;
        [SerializeField] protected GameObject holder;

        public Combat_Action ActionType => actionType;
        public GameObject Holder => holder;

        public CombatUIHolder(Combat_Action _action, GameObject _holder)
        {
            actionType = _action;
            holder = _holder;
        }

        public void Enable()
		{
            if (Holder)
			{
                holder.SetActive(true);
			}
		}

        public void Disable()
		{
            if (Holder)
			{
                holder.SetActive(false);
			}
		}
    }

	#region Button Holder
	[SerializeField] private ButtonWrapper_CombatBase[] normalActionButtons;

    [SerializeField] private CombatUIHolder[] holders;
    [SerializeField] private List<CombatUIHolder> activeHolders = new List<CombatUIHolder>();
    #endregion

    #region Description Holder
    [SerializeField] private Text descriptionText;
	private StringBuilder descriptionBuilder = new StringBuilder();
	#endregion


	private void OnValidate()
    {
        if (holders == null)
        {
            int actionLength = Utility.GetEnumLength<Combat_Action>();

            holders = new CombatUIHolder[actionLength];

            for (int i = 0; i < actionLength; i++)
            {
                holders[i] = new CombatUIHolder((Combat_Action)i, null);
            }
        }
    }

    public void ToggleUIHolder(Combat_Action _actionType, bool _enabled, out bool CanToggle)
    {
        CanToggle = false;

        CombatUIHolder holder = holders.Single(x => x.ActionType == _actionType);

        if (holder != null)
        {
            // Disable previous active holder
            activeHolders.ForEach(x => x.Disable());

            // Clear previous holder list
            activeHolders.Clear();

            // Update active status on holder
            holder.Enable();

            // Update current active holder list
            if (_enabled)
			{
                if (activeHolders.Contains(holder) == false)
				{
                    activeHolders.Add(holder);
				}
			}
            else
			{
                activeHolders.Remove(holder);
			}

            CanToggle = true;
        }
    }

    public void UpdateDescriptionBox(string _description = "")
	{
        if (_description == string.Empty)
		{
            descriptionText.text = string.Empty;
		}
        else
		{
            if (descriptionBuilder == null)
            {
                descriptionBuilder = new StringBuilder();
            }

            descriptionBuilder.Clear();
            descriptionBuilder.Append(_description);

            descriptionText.text = descriptionBuilder.ToString();
        }
	}

    public void ClearDescriptionBox()
	{
        descriptionText.text = string.Empty;
	}
}
