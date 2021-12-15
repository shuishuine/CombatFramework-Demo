using Battle;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Battle")]
public class ActionPlayAni : Action
{
	private SoliderFace soliderFace;
	public AnimationClip clip;

	public override void OnAwake()
	{
		soliderFace = gameObject.GetComponent<SoliderFace>();
	}

	public override void OnStart()
	{
		soliderFace.aniMgr.PlayAnimationByClipName(clip.name);
	}

	public override TaskStatus OnUpdate()
	{
		if (soliderFace.aniMgr.IsPlaying(clip.name))
		{
			return TaskStatus.Running;
		}
		else
		{
			return TaskStatus.Success;
		}
	}
}