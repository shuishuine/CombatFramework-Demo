using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using Battle;

[TaskCategory("Battle")]
public class ChangeNavMeshAgent : Action
{
	public bool CloseNavMeshAgent = false;
	public bool CloseMouseInput = false;
	public bool CloseSkillInput = false;

	private GameObject obj;
	private SoliderFace soliderFace;

    public override void OnAwake()
    {
		switch (Owner)
		{
			case SkillTree: obj = gameObject; break;
			case BulletTree: obj = (Owner as BulletTree).soliderFace.gameObject; break;
			case BuffTree: obj = (Owner as BulletTree).soliderFace.gameObject; break;
		}
		soliderFace = obj.GetComponent<SoliderFace>();
	}

    public override TaskStatus OnUpdate()
	{
        if (obj)
        {
			soliderFace.agent.enabled = !CloseNavMeshAgent;
			soliderFace.NavMeshMove = !CloseNavMeshAgent;
			soliderFace.MouseInput = !CloseMouseInput;
			soliderFace.SkillInput = !CloseSkillInput;
		}
		return TaskStatus.Success;
	}
}