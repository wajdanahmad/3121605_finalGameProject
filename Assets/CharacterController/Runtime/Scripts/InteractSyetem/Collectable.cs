using UnityEngine;


namespace GameDevUtils.InteractSyetem
{


	public class Collectable : TriggerEffector
	{

		protected override void OnEffect(Collider other, IEffectContainer container)
		{
			triggered = true;
			base.OnEffect(other, container);
			gameObject.SetActive(false);
		}

	}


}