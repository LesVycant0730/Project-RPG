using UnityEngine;

namespace RPG_Input
{
    public class InputManager : MonoBehaviour
    {
		public static InputManager Instance { get; private set; }

		private InputType _input;

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
			}
        }

        // Update is called once per frame
        void Update()
        {
            if (InputKeys.InputUpdate(out _input))
			{
                InputAction();
                print("Input type: " + _input);
			}
        }

        private void InputAction()
        {
            switch (_input)
			{
                case InputType.Accept:
                    break;
                case InputType.Back:
                    ActionManager.Instance.Action_Back();
                    break;
                case InputType.Inspect:
                    break;
                default:
                    break;
			}
		}
    }
}


