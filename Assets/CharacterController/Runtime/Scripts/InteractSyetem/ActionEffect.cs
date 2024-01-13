using System;


namespace GameDevUtils.InteractSyetem
{


	public class ActionEffect : IEffect
	{

		Action task;

		public ActionEffect(Action task)
		{
			this.task = task;
		}
		
		public void Emit()
		{
			task?.Invoke();
		}

	}


}