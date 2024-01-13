using UnityEngine;


namespace GameDevUtils.CharacterController2
{


	public abstract class CharacterInput : ScriptableObject
	{

		[SerializeField] protected float moveSpeed           = 2;
		[SerializeField] protected float rotationSpeed       = 0.1f;
		[SerializeField] protected float jumpForce           = 4;
		[SerializeField] protected float sensitivity         = 1;
		[SerializeField] protected float animationMultiplier = 1;
		public bool joystickStart;
		public virtual float MoveSpeed => moveSpeed;

		public virtual float RotationSpeed => rotationSpeed;

		public virtual float JumpForce => jumpForce;

		public float Sensitivity => sensitivity;

		public virtual float AnimationMultiplier => animationMultiplier;

		public bool  Walk       { get; protected set; }
		public bool  Jump       { get; protected set; }
		public float Vertical   { get; protected set; }
		public float Horizontal { get; protected set; }

		public abstract void Update();

	}


}