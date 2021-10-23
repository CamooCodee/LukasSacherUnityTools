using System;
using UnityEngine;

namespace CustomAttributes
{
    public class AcceptOnlyAttribute : PropertyAttribute
    {
        private readonly string _targetField;

        public AcceptOnlyAttribute(string targetFieldName)
        {
            if(targetFieldName == string.Empty)
                throw new ArgumentException("The target field name cannot be empty.", nameof(targetFieldName));

            _targetField = targetFieldName;
        }

        public string GetTargetFieldName() => _targetField;
    }
}