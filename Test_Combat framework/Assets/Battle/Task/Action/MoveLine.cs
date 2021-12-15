using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class MoveLine : Action
{

	public float distance;
	public float speed;

	private float dised;

    public override void OnAwake()
    {
		dised = 0;
	}


	public override TaskStatus OnUpdate()
	{
		float deltaDis = speed * Time.deltaTime;
		dised += deltaDis;

        if (distance <= 0)
        {
			Debug.Log("ÇëÊäÈëÒ»¶¨¾àÀë");
			return TaskStatus.Failure;
		}

        if (dised < distance)
        {
			transform.Translate(transform.forward * deltaDis, Space.World);
			return TaskStatus.Running;
		}
        else
        {
			return TaskStatus.Success;
		}
	}
}