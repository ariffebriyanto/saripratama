#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6b99d1edd7b1c40f5851a923454c25bb84ad8c47"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MutasiMasuk_MonMtsMasuk), @"mvc.1.0.view", @"/Views/MutasiMasuk/MonMtsMasuk.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/MutasiMasuk/MonMtsMasuk.cshtml", typeof(AspNetCore.Views_MutasiMasuk_MonMtsMasuk))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b99d1edd7b1c40f5851a923454c25bb84ad8c47", @"/Views/MutasiMasuk/MonMtsMasuk.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_MutasiMasuk_MonMtsMasuk : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/Mutasi/MonMtsMasuk.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
  
    ViewData["Title"] = "Monitoring Penerimaan Mutasi";

#line default
#line hidden
            BeginContext(64, 38, true);
            WriteLiteral("\r\n    <script>\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(103, 35, false);
#line 6 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
                 Write(Url.Action("GetMts", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(138, 28, true);
            WriteLiteral("\';\r\n    var urlGetHeader = \'");
            EndContext();
            BeginContext(167, 45, false);
#line 7 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
                   Write(Url.Action("GetMts_INPartial", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(212, 32, true);
            WriteLiteral("\';\r\n    var urlGetDetailData = \'");
            EndContext();
            BeginContext(245, 41, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
                       Write(Url.Action("GetDetailMts", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(286, 23, true);
            WriteLiteral("\';\r\n    var urlForm = \'");
            EndContext();
            BeginContext(310, 36, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
              Write(Url.Action("EditMts", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(346, 23, true);
            WriteLiteral("\';\r\n    var urlSave = \'");
            EndContext();
            BeginContext(370, 36, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
              Write(Url.Action("SaveMts", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(406, 25, true);
            WriteLiteral("\';\r\n    var urlCreate = \'");
            EndContext();
            BeginContext(432, 34, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
                Write(Url.Action("Index", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(466, 24, true);
            WriteLiteral("\';\r\n    var urlPrint = \'");
            EndContext();
            BeginContext(491, 39, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
               Write(Url.Action("GetPrintPO", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(530, 27, true);
            WriteLiteral("\';\r\n\r\n     var urlGetQC = \'");
            EndContext();
            BeginContext(558, 39, false);
#line 14 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
                Write(Url.Action("GetDataMts", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(597, 28, true);
            WriteLiteral("\';\r\n    var urlGetGudang = \'");
            EndContext();
            BeginContext(626, 38, false);
#line 15 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
                   Write(Url.Action("GetGudang", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(664, 28, true);
            WriteLiteral("\';\r\n    var urlGetBarang = \'");
            EndContext();
            BeginContext(693, 38, false);
#line 16 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
                   Write(Url.Action("GetBarang", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(731, 29, true);
            WriteLiteral("\';\r\n    var urlPembatalan = \'");
            EndContext();
            BeginContext(761, 39, false);
#line 17 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
                    Write(Url.Action("Pembatalan", "MutasiMasuk"));

#line default
#line hidden
            EndContext();
            BeginContext(800, 21, true);
            WriteLiteral("\';\r\n\r\n    </script>\r\n");
            EndContext();
            BeginContext(821, 103, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6b99d1edd7b1c40f5851a923454c25bb84ad8c478581", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 20 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MutasiMasuk\MonMtsMasuk.cshtml"
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
            BeginContext(924, 2304, true);
            WriteLiteral(@"


<div id=""wrapperList"">
    <div class=""panel-title"">
        <div class=""col-md-12"" style=""margin-bottom: 15px;"">
            <span class=""fontTitle"">Monitoring Penerimaan Mutasi Antar Cabang</span>
        </div>
    </div>


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
                    </div>
                    <label class=""col-md-1 col");
            WriteLiteral(@"-form-label"" style=""text-align:center"">To:</label>
                    <div class=""col-md-2"">
                        <div class='input-group date' id=""divtanggalto"">
                            <input id='tanggalto' type='text' class=""form-control datepicker"" value="""" />
                            <span class=""input-group-addon"">
                                <span class=""glyphicon glyphicon-calendar""></span>
                            </span>
                        </div>
                    </div>
                    <div class=""col-md-4"">
                        <div class="""">
                            <label class=""col-md-3 col-form-label"">Barang:</label>
                            <div class=""col-md-9"">
                                <input id=""barang"" class=""form-control"" placeholder=""Filter Barang "" />
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-1"">
                        <button id=""add");
            WriteLiteral(@"PO"" class=""btn btn-info pull-right"" onclick=""searchData();""><i class=""glyphicon glyphicon-search""></i>&nbsp; Search</button>
                    </div>
                </div>
            </div>
        </div>
        
        <div class=""col-md-2"">
");
            EndContext();
            BeginContext(3387, 792, true);
            WriteLiteral(@"            <button id=""btnPrint"" class=""btn btn-info pull-right"" onclick=""printpage();"" style=""margin-right: 15px;display:none;""><i class=""glyphicon glyphicon-print""></i>&nbsp; Cetak</button>

        </div>
    </div>

    <div class=""col-md-12"" style=""display:inline-block;"">
        <div id=""GridPO"" ></div>
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
