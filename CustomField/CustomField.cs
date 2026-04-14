using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegister
{
    public class CustomField
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

        public CustomField(string fieldName, string fieldValue)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
        }

        public override string ToString()
        {
            var parts = new List<string>();
            if (!string.IsNullOrEmpty(FieldName)) parts.Add($"{FieldName}");
            if (!string.IsNullOrEmpty(FieldValue)) parts.Add($"{FieldValue}");

            return string.Join(": ", parts.Where(p => !string.IsNullOrEmpty(p)));
            //return $"{FieldName}: {FieldValue}";
        }

    }

}
