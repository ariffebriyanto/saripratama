#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Buku_Besar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4fd911925ce6fe011fa6c7174421dbd856bdf63c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Master_Buku_Besar), @"mvc.1.0.view", @"/Views/Master/Buku_Besar.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Master/Buku_Besar.cshtml", typeof(AspNetCore.Views_Master_Buku_Besar))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4fd911925ce6fe011fa6c7174421dbd856bdf63c", @"/Views/Master/Buku_Besar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Master_Buku_Besar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/Master/MBukuBesar.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Buku_Besar.cshtml"
  
    ViewData["Title"] = "Buku Besar GL";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(98, 32, true);
            WriteLiteral("<script>\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(131, 39, false);
#line 7 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Buku_Besar.cshtml"
                 Write(Url.Action("GetSIFBukuBesar", "Master"));

#line default
#line hidden
            EndContext();
            BeginContext(170, 23, true);
            WriteLiteral("\';\r\n    var urlForm = \'");
            EndContext();
            BeginContext(194, 37, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Buku_Besar.cshtml"
              Write(Url.Action("EditBukuBesar", "Master"));

#line default
#line hidden
            EndContext();
            BeginContext(231, 23, true);
            WriteLiteral("\';\r\n    var urlSave = \'");
            EndContext();
            BeginContext(255, 37, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Buku_Besar.cshtml"
              Write(Url.Action("SaveBukuBesar", "Master"));

#line default
#line hidden
            EndContext();
            BeginContext(292, 25, true);
            WriteLiteral("\';\r\n    var urlDelete = \'");
            EndContext();
            BeginContext(318, 39, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Buku_Besar.cshtml"
                Write(Url.Action("DeleteBukuBesar", "Master"));

#line default
#line hidden
            EndContext();
            BeginContext(357, 25, true);
            WriteLiteral("\';\r\n    var urlUpdate = \'");
            EndContext();
            BeginContext(383, 39, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Buku_Besar.cshtml"
                Write(Url.Action("UpdateBukuBesar", "Master"));

#line default
#line hidden
            EndContext();
            BeginContext(422, 17, true);
            WriteLiteral("\';\r\n\r\n</script>\r\n");
            EndContext();
            BeginContext(439, 102, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fd911925ce6fe011fa6c7174421dbd856bdf63c6253", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 14 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Master\Buku_Besar.cshtml"
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
            BeginContext(541, 118, true);
            WriteLiteral("\r\n\r\n<div id=\"wrapperList\">\r\n    <div class=\"panel-title\" >\r\n        <span class=\"fontTitle\">Master Buku Besar</span>\r\n");
            EndContext();
            BeginContext(822, 159, true);
            WriteLiteral("    </div>\r\n    <div>\r\n\r\n    </div>\r\n    <div id=\"GridBrg\" style=\"display:inline-block;\"></div>\r\n</div>\r\n<div id=\"editForm\"></div>\r\n<div id=\"dialog\"></div>\r\n\r\n");
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
