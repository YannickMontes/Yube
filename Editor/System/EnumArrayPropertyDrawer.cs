using UnityEditor;
using UnityEngine;

namespace Yube
{
    [CustomPropertyDrawer(typeof(EnumArray<,>), true)]
    public class EnumArrayPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float baseHeight = base.GetPropertyHeight(property, label);
            if (foldoutOpen)
            {
                SerializedProperty namesProp = property.FindPropertyRelative(namesPropName);
                return baseHeight + (namesProp.arraySize) * EditorGUIUtility.singleLineHeight + (space * namesProp.arraySize - 1);
            }
            else
            {
                return baseHeight;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect newPosition = position;
            newPosition.height = EditorGUIUtility.singleLineHeight;
            foldoutOpen = EditorGUI.Foldout(newPosition, foldoutOpen, new GUIContent(property.displayName), true);
            if (foldoutOpen)
            {
                EditorGUI.indentLevel++;
                newPosition.y += EditorGUIUtility.singleLineHeight;

                SerializedProperty namesProp = property.FindPropertyRelative(namesPropName);
                SerializedProperty valuesProp = property.FindPropertyRelative(valuesPropName);

                for (int i = 0; i < namesProp.arraySize; i++)
                {
                    EditorGUI.PropertyField(newPosition, valuesProp.GetArrayElementAtIndex(i), new GUIContent(namesProp.GetArrayElementAtIndex(i).stringValue));
                    newPosition.y += EditorGUIUtility.singleLineHeight + (i + 1 < namesProp.arraySize ? space : 0);
                }
                EditorGUI.indentLevel--;
            }
        }

        private bool foldoutOpen = false;
        private float space = 3;
        private string namesPropName = "names";
        private string valuesPropName = "values";
    }
}