using System;
using System.Collections.Generic;
using System.Linq;

namespace Damacon.SharedWeb.Helpers
{
    public class ReflectionExtensions
    {
        public static string GetPropertyValue(object item, string propertyName, IList<NameValue> values, string format = null)
        {
            string returnValue = string.Empty;
            object propValue = item.GetType().GetProperty(propertyName).GetValue(item, null);
            if (propValue != null)
            {
                if (values != null)
                {
                    var text = values.FirstOrDefault(x => x.Value.Equals(propValue.ToString(), System.StringComparison.InvariantCultureIgnoreCase));
                    if (text != null && text.Text != null)
                    {
                        returnValue = text.Text;
                    }
                }
                else
                {
                    if (format == null)
                    {
                        if (propValue.GetType() == typeof(DateTime))
                        {
                            propValue = ((DateTime)propValue).ToString(UIExtensions.DateFormat);
                        }
                    }
                    returnValue = string.IsNullOrEmpty(format) ? propValue.ToString() : System.Convert.ToDouble(propValue).ToString(format);
                }
            }

            return returnValue;
        }
    }
}