#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Sopir.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a187601b695592e312e408315d46a15f917754c7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Master_Sopir), @"mvc.1.0.view", @"/Views/Master/Sopir.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Master/Sopir.cshtml", typeof(AspNetCore.Views_Master_Sopir))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a187601b695592e312e408315d46a15f917754c7", @"/Views/Master/Sopir.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Master_Sopir : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/Master/Sopir.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Sopir.cshtml"
  
    ViewData["Title"] = "Master Sopir";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(97, 38, true);
            WriteLiteral("\r\n    <script>\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(136, 32, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Sopir.cshtml"
                 Write(Url.Action("GetSopir", "Master"));

#line default
#line hidden
            EndContext();
            BeginContext(168, 23, true);
            WriteLiteral("\';\r\n    var urlSave = \'");
            EndContext();
            BeginContext(192, 36, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Sopir.cshtml"
              Write(Url.Action("saveCustomer", "Master"));

#line default
#line hidden
            EndContext();
            BeginContext(228, 25, true);
            WriteLiteral("\';\r\n    var urlDelete = \'");
            EndContext();
            BeginContext(254, 38, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Sopir.cshtml"
                Write(Url.Action("DeleteCustomer", "Master"));

#line default
#line hidden
            EndContext();
            BeginContext(292, 25, true);
            WriteLiteral("\';\r\n    var urlUpdate = \'");
            EndContext();
            BeginContext(318, 38, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Sopir.cshtml"
                Write(Url.Action("UpdateCustomer", "Master"));

#line default
#line hidden
            EndContext();
            BeginContext(356, 25, true);
            WriteLiteral("\';\r\n    var GudangList = ");
            EndContext();
            BeginContext(383, 28, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Sopir.cshtml"
                 Write(Html.Raw(ViewBag.GudangList));

#line default
#line hidden
            EndContext();
            BeginContext(412, 26, true);
            WriteLiteral(";\r\n\r\n\r\n\r\n    </script>\r\n\r\n");
            EndContext();
            BeginContext(438, 97, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a187601b695592e312e408315d46a15f917754c76189", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 18 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Sopir.cshtml"
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
            BeginContext(535, 734, true);
            WriteLiteral(@"

<div id=""wrapperList"">
    <div class=""panel-title"">
        <span class=""fontTitle"">Master Sopir</span>

    </div>
    <div class=""col-md-12"" style=""margin-bottom: 15px;"">
        <div class=""col-md-8"">
            <div class=""col-md-4"">
                <div class="""">


                </div>
            </div>

        </div>
        <div class=""col-md-4"">
            <button id=""btnPrint"" class=""btn btn-info pull-right"" onclick=""printpage();"" style=""margin-right: 15px;display:none;""><i class=""glyphicon glyphicon-print""></i>&nbsp; Cetak</button>

        </div>
    </div>

    <div id=""GridBrg"" style=""display:inline-block;""></div>
</div>
<div id=""editForm""></div>
<div id=""dialog""></div>



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