using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TAC.Utils.Mvc;

namespace TAC.Utils.Helpers
{
    public static class FormHandlerExtensions
    {
        
        public static HtmlString FormHandlerX(this HtmlHelper html)
        {
            return FormHandlerX(html, GetRenderingField("controller action"));
        }
        public static HtmlString FormHandlerX(this HtmlHelper html, string actionName)
        {
            return FormHandlerX(html, GetRenderingField("controller"),actionName);
        }
        public static HtmlString FormHandlerX(this HtmlHelper html, string controllerName, string actionName)
        {
            return new HtmlString(
            string.Concat(
                html.Hidden(ValidateFormHandler.ControllerTag,controllerName),
                html.Hidden(ValidateFormHandler.ActionTag, actionName)
                )
            );
        }
        private static string GetRenderingField(string field)
        {
            return RenderingContext.CurrentOrNull.Rendering.RenderingItem.InnerItem[field];
        }
    }
}
