using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class CheckMany : Conditional
{
    public SharedInt many;


    public override TaskStatus OnUpdate()
	{
        if (many.Value <= 0)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
	}
}