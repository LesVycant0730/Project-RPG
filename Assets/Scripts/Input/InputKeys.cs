using UnityEngine.InputSystem;

namespace RPG_Input
{
    public enum InputType
	{
        Enter,
        Back,
        Inspect,
        None
	}

    public static class InputKeys
    {
        // Accept/Proceed Key
        private static readonly Key AcceptKey = Key.Enter;
        private static readonly Key AcceptKey2 = Key.Space;
        private static readonly Key AcceptKey3 = Key.F;

        // Reject/Back Key
        private static readonly Key Back = Key.Escape;

        // Inspect Key
        private static readonly Key Inspect = Key.E;

        public static bool InputUpdate(out InputType _type)
		{
            if (GetAcceptKey())
			{
                _type = InputType.Enter;
			}
            else if (GetBackKey())
			{
                _type = InputType.Back;
            }
            else if (GetInspectKey())
			{
                _type = InputType.Inspect;
            }
            else
			{
                _type = InputType.None;
                return false;
			}

            return true;
        }

        public static bool GetAcceptKey()
		{
            return false;
            //return Input.GetKeyDown(AcceptKey) || Input.GetKeyDown(AcceptKey2) || Input.GetKeyDown(AcceptKey3);
		}

        public static bool GetBackKey()
		{
            return false;
           // return Input.GetKeyDown(Back);
		}

        public static bool GetInspectKey()
		{
            return false;
            //return Input.GetKeyDown(Inspect);
		}
    }
}

