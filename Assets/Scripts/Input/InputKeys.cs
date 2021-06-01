using UnityEngine;

namespace RPG_Input
{
    public enum InputType
	{
        Accept,
        Back,
        Inspect,
        None
	}

    public static class InputKeys
    {
        // Accept/Proceed Key
        private static readonly KeyCode AcceptKey = KeyCode.Return;
        private static readonly KeyCode AcceptKey2 = KeyCode.Space;
        private static readonly KeyCode AcceptKey3 = KeyCode.F;

        // Reject/Back Key
        private static readonly KeyCode Back = KeyCode.Escape;

        // Inspect Key
        private static readonly KeyCode Inspect = KeyCode.E;

        public static bool InputUpdate(out InputType _type)
		{
            if (GetAcceptKey())
			{
                _type = InputType.Accept;
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
            return Input.GetKeyDown(AcceptKey) || Input.GetKeyDown(AcceptKey2) || Input.GetKeyDown(AcceptKey3);
		}

        public static bool GetBackKey()
		{
            return Input.GetKeyDown(Back);
		}

        public static bool GetInspectKey()
		{
            return Input.GetKeyDown(Inspect);
		}
    }
}

