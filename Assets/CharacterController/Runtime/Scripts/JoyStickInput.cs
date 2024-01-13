using UnityEngine;


namespace GameDevUtils.CharacterController2
{


	[CreateAssetMenu(menuName = "GameDevUtils/CharacterController/JoyStickInput")]
	public class JoyStickInput : CharacterInput
	{

        Joystick joystick;
        private Joystick Joystick
        {
            get
            {
                if (!joystick)
                {
                    joystick = FindObjectOfType<Joystick>();
                }

                return joystick;
            }
        }

        public override void Update()
        {
            if(joystickStart)
            {
                Horizontal = -Joystick.Horizontal;
                Vertical = -Joystick.Vertical;
            }
          
        }

	}


}