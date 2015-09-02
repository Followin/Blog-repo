using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WEB.Infrastructure
{
    public class MyMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
        {
            var result = base.GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);
            if (result.TemplateHint == null &&
                typeof(Enum).IsAssignableFrom(result.ModelType))
            {
                result.TemplateHint = "Enum";
            }
            return result;
        }
    }
}