using UnityEngine;


namespace GameDevUtils.CharacterController2
{


	[CreateAssetMenu(menuName = "GameDevUtils/CharacterController/DragInput")]
	public class DragInput : CharacterInput
	{

		private Vector3 prevPosition;

		public override void Update()
		{
			Vertical = 1;
			if (Input.GetMouseButtonDown(0))
			{
				prevPosition = Input.mousePosition;
			}

			if (Input.GetMouseButton(0))
			{
				Vector3 touchDelta    = Input.mousePosition - prevPosition; // screen touch delta
				var     positionDelta = touchDelta * Sensitivity;
				positionDelta.x /= Screen.width / 2f;
				Horizontal      =  positionDelta.x;
				prevPosition    =  Input.mousePosition;
			}
			else
			{
				Horizontal = 0;
			}
		}

	}


}