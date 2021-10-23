using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CustomAttributes
{
    [CustomPropertyDrawer(typeof(AcceptOnlyAttribute))]
    public class AcceptOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);
            if(property.objectReferenceValue == null)
                return;
            
            if (!(property.objectReferenceValue is Component draggedIn))
            {
                Debug.LogError($"Properties using the Accept Only Attribute must be of type Component!");
                return;
            }

            var myAttribute = (AcceptOnlyAttribute) attribute;
            var fieldName = myAttribute.GetTargetFieldName();
            var targetObj = property.serializedObject;
            var targetObjType = targetObj.targetObject.GetType();
            var field = targetObjType.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (field == null)
            {
                Debug.LogError($"{nameof(AcceptOnlyAttribute)}: No field found with the given name. '{fieldName}'");
                property.objectReferenceValue = null;
                return;
            }

            var targetVal = draggedIn.gameObject.GetComponent(field.FieldType);
            
            property.objectReferenceValue = targetVal;
        }
    }
}