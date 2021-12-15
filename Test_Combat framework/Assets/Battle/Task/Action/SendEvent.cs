using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class SendEvent : Action
{
	public SharedGameObject target;
	public SharedString eventName;

	public override TaskStatus OnUpdate()
	{
        if (target == null || target.Value == null)
        {
			target.Value = gameObject;
        }
		var behavior = target.Value.GetComponents<Behavior>();

        foreach (var item in behavior)
        {
            if (eventName != null)
            {
				item.SendEvent(eventName.Value);
			}
		}
		return TaskStatus.Success;
	}
}