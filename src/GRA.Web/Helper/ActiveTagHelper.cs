﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRA.Web.Helper
{
    [HtmlTargetElement(Attributes = "ActiveBy, routeKey, value")]
    public class ActiveTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public ActiveTagHelper(IUrlHelperFactory factory)
        {
            urlHelperFactory = factory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContextData { get; set; }

        [HtmlAttributeName("routeKey")]
        public string routeKey { get; set; }

        [HtmlAttributeName("value")]
        public string value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper url = urlHelperFactory.GetUrlHelper(ViewContextData);
            var routeData = url.ActionContext.RouteData.Values;
            string routeValue = routeData[routeKey] as string ?? url.ActionContext.HttpContext.Request.Query[routeKey].ToString();
            
            if (String.Equals(value, routeValue, StringComparison.OrdinalIgnoreCase))
            {
                output.Attributes.Add("class", "active");
            }
        }
    }
}