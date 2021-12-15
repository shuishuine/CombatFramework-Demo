using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class Incrementally_Int : Action
{
	public SharedInt sharedNumber;
	public SharedInt Number;
	public SharedInt addEnd;

	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		Number.Value += addEnd.Value;
		sharedNumber.Value = Number.Value;
		return TaskStatus.Success;
	}
}