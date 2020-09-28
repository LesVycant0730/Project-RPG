using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
                print("Input type: " + _input);
			}
        }
    }
}


