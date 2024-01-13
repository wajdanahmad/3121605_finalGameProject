using System.Collections.Generic;
using UnityEngine;


namespace GameDevUtils.InteractSyetem
{


	public interface IEffectContainer
	{

		void AddEffect(string id, IEffect effect);

		void EmitEffect(IEffector effect);

		void EmitEffect(string effectId);

	}

	public interface IEffector
	{

		string  Id          { get; }
		Vector3 effectPoint { get; }

	}

	public interface IEffect
	{

		void Emit();

	}

	public class EffectContainer : IEffectContainer
	{

		Dictionary<string, IEffect> effectors;

		public EffectContainer()
		{
			effectors = new Dictionary<string, IEffect>();
		}

		public virtual void AddEffect(string id, IEffect effect)
		{
			effectors.Add(id, effect);
		}


		public virtual void EmitEffect(IEffector effect)
		{
			if (effectors.ContainsKey(effect.Id))
				effectors[effect.Id]?.Emit();
		}

		public virtual void EmitEffect(string effectId)
		{
			if (effectors.ContainsKey(effectId))
				effectors[effectId]?.Emit();
		}

	}


	public abstract class EffectorBase : MonoBehaviour, IEffector
	{

		[SerializeField] protected string  id;
		public                     string  Id          => id;
		public                     Vector3 effectPoint { get; protected set; }

		protected virtual void OnEffect(Collider other, IEffectContainer container)
		{
			effectPoint = other.transform.position;
			container.EmitEffect(this);
		}

	}

	public class TriggerEffector : EffectorBase
	{

		protected bool triggered = false;

		void OnTriggerEnter(Collider other)
		{
			if (triggered) return;
			var dealer = other.GetComponent<IEffectContainer>() ?? other.GetComponentInParent<IEffectContainer>();
			if (dealer != null)
				OnEffect(other, dealer);
		}

	}

	public class CollideEffector : EffectorBase
	{

		protected bool triggered = false;

		void OnCollisionEnter(Collision collision)
		{
			if (triggered) return;
			var dealer = collision.collider.GetComponent<IEffectContainer>() ?? collision.collider.GetComponentInParent<IEffectContainer>();
			if (dealer != null)
				OnEffect(collision.collider, dealer);
		}

	}


}