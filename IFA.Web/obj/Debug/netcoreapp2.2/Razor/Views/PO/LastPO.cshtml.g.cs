#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\LastPO.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "575ec0a464e11609400edbb80b21fb8085f28f8c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PO_LastPO), @"mvc.1.0.view", @"/Views/PO/LastPO.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PO/LastPO.cshtml", typeof(AspNetCore.Views_PO_LastPO))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"575ec0a464e11609400edbb80b21fb8085f28f8c", @"/Views/PO/LastPO.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_PO_LastPO : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/PO/LastPO.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\LastPO.cshtml"
  
    ViewData["Title"] = "Monitoring PO Terakhir";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(107, 34, true);
            WriteLiteral("\r\n<script>\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(142, 36, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\LastPO.cshtml"
                 Write(Url.Action("GetLastPOPartial", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(178, 30, true);
            WriteLiteral("\';\r\n    var urlExportExcel = \'");
            EndContext();
            BeginContext(209, 37, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\LastPO.cshtml"
                     Write(Url.Action("ExportExcelLastPO", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(246, 17, true);
            WriteLiteral("\';\r\n\r\n</script>\r\n");
            EndContext();
            BeginContext(263, 94, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "575ec0a464e11609400edbb80b21fb8085f28f8c5047", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\LastPO.cshtml"
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
            BeginContext(357, 3045, true);
            WriteLiteral(@"

<div id=""wrapperList"">
    <div class=""panel-title"">
        <div class=""col-md-12"" style=""margin-bottom: 15px;"">
            <span class=""fontTitle"">Monitoring PO Terakhir</span>
        </div>
    </div>
    <div class=""panel-body"">
        <div class=""col-md-12"" style=""margin-bottom: 15px;"">
            <div class=""col-md-8"">
                <div class=""col-md-12"">
                    <div class="""">
                        <label class=""col-md-1 col-form-label"" style=""margin-left: 10px;"">Tanggal:</label>
                        <div class=""col-md-2"">
                            <div class='input-group date' id=""divtanggalfrom"">
                                <input id='tanggalfrom' type='text' class=""form-control datepicker"" value="""" />
                                <span class=""input-group-addon"">
                                    <span class=""glyphicon glyphicon-calendar""></span>
                                </span>
                            </div>
                       ");
            WriteLiteral(@" </div>
                        <label class=""col-md-1 col-form-label"" style=""text-align:center"">To:</label>
                        <div class=""col-md-2"">
                            <div class='input-group date' id=""divtanggalto"">
                                <input id='tanggalto' type='text' class=""form-control datepicker"" value="""" />
                                <span class=""input-group-addon"">
                                    <span class=""glyphicon glyphicon-calendar""></span>
                                </span>
                            </div>
                        </div>
                        <div class=""col-md-2"">
                            <button id=""addPO"" class=""btn btn-info pull-right"" onclick=""searchPO();""><i class=""glyphicon glyphicon-search""></i>&nbsp; Filter</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-md-4"">
                <button id=""exportExcel"" class=""btn btn-pr");
            WriteLiteral(@"imary pull-right"" onclick=""onexportExcel(); return false;""><i class=""glyphicon glyphicon-download-alt""></i>&nbsp; Export To Excel</button>
            </div>
        </div>


        <div class=""col-md-12"" style=""display:inline-block;"">
            <div id=""GridPO"" class=""font10""></div>
            <script type=""text/x-kendo-template"" id=""template"">
                <div class=""tabstrip"">
                    <ul>
                        <li class=""k-state-active"">
                            Details
                        </li>
                    </ul>
                    <div>
                        <div class=""detail""></div>
                    </div>

                </div>

            </script>
        </div>

    </div>
</div>
<div id=""editForm""></div>
<div id=""dialog""></div>


<style>

    /*horizontal Grid scrollbar should appear if the browser window is shrinked too much*/
    #grid table {
        min-width: 1190px;
    }
</style>


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
