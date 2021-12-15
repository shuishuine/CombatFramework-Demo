using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Battle;

[TaskCategory("Battle")]
public class AddBuff : Action
{
	public bool IsRemove = false;
	public string buffName;
	public SharedTransform target;
	public SharedSoliderFaces targets;

	public override TaskStatus OnUpdate()
	{
		SkillTree skillTree = null;
        switch (Owner)
        {
            case SkillTree: skillTree = (SkillTree)Owner;	break;
			case BulletTree: skillTree = (Owner as BulletTree).skillTree; break;
			case BuffTree: skillTree = (Owner as BuffTree).skillTree; break;
		}


        if (target != null && target.Value)
		{
			if (IsRemove)
			{
				target.Value.GetComponent<SoliderFace>().RemoveBuff(buffName, skillTree);
			}
			else
			{
				target.Value.GetComponent<SoliderFace>().AddBuff(buffName, skillTree);
			}
			return TaskStatus.Success;
		}
		else if (targets != null && targets.Value.Count >= 1)
        {
            foreach (var item in targets.Value)
            {
				if (IsRemove)
				{
					item.RemoveBuff(buffName, skillTree);
				}
				else
				{
					item.AddBuff(buffName, skillTree);
				}
				return TaskStatus.Success;
			}
        }
        else
        {
			if (IsRemove)
			{
				GetComponent<SoliderFace>().RemoveBuff(buffName, skillTree);
			}
			else
			{
				GetComponent<SoliderFace>().AddBuff(buffName, skillTree);
			}
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}