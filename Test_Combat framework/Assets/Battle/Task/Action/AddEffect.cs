using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Battle;

[TaskCategory("Battle")]
public class AddEffect : Action
{
	public bool IsRemove = false;
	public string effectName;
	public SharedTransform target;


	public override TaskStatus OnUpdate()
	{
		if (target == null || target.Value == null)
        {
			if (IsRemove)
			{
				GetComponent<SoliderFace>().RemoveEffect(effectName);
			}
			else
			{
				GetComponent<SoliderFace>().AddEffect(effectName);
			}
		}
		else
        {
			if (IsRemove)
			{
				target.Value.GetComponent<SoliderFace>().RemoveEffect(effectName);
			}
			else
			{
				target.Value.GetComponent<SoliderFace>().AddEffect(effectName);
			}
		}
		
		return TaskStatus.Success;
	}
}