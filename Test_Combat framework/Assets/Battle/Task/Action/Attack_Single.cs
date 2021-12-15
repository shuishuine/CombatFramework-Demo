using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Battle;
[TaskCategory("Battle")]
public class Attack_Single : Action
{
	public SharedTransform target;
	public SharedFloat Damage;
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		target.Value.GetComponent<SoliderFace>().GetDamage(Damage.Value);
		return TaskStatus.Success;
	}
}