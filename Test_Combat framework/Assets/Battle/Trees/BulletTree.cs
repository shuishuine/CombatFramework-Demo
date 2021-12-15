using Battle;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTree : Behavior
{
    public SkillTree skillTree;
    public SkillConfig skillConfig;
    public SoliderFace soliderFace;
    public GameObject targetObj;

    public void Init(SkillTree tree,GameObject obj = null)
    {
        skillTree = tree;
        skillConfig = tree.skillConfig;
        soliderFace = tree.gameObject.GetComponent<SoliderFace>();
        if (obj) targetObj = obj;
    }
}
