using UnityEngine;


namespace GameDevUtils.CharacterController2
{


	[CreateAssetMenu(menuName = "GameDevUtils/CharacterController/FreeMovement")]
	public class FreeMovement : CharacterBase
	{

		[SerializeField] RangeBy movementRange, rotationRange;
		float                    currentRotation;
		float                    m_jumpTimeStamp   = 0;
		readonly float           m_minJumpInterval = 0.25f;
		bool                     m_jumpInput       = false;
		readonly float           m_interpolation   = 0.01f;
		readonly float           m_walkScale       = 0.33f;

		public override void Init(GameDevBehaviour controller)
		{
			base.Init(controller);
			rotationRange.@from = controller.transform.eulerAngles.y;
		}

		protected override void AirMovement()
		{
			bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;
			if (jumpCooldownOver && isGrounded && m_jumpInput)
			{
				m_jumpTimeStamp = Time.time;
				rigidbody.AddForce(Vector3.up * characterInput.JumpForce, ForceMode.Impulse);
			}
		}

		protected override void Movement()
		{
			Vector3 desiredVelocity = behaviour.transform.forward * ((Mathf.Abs(characterInput.Vertical) + Mathf.Abs(characterInput.Horizontal) > 0.1f) ? characterInput.MoveSpeed : 0);
			desiredVelocity.y  = rigidbody.velocity.y;
			rigidbody.velocity = desiredVelocity;
			currentVelocity    = desiredVelocity;
		}

		protected override void Rotation()
		{
			if (Mathf.Abs(characterInput.Vertical) > 0.1f || Mathf.Abs(characterInput.Horizontal) > 0.1f)
			{
				currentRotation = Mathf.Atan2(characterInput.Horizontal, characterInput.Vertical);
				currentRotation = Mathf.Rad2Deg * currentRotation;
				currentRotation = rotationRange.Clamp(currentRotation);
				var targetRotation = Quaternion.Euler(Vector3.up * (currentRotation));
				behaviour.transform.rotation = targetRotation;
			}

			//controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, characterInput.RotationSpeed * Time.deltaTime);
		}

		protected override void UpdateAnimator(Vector3 velocity)
		{
			if (isGrounded)
			{
				animator.SetFloat("Value", velocity.magnitude);
			}
			
		}

	}


}