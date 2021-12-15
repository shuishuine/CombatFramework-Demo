using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;

public class BuffTree : Behavior {

    public string buffName;
    public SkillTree skillTree;
	public void Init(string buffName,SkillTree skillTree)
    {
        this.buffName = buffName;
        this.skillTree = skillTree;
    }
}
