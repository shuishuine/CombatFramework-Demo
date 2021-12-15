using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
[TaskCategory("Battle")]
public class Set_NavMeshAgentSpeed : Action
{
	public SharedFloat speed;
	public SharedGameObject target;

	public override TaskStatus OnUpdate()
	{
        if (target.Value == null)
        {
			target.Value = gameObject;
		}
		target.Value.GetComponent<NavMeshAgent>().speed = speed.Value;
		return TaskStatus.Success;
	}
}