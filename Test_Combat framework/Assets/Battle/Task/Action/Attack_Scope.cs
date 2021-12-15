using Battle;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Battle")]
public class Attack_Scope : Action
{
	public SharedSoliderFaces targets;
	public SharedFloat Damage;
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
        foreach (var soliderFace in targets.Value)
        {
            soliderFace.GetDamage(Damage.Value);
        }

        return TaskStatus.Success;
	}
}