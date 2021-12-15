using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.VersionControl;

public class CreateAC
{
    private const string ACName = "HeroAC";

    [MenuItem("MyMenu/Create Controller")]
    static void CreateController()
    {
        var controller = AnimatorController.CreateAnimatorControllerAtPath("Assets/Battle/Mecanim/HeroAC.controller");

        // Add StateMachines  总有一个默认的根状态机
        var rootStateMachine = controller.layers[0].stateMachine;

        // Add States
        for (int i = 1; i <= 5; i++)
        {
            string s = "State_" + i;
            var stateA1 = rootStateMachine.AddState(s);
            var ani = CreateAnimationClip(s);
            stateA1.motion = ani;
        }

        // Add State
        var _State = rootStateMachine.AddState("State");
        var _ani1 = CreateAnimationClip("State");
        _State.motion = _ani1;

        // Default - BlendTree
        var state = controller.CreateBlendTreeInController("Default",out var blendTree);
        blendTree.blendType = BlendTreeType.Simple1D;
        blendTree.blendParameter = "Blend";
        blendTree.useAutomaticThresholds = false;
        blendTree.AddChild(CreateAnimationClip("Idle"),0);
        blendTree.AddChild(CreateAnimationClip("Run"),3);
        blendTree.AddChild(CreateAnimationClip("Run2"),6);
        rootStateMachine.defaultState = state;

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static AnimationClip CreateAnimationClip(string name)
    {
        var clip = new AnimationClip() { name = name };
        AssetDatabase.CreateAsset(clip, $"Assets/Battle/Mecanim/{name}.anim");
        return clip;
    }
}