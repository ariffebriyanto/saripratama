#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Fin_AR_Lunas\MonAR.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07f540c0f60d7b581cf5093bb7e1e89576ca2b70"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Fin_AR_Lunas_MonAR), @"mvc.1.0.view", @"/Views/Fin_AR_Lunas/MonAR.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Fin_AR_Lunas/MonAR.cshtml", typeof(AspNetCore.Views_Fin_AR_Lunas_MonAR))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07f540c0f60d7b581cf5093bb7e1e89576ca2b70", @"/Views/Fin_AR_Lunas/MonAR.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Fin_AR_Lunas_MonAR : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/FINAR/MonAR.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Fin_AR_Lunas\MonAR.cshtml"
  
    ViewData["Title"] = "Monitoring AR";

#line default
#line hidden
            BeginContext(51, 37, true);
            WriteLiteral("\r\n    <script>\r\n   var urlGetData = \'");
            EndContext();
            BeginContext(89, 44, false);
#line 7 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Fin_AR_Lunas\MonAR.cshtml"
                Write(Url.Action("GetMonMutasiAR", "Fin_AR_Lunas"));

#line default
#line hidden
            EndContext();
            BeginContext(133, 32, true);
            WriteLiteral("\';\r\n    var urlGetDetailData = \'");
            EndContext();
            BeginContext(166, 45, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Fin_AR_Lunas\MonAR.cshtml"
                       Write(Url.Action("GetMonMutasiARD", "Fin_AR_Lunas"));

#line default
#line hidden
            EndContext();
            BeginContext(211, 25, true);
            WriteLiteral("\';\r\n    var urlCreate = \'");
            EndContext();
            BeginContext(237, 36, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Fin_AR_Lunas\MonAR.cshtml"
                Write(Url.Action("Create", "Fin_AR_Lunas"));

#line default
#line hidden
            EndContext();
            BeginContext(273, 24, true);
            WriteLiteral("\';\r\n    var urlPrint = \'");
            EndContext();
            BeginContext(298, 40, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Fin_AR_Lunas\MonAR.cshtml"
               Write(Url.Action("GetPrintAR", "Fin_AR_Lunas"));

#line default
#line hidden
            EndContext();
            BeginContext(338, 29, true);
            WriteLiteral("\';\r\n    var urlPembatalan = \'");
            EndContext();
            BeginContext(368, 37, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Fin_AR_Lunas\MonAR.cshtml"
                    Write(Url.Action("Pembatalan", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(405, 26, true);
            WriteLiteral("\';\r\n    var urlPegawai = \'");
            EndContext();
            BeginContext(432, 36, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Fin_AR_Lunas\MonAR.cshtml"
                 Write(Url.Action("PegawaiKasBon", "Kasir"));

#line default
#line hidden
            EndContext();
            BeginContext(468, 21, true);
            WriteLiteral("\';\r\n\r\n    </script>\r\n");
            EndContext();
            BeginContext(489, 96, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07f540c0f60d7b581cf5093bb7e1e89576ca2b706643", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 15 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Fin_AR_Lunas\MonAR.cshtml"
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
            BeginContext(585, 3467, true);
            WriteLiteral(@"

<div id=""wrapperList"">
    <div class=""panel-title"">
        <div class=""col-md-12"" style=""margin-bottom: 15px;"">
            <span class=""fontTitle"">Monitoring AR</span>
        </div>
    </div>
    <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
        <label class=""col-md-1 col-form-label"" style=""margin-left: 10px;"">NO Trans<span></span></label>
        <div class=""col-md-3"">


            <input class=""form-control form-control"" value="""" id=""no_trans"" />
            <input type=""text"" id=""p_np"" name=""p_np"" hidden=""hidden"" />

        </div>
        <div class=""col-md-2"">
            <button id=""addPO"" class=""btn btn-info pull-right"" onclick=""getData();""><i class=""glyphicon glyphicon-search""></i>&nbsp; Search</button>
        </div>

    </div>
    <div class=""col-md-12"" style=""margin-bottom: 15px; display:none;"">
        <div class=""col-md-12"">
            <div class=""col-md-9"">
                <div class="""">
                    <label class=""col-md-1 col-form-label""");
            WriteLiteral(@" style=""margin-left: 10px;"">Tanggal:</label>
                    <div class=""col-md-3"">
                        <div class='input-group date' id=""divtanggalfrom"">
                            <input id='tanggalfrom' type='text' class=""form-control datepicker"" value="""" />
                            <span class=""input-group-addon"">
                                <span class=""glyphicon glyphicon-calendar""></span>
                            </span>
                        </div>
                    </div>
                    <label class=""col-md-1 col-form-label"" style=""text-align:center"">To:</label>
                    <div class=""col-md-3"">
                        <div class='input-group date' id=""divtanggalto"">
                            <input id='tanggalto' type='text' class=""form-control datepicker"" value="""" />
                            <span class=""input-group-addon"">
                                <span class=""glyphicon glyphicon-calendar""></span>
                            </span>
");
            WriteLiteral(@"                        </div>
                    </div>
                    <div class=""col-md-2"">
                        <button id=""addPO"" class=""btn btn-info pull-right"" onclick=""getData();""><i class=""glyphicon glyphicon-search""></i>&nbsp; Search</button>
                    </div>
                </div>
            </div>
            <div class=""col-md-3"">
                <button id=""addPO"" class=""btn btn-primary pull-right"" onclick=""addnewPO();""><i class=""glyphicon glyphicon-plus""></i>&nbsp; Tambah AR</button>
                <button id=""btnPrint"" class=""btn btn-info pull-right"" onclick=""printpage();"" style=""margin-right: 15px;display:none;""><i class=""glyphicon glyphicon-print""></i>&nbsp; Cetak</button>

            </div>
        </div>

    </div>

    <div class=""col-md-12"" style=""margin-bottom: 15px;"">


    </div>


    <div class="""" style=""display:inline-block;"">
        <div id=""GridTerima""></div>
        <script type=""text/x-kendo-template"" id=""template"">
            <");
            WriteLiteral(@"div class=""tabstrip"">
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
