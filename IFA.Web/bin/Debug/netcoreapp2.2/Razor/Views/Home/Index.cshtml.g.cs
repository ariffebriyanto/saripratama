#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6ecd1a3132d702e294b9f88068ad853380b8e85f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\_ViewImports.cshtml"
using ERP.Web;

#line default
#line hidden
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\_ViewImports.cshtml"
using ERP.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ecd1a3132d702e294b9f88068ad853380b8e85f", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/Home/Home.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home";

#line default
#line hidden
            BeginContext(40, 57, true);
            WriteLiteral("<script type=\"text/javascript\">\r\n    var urlGetStatPO = \'");
            EndContext();
            BeginContext(98, 31, false);
#line 5 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Home\Index.cshtml"
                   Write(Url.Action("GetStatPO", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(129, 29, true);
            WriteLiteral("\';\r\n    var urlGetStatMTS = \'");
            EndContext();
            BeginContext(159, 32, false);
#line 6 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Home\Index.cshtml"
                    Write(Url.Action("GetStatMTS", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(191, 29, true);
            WriteLiteral("\';\r\n    var urlGetStatPIU = \'");
            EndContext();
            BeginContext(221, 32, false);
#line 7 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Home\Index.cshtml"
                    Write(Url.Action("GetStatPIU", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(253, 33, true);
            WriteLiteral("\';\r\n        var urlGetStatDUE = \'");
            EndContext();
            BeginContext(287, 32, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Home\Index.cshtml"
                        Write(Url.Action("GetStatDUE", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(319, 29, true);
            WriteLiteral("\';\r\n     var urlGetBooked = \'");
            EndContext();
            BeginContext(349, 35, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Home\Index.cshtml"
                    Write(Url.Action("GetStatBooked", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(384, 34, true);
            WriteLiteral("\';    //GetStatBooked\r\n</script>\r\n");
            EndContext();
            BeginContext(418, 71, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6ecd1a3132d702e294b9f88068ad853380b8e85f5702", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Home\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(489, 1745, true);
            WriteLiteral(@"
<div class=""text-center"">
    <h1 class=""display-4"">Dashboard</h1>
</div>

<div class=""col-md-12 "">
    <div class=""col-md-6 "">

        <div class=""panel panel-default"">

            <div class=""panel panel-success"">
                <div class=""panel-heading"">Piutang Limit</div>

                <div class=""panel-body"" style=""display:inline-block;"">
                    <div id=""GridPiu""></div>
                </div>

            </div>
            <div class=""panel panel-success"">

                <div class=""panel-heading"">Top Stat Booked</div>

                <div class=""panel-body"" style=""display:inline-block;"">
                    <div id=""GridBooked""></div>
                </div>
            </div>
            <div class=""panel panel-success"">

                <div class=""panel-heading"">Piutang Due Date</div>

                <div class=""panel-body"" style=""display:inline-block;"">
                    <div id=""GridDue""></div>
                </div>
            </div>

");
            WriteLiteral(@"        </div>

    </div>
    <div class=""col-md-6 "">
        <div class=""panel panel-default"">
            <div class=""panel panel-success"">
                <div class=""panel-heading"">Mutasi Outstanding > 7 Hari </div>

                <div class=""panel-body"" style=""display:inline-block;"">
                    <div id=""GridMTS""></div>
                </div>
            </div>

            <div class=""panel panel-success"">
                <div class=""panel-heading"">PO Status OPEN</div>

                <div class=""panel-body"" style=""display:inline-block;"">
                    <div id=""GridStatusPO""></div>
                </div>


            </div>


        </div>

    </div>
</div>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
