using UnityEngine;


namespace GameDevUtils.CharacterController2
{


	[CreateAssetMenu(menuName = "GameDevUtils/CharacterController/SlideMovement")]
	public class SlideMovement : CharacterBase
	{

		[SerializeField] RangeBy movementRange, rotationRange;

		float          m_jumpTimeStamp   = 0;
		readonly float m_minJumpInterval = 0.25f;
		bool           m_jumpInput       = false;
		readonly float m_interpolation   = 0.01f;
		readonly float m_walkScale       = 0.33f;
		float          currentRotation;

		public override void Init(GameDevBehaviour controller)
		{
			base.Init(controller);
			currentRotation = animator.transform.localRotation.y;
		}

		protected override void DoUpdate()
		{
			base.DoUpdate();
			if (!m_jumpInput && characterInput.Jump)
			{
				m_jumpInput = true;
			}
		}

		protected override void Movement()
		{
			float v = characterInput.Vertical;
			float h = characterInput.Horizontal;
			if (characterInput.Walk)
			{
				v *= m_walkScale;
				h *= m_walkScale;
			}

			currentVelocity = behaviour.transform.forward * v;
			var desirePosition = currentVelocity;
			desirePosition.z *= m_interpolation * characterInput.MoveSpeed;
			desirePosition.x =  h;
			desirePosition   += behaviour.transform.position;
			desirePosition.x =  movementRange.Clamp(desirePosition.x);
			rigidbody.MovePosition(desirePosition);
		}

		protected override void Rotation()
		{
			currentRotation += characterInput.RotationSpeed * characterInput.Horizontal;
			if (characterInput.Horizontal == 0)
				currentRotation = Mathf.MoveTowards(currentRotation, rotationRange.@from, characterInput.RotationSpeed / 2);
			currentRotation                  = rotationRange.Clamp(currentRotation);
			animator.transform.localRotation = Quaternion.Euler(0, currentRotation, 0);
		}

		protected override void UpdateAnimator(Vector3 velocity)
		{
			if (isGrounded)
			{
				animator.SetFloat("Value", velocity.magnitude);
			}
			else
			{
				animator.SetBool("Jump", !isGrounded);
			}
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

	}


}