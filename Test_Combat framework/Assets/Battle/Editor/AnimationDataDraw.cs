using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEditor;

[CustomPropertyDrawer(typeof(List<AnimationData>))]
public class AnimationDataDraw :  PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		
	}
}
