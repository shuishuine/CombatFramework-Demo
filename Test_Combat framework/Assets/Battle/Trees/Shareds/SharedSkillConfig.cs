using UnityEngine;
using BehaviorDesigner.Runtime;

namespace Battle
{
	public class SharedSkillConfig : SharedVariable<SkillConfig>
	{
		public static implicit operator SharedSkillConfig(SkillConfig value)
		{
			return new SharedSkillConfig { mValue = value };
		}
		//public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
		//public static implicit operator SharedSkillConfig(SkillConfig value) { return new SharedSkillConfig { mValue = value }; }
	}
}
