using UnityEngine;

namespace RPG_Input
{
    public class InputManager : MonoBehaviour
    {
		public static InputManager Instance { get; private set; }

        private Input_01 input; 

		[SerializeField] private InputType current;

        // Start is called before the first frame update
        void Awake()
        {
            if (Instance != null)
			{
                Destroy(gameObject);
			}
            else
			{
                Instance = this;

                InitializeInput();
			}
        }

		private void OnDestroy()
		{
            input?.Disable();
		}

		private void InitializeInput()
		{
            input = new Input_01();

            input.Enable();

            // UI Input
            input.UICustom.EnterNext.performed += (ctx) => { InputAction(InputType.Enter); };
            input.UICustom.BackPrevious.performed += (ctx) => { InputAction(InputType.Back); };
            input.UICustom.ElementTargetUp.performed += (ctx) => { InputAction(InputType.Element_Up); };
            input.UICustom.ElementTargetDown.performed += (ctx) => { InputAction(InputType.Element_Down); };
            input.UICustom.ElementTargetLeft.performed += (ctx) => { InputAction(InputType.Element_Left); };
            input.UICustom.ElementTargetRight.performed += (ctx) => { InputAction(InputType.Element_Right); };

            // Player Input
            input.Player.InspectProceed.performed += (ctx) => { InputAction(InputType.Inspect); };
            //input.Player.ExitReject.performed += (ctx) => { InputAction(InputType.Back); };

        }

        private void InputAction(InputType _input)
        {
            current = _input;

            switch (_input)
			{
                case InputType.Enter:
                    break;
                case InputType.Back:
                    ActionManager.Instance.ActionBack();
                    break;
                case InputType.Inspect:
                    break;
                case InputType.Element_Up:
                    break;
                case InputType.Element_Down:
                    break;
                case InputType.Element_Left:
                    break;
                case InputType.Element_Right:
                    break;
                default:
                    break;
			}
		}
    }
}


