using UnityEngine;



namespace GameDevUtils.CharacterController2
{


	public abstract class CharacterBase : ScriptableObject
	{

		[SerializeField] public CharacterInput characterInput;
		[SerializeField] protected LayerMask      groundLayerMask;
		[SerializeField] protected float          groundDistance;
		public                  bool           isGrounded = false;

		protected      GameDevBehaviour behaviour;
		protected      Collider         collider  => behaviour.Collider;
		protected      Rigidbody        rigidbody => behaviour.Rigidbody;
		protected      Animator         animator  => behaviour.Animator;
		protected      Vector3          currentVelocity;
		public virtual bool             CanControl { get; set; }

		public virtual void Init(GameDevBehaviour behaviour)
		{
			this.behaviour               =  behaviour;
			this.behaviour.OnInit        += Init;
			this.behaviour.OnFixedUpdate += DoFixedUpdate;
			this.behaviour.OnUpdate      += DoUpdate;
		}

		protected virtual void Init()
		{
			CanControl = false;
			characterInput.joystickStart = false;
		}


		protected virtual void DoUpdate()
		{
			animator.speed = characterInput.AnimationMultiplier;
			characterInput.Update();
		}

		protected virtual void DoFixedUpdate()
		{
			if (CanControl)
			{
				//CheckGrounded();
				Movement();
				Rotation();
				//AirMovement();
			}
			else
			{
				currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, Time.deltaTime);
			}

			UpdateAnimator(currentVelocity);
		}

		protected abstract void AirMovement();

		protected abstract void Movement();

		protected abstract void Rotation();

		protected abstract void UpdateAnimator(Vector3 velocity);

		protected virtual void CheckGrounded()
		{
			isGrounded = Physics.CheckCapsule(collider.bounds.center, new Vector3(collider.bounds.center.x, collider.bounds.min.y - groundDistance, collider.bounds.center.z), 0.35f, groundLayerMask);
		}

	}


}