using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Battle;

[TaskCategory("Battle")]
public class Set_qAttack : Action
{
	public SharedTransform target;

	public override TaskStatus OnUpdate()
	{
        switch (Owner)
        {
            case SkillTree: GetComponent<SoliderFace>().q_Attack = target.Value; break;
            case BulletTree: (Owner as BulletTree).soliderFace.q_Attack = target.Value; break;
        }
        
		return TaskStatus.Success;
	}
}