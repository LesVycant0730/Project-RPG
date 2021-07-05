using RPG_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

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
    [Header ("Buttons")]
    [SerializeField] private Selectable[] demoActionButtons;
    [SerializeField] private ButtonWrapper_CombatBase[] normalActionButtons;
    [SerializeField] private CombatUIHolder[] holders;
    [SerializeField] private List<CombatUIHolder> activeHolders = new List<CombatUIHolder>();
	#endregion

	#region Skill Button
    [Header ("Skill Button")]
	[SerializeField] private AssetReference skillButtonAsset;
    [SerializeField] private Transform skillButtonContent;
    [SerializeField] private List<ButtonWrapper_CombatSkill> skillButtonLists = new List<ButtonWrapper_CombatSkill>();
    private GameObject loadedSkillButtonAsset;
    #endregion

    #region Description Holder
    [Header ("Description")]
    [SerializeField] private Text textDescription;
	private StringBuilder descriptionBuilder = new StringBuilder();
    #endregion

    #region Combat Log Holder
    [Header("Combat Log")]
    [SerializeField, Range(1000, 6000)] private int maxCombatLogCharacters = 2000;
    [SerializeField] private Text combatLog;
	private StringBuilder combatLogBuilder = new StringBuilder();
    #endregion

    #region Character Status
    [Header ("Character Status")]
    [SerializeField] private PlayerStatusUI[] playerStatusUI;
    [SerializeField] private EnemyStatusUI[] enemyStatusUI;
	#endregion

	#region Combat Header
    [Header ("Combat Header")]
    [SerializeField] private Text textCombatTurn;
    #endregion

    #region Game Speed
    [SerializeField] private Text textGameSpeed;
	#endregion

#if UNITY_EDITOR
	private void OnValidate()
    {
        if (holders == null)
        {
            int actionLength = Util.GetEnumLength<Combat_Action>();

            holders = new CombatUIHolder[actionLength];

            for (int i = 0; i < actionLength; i++)
            {
                holders[i] = new CombatUIHolder((Combat_Action)i, null);
            }
        }
    }
#endif

	private void Awake()
	{
        OnCombatDefault();
	}

	// Used in slider UI
	public void OnSliderValueChanged(float value)
	{
        textGameSpeed.text = $"Game Speed: {value:f2}";
	}

    public void ToggleUIHolder(Combat_Action _actionType, bool _enabled, out bool _canToggle)
    {
        _canToggle = false;

        CombatUIHolder holder = holders.Single(x => x.ActionType == _actionType);

        if (holder != null)
        {
            // Disable previous active holder
            activeHolders.ForEach(x => x.Disable());

            // Clear previous holder list
            activeHolders.Clear();

            // Update active status on holder
            

            // Update current active holder list
            if (_enabled)
			{
                if (activeHolders.Contains(holder) == false)
				{
                    activeHolders.Add(holder);
				}

                holder.Enable();
            }
            else
			{
                activeHolders.Remove(holder);
                holder.Disable();
			}

			_canToggle = true;
        }
    }

	#region Description/Combat Log
	public void UpdateDescriptionBox(string _description = "")
	{
        if (string.IsNullOrEmpty(_description))
		{
            textDescription.text = string.Empty;
		}
        else
		{
            if (descriptionBuilder == null)
            {
                descriptionBuilder = new StringBuilder();
            }

            descriptionBuilder.Clear();
            descriptionBuilder.Append(_description);

            textDescription.text = descriptionBuilder.ToString();
        }
	}

    public void UpdateCombatLogBox(string _log = "")
    {
        if (string.IsNullOrEmpty(_log))
        {
            return;
        }
        else
        {
            if (combatLogBuilder == null)
                combatLogBuilder = new StringBuilder();

            if (combatLogBuilder.Length + _log.Length > maxCombatLogCharacters)
			{
                for (int i = 0; i < combatLogBuilder.Length; i++)
                {
                    if (combatLogBuilder.Length + _log.Length <= maxCombatLogCharacters)
					{
                        break;
					}
                    else
					{
                        char ch = combatLogBuilder[i];

                        if (ch == '\n')
                        {
                            combatLogBuilder.Remove(0, i);
                        }
                    }
				}
			}

            combatLogBuilder.Append(_log.Replace("_", " "));

            combatLog.text = combatLogBuilder.ToString();
        }
    }

    public void ClearCombatLog()
	{
        combatLogBuilder = new StringBuilder();
        combatLog.text = string.Empty;
    }

    public void ClearDescriptionBox()
	{
        textDescription.text = string.Empty;
	}
    #endregion

    #region Combat Event
    public void OnCombatExit()
    {
        skillButtonAsset.ReleaseAsset();

        skillButtonLists.ForEach(x =>
        {
            if (x != null)
            {
                Destroy(x.gameObject);
            }
        });

        skillButtonLists.Clear();
    }

    public void OnCombatToggle(bool _enabled)
    {
        Array.ForEach(demoActionButtons, x => x.interactable = _enabled);
    }

    public void OnCombatDefault()
    {
        demoActionButtons[0].interactable = true;
        demoActionButtons[1].interactable = false;
        demoActionButtons[2].interactable = false;
        textCombatTurn.gameObject.SetActive(false);
    }

    public void OnTurnUpdate(RPGCharacter _char, RPG_Party _party)
	{
        // Update combat turn text
        textCombatTurn.gameObject.SetActive(_char.Character != null);
        textCombatTurn.color = _party == RPG_Party.Ally ? Color.blue : Color.red;
        textCombatTurn.text = _party == RPG_Party.Ally ? "Player Turn" : "Enemy Turn";

        // Update combat demo action buttons 
        Array.ForEach(demoActionButtons, x => x.interactable = _party == RPG_Party.Ally);

        // Update skill button list based on character available skill lists.
        if (_party == RPG_Party.Ally)
		{
            UpdateSkillButtons(_char);
		}
	}
	#endregion

	private async void UpdateSkillButtons(RPGCharacter _char)
	{
        List<Skill_Name> skillList = _char.CharacterStat.GetSkillList();

        if (!skillList.IsNullOrEmpty())
        {
            // Load skill button asset
            if (loadedSkillButtonAsset == null)
                loadedSkillButtonAsset = await UtilAddressables.LoadAsset<GameObject>(skillButtonAsset, null);

            for (int i = 0; i < skillList.Count; i++)
            {
                // Reuse existing buttons
                if (i < skillButtonLists.Count)
                {
                    skillButtonLists[i].SetSkill(skillList[i]);
                }
                else
                {
                    ButtonWrapper_CombatSkill newButton = Instantiate(loadedSkillButtonAsset, skillButtonContent).GetComponent<ButtonWrapper_CombatSkill>();
                    newButton.SetSkill(skillList[i]);
                    skillButtonLists.Add(newButton);
                }
            }
        }
    }

    public void DisableActionButtons()
	{
        ToggleUIHolder(Combat_Action.Demo, false, out bool _canToggle);
        Array.ForEach(demoActionButtons, x => x.interactable = false);
    }

    public void UpdateCharacterInfo(RPGCharacter _character)
	{
        int characterIndex = _character.PartyIndex;

        if (characterIndex == -1)
            throw new Exception($"Negative Character Index");

        switch (_character.CharacterParty)
		{
            case RPG_Party.Ally:
                Debug.Assert(playerStatusUI.Length > characterIndex, $"Expected Player status UI of index: {characterIndex} while the length is only {playerStatusUI.Length}");
                playerStatusUI[characterIndex].UpdateInfo(_character.CharacterStatInfo);
                break;
            case RPG_Party.Enemy:
                Debug.Assert(enemyStatusUI.Length > characterIndex, $"Expected Enemy status UI of index: {characterIndex} while the length is only {enemyStatusUI.Length}");
                enemyStatusUI[characterIndex].UpdateInfo(_character.CharacterStatInfo);
                break;
		}
	}
}
