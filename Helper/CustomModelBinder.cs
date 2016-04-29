using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;


namespace Helper
{
    public class CustomModelBinder : System.Web.Mvc.DefaultModelBinder
    {
        protected override object GetPropertyValue(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, System.Web.Mvc.IModelBinder propertyBinder)
        {
            var propertyType = propertyDescriptor.PropertyType;
            if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                var providerValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (null != providerValue)
                {
                    DateTime date;
                    if (DateTime.TryParseExact(providerValue.AttemptedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        return date;
                    }
                }
            }
            return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }
    }
}
