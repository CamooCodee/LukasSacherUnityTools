#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CustomAttributes;
using UnityEngine;

namespace lukassacher.UnityTools
{
    [DefaultExecutionOrder(-1005)]
    public class ToolsManager : MonoBehaviour
    {
        [SerializeField] private Color requiredLogColor = new Color(0.839215686f, 0.415686275f, 0.388235294f, 1);
        [SerializeField] private bool useLogTypeStandard = false;
        [SerializeField] private LogType requiredLogType = LogType.Warning;
        
        
        private void Awake()
        {
            AssignAcceptOnlyFields();
            RequiredAttributeInit();
        }

        void RequiredAttributeInit()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var types = new List<Type>();
            
            foreach (var assembly in assemblies)
            {
                types.AddRange(
                    assembly.GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(MonoBehaviour)) &&
                                    (t.Namespace == null || !t.Namespace.StartsWith("Unity"))));
            }
            
            foreach (var type in types)
            {
                var objects = FindObjectsOfType(type);
                if(objects.Length == 0) continue;
                
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(prop => prop.IsDefined(typeof(RequiredAttribute), false));
                
                foreach (var field in fields)
                {
                    if(field.FieldType.IsValueType) return;
                    
                    foreach (var o in objects)
                    {
                        var val = field.GetValue(o);

                        if (!ReferenceEquals(val, null) && !val.Equals(null)) continue;

                        string errorMessage =
                            $"The field '{field.Name}' of type '{field.FieldType.Name}' is required on '{o.name}'!";

                        if (!useLogTypeStandard)
                            errorMessage = errorMessage.Color(requiredLogColor);
                            
                        this.log(errorMessage, o, requiredLogType);
                    }
                }
            }
        }

        void AssignAcceptOnlyFields()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var types = new List<Type>();
            
            foreach (var assembly in assemblies)
            {
                types.AddRange(
                    assembly.GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(MonoBehaviour)) &&
                                    (t.Namespace == null || !t.Namespace.StartsWith("Unity")) &&
                                    t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(f => f.GetCustomAttributes(typeof(AcceptOnlyAttribute), false).Length > 0)));
            }

            foreach (var type in types)
            {
                var objects = FindObjectsOfType(type);
                if(objects.Length == 0) continue;
                
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(prop => prop.IsDefined(typeof(AcceptOnlyAttribute), false));
                
                foreach (var field in fields)
                {
                    if(field.FieldType.IsValueType) return;
                    var attribute = field.GetCustomAttribute<AcceptOnlyAttribute>();

                    foreach (var o in objects)
                    {
                        var val = field.GetValue(o);
                        var toAssign = 
                            type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).First(f => f.Name == attribute.GetTargetFieldName());
                        
                        if(ReferenceEquals(val, null) || val.Equals(null))
                            toAssign.SetValue(o, null);
                        else
                            toAssign.SetValue(o, val);
                    }
                }
            }
        }
    }
}
#endif