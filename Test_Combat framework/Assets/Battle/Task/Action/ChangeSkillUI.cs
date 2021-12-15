using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Battle;

[TaskCategory("Battle")]
public class ChangeSkillUI : Action
{
	private int xb;
	public int nextId;


    public override void OnAwake()
    {
		SkillConfig config = null;
        switch (Owner)
        {
			case BulletTree: config = (Owner as BulletTree).skillTree.skillConfig; break;
			case SkillTree: config = (Owner as SkillTree).skillConfig; break;
        }
        if (config == null) return;

		xb = config.index;
	}

    public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		SoliderFace soliderFace = null;
		switch (Owner)
		{
			case BulletTree: soliderFace = (Owner as BulletTree).soliderFace; break;
			case SkillTree: soliderFace = gameObject.GetComponent<SoliderFace>();  break;
		}

        if (nextId != 0)
        {
			soliderFace.ChangeSkillUI(xb, nextId);
		}
		
		return TaskStatus.Success;
	}
}