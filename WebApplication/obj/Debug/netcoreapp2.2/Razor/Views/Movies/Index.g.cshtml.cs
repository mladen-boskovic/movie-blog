#pragma checksum "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e0475bdac5556d9e78d1d4ddaace86d2414cdf26"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movies_Index), @"mvc.1.0.view", @"/Views/Movies/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Movies/Index.cshtml", typeof(AspNetCore.Views_Movies_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\_ViewImports.cshtml"
using WebApplication;

#line default
#line hidden
#line 2 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\_ViewImports.cshtml"
using WebApplication.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e0475bdac5556d9e78d1d4ddaace86d2414cdf26", @"/Views/Movies/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa0ef8da47a84ffb33e8bc853509aa4fa5703a26", @"/Views/_ViewImports.cshtml")]
    public class Views_Movies_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Application.DTO.MovieDto>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(46, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(89, 29, true);
            WriteLiteral("\r\n<h1>Index</h1>\r\n\r\n<p>\r\n    ");
            EndContext();
            BeginContext(118, 37, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e0475bdac5556d9e78d1d4ddaace86d2414cdf264790", async() => {
                BeginContext(141, 10, true);
                WriteLiteral("Create New");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(155, 92, true);
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(248, 38, false);
#line 16 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
            EndContext();
            BeginContext(286, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(342, 40, false);
#line 19 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(382, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(438, 47, false);
#line 22 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(485, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(541, 40, false);
#line 25 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Year));

#line default
#line hidden
            EndContext();
            BeginContext(581, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(637, 49, false);
#line 28 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.LengthMinutes));

#line default
#line hidden
            EndContext();
            BeginContext(686, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(742, 46, false);
#line 31 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.TrailerUrl));

#line default
#line hidden
            EndContext();
            BeginContext(788, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(844, 40, false);
#line 34 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.User));

#line default
#line hidden
            EndContext();
            BeginContext(884, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(940, 41, false);
#line 37 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Image));

#line default
#line hidden
            EndContext();
            BeginContext(981, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1037, 45, false);
#line 40 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.CreatedAt));

#line default
#line hidden
            EndContext();
            BeginContext(1082, 86, true);
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 46 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(1217, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1278, 37, false);
#line 50 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
            EndContext();
            BeginContext(1315, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1383, 39, false);
#line 53 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1422, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1490, 46, false);
#line 56 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
            EndContext();
            BeginContext(1536, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1604, 39, false);
#line 59 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Year));

#line default
#line hidden
            EndContext();
            BeginContext(1643, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1711, 48, false);
#line 62 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.LengthMinutes));

#line default
#line hidden
            EndContext();
            BeginContext(1759, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1827, 45, false);
#line 65 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.TrailerUrl));

#line default
#line hidden
            EndContext();
            BeginContext(1872, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1940, 39, false);
#line 68 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.User));

#line default
#line hidden
            EndContext();
            BeginContext(1979, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2047, 40, false);
#line 71 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Image));

#line default
#line hidden
            EndContext();
            BeginContext(2087, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2155, 44, false);
#line 74 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.CreatedAt));

#line default
#line hidden
            EndContext();
            BeginContext(2199, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2267, 53, false);
#line 77 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.ActionLink("Edit", "Edit", new { id = item.Id }));

#line default
#line hidden
            EndContext();
            BeginContext(2320, 24, true);
            WriteLiteral(" |\r\n                    ");
            EndContext();
            BeginContext(2345, 59, false);
#line 78 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
               Write(Html.ActionLink("Details", "Details", new { id = item.Id }));

#line default
#line hidden
            EndContext();
            BeginContext(2404, 24, true);
            WriteLiteral(" |\r\n                    ");
            EndContext();
            BeginContext(2428, 321, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e0475bdac5556d9e78d1d4ddaace86d2414cdf2615628", async() => {
                BeginContext(2468, 100, true);
                WriteLiteral("\r\n                        <div class=\"form-group\">\r\n                            <input type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2568, "\"", 2584, 1);
#line 81 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
WriteAttributeValue("", 2576, item.Id, 2576, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2585, 157, true);
                WriteLiteral(" name=\"id\"/>\r\n                            <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" />\r\n                        </div>\r\n                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2749, 44, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 87 "C:\Users\mlade\Documents\Visual Studio 2019\Projects\MovieBlog\WebApplication\Views\Movies\Index.cshtml"
        }

#line default
#line hidden
            BeginContext(2804, 24, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Application.DTO.MovieDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591