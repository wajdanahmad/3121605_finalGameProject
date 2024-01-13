using System;
using GameDevUtils.HealthSystem;
using GameDevUtils.InteractSyetem;
using UnityEngine;


namespace GameDevUtils.CharacterController2
{


	public class CharacterController : GameDevBehaviour, IEffectContainer, IDamageable
	{

		[SerializeField] public CharacterBase             character;
		[SerializeField] HealthSystem.HealthSystem healthSystem;
		IEffectContainer                           effectContainer;
		public bool                                IsDestroyed { get; set; }


		void Awake()
		{
			GameController.onGameplay += StartPlay;
			effectContainer = new EffectContainer();
			character.Init(this);
			character.isGrounded = true;
		}

		

		public void AddEffect(string id, IEffect effect)
		{
			effectContainer.AddEffect(id, effect);
		}

		public void EmitEffect(IEffector effect)
		{
			effectContainer.EmitEffect(effect);
		}

		public void EmitEffect(string effectId)
		{
			effectContainer.EmitEffect(effectId);
		}


		public void Damage(float damageAmount, Vector3 hitPoint)
		{
			healthSystem.TakeDamage(damageAmount, hitPoint);
		}

		public void DestroyObject()
		{
			healthSystem.Death();
		}
		void StartPlay()
        {
			character.CanControl = true;
			character.characterInput.joystickStart = true;
        }
	}


	
}