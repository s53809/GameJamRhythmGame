using UnityEngine;
using System;

// ���� ����Բ� ���Ŀ� ��
#if UNITY_EDITOR
namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(ReadOnly), true)]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
                => EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = !Application.isPlaying && ((ReadOnly)attribute).runtimeOnly;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
#endif

[AttributeUsage(AttributeTargets.Field)]
public class ReadOnly : PropertyAttribute
{
    public readonly bool runtimeOnly;

    public ReadOnly(bool runtimeOnly = false)
    {
        this.runtimeOnly = runtimeOnly;
    }
}