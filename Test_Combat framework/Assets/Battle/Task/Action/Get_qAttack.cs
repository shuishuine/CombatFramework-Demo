using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Battle;

[TaskCategory("Battle")]
public class Get_qAttack : Action
{
	public SharedTransform target;
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		
		switch (Owner)
        {
			case SkillTree: target.Value = GetComponent<SoliderFace>().q_Attack; break;
			case BulletTree: target.Value = (Owner as BulletTree).soliderFace.q_Attack; break;
        }
		
		return TaskStatus.Success;
	}
}