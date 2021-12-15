using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class ReturnEvent : Action
{
	public SharedString eventName;
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		SkillTree skillTree = null;
        switch (Owner)
        {
			case BulletTree: skillTree = (Owner as BulletTree).skillTree; break;
			case BuffTree: skillTree = (Owner as BuffTree).skillTree; break;
        }
		skillTree.SendEvent(eventName.Value);

		return TaskStatus.Success;
	}
}