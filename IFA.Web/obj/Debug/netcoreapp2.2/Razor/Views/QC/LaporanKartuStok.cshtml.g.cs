#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\LaporanKartuStok.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8c936afb22fcf70188d01d1eee5437b2c76d1199"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_QC_LaporanKartuStok), @"mvc.1.0.view", @"/Views/QC/LaporanKartuStok.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/QC/LaporanKartuStok.cshtml", typeof(AspNetCore.Views_QC_LaporanKartuStok))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c936afb22fcf70188d01d1eee5437b2c76d1199", @"/Views/QC/LaporanKartuStok.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_QC_LaporanKartuStok : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/QC/LaporanKartuStok.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\LaporanKartuStok.cshtml"
  
    ViewData["Title"] = "Laporan Kartu Stok";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(103, 54, true);
            WriteLiteral("<script type=\"text/javascript\">\r\n    var urlBarang = \'");
            EndContext();
            BeginContext(158, 29, false);
#line 7 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\LaporanKartuStok.cshtml"
                Write(Url.Action("GetBarang", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(187, 24, true);
            WriteLiteral("\';\r\n    var urlTahun = \'");
            EndContext();
            BeginContext(212, 28, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\LaporanKartuStok.cshtml"
               Write(Url.Action("GetTahun", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(240, 24, true);
            WriteLiteral("\';\r\n    var urlBulan = \'");
            EndContext();
            BeginContext(265, 28, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\LaporanKartuStok.cshtml"
               Write(Url.Action("GetBulan", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(293, 36, true);
            WriteLiteral("\';\r\n    var urlGetDataStokHeader = \'");
            EndContext();
            BeginContext(330, 37, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\LaporanKartuStok.cshtml"
                           Write(Url.Action("GetDataStokHeader", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(367, 36, true);
            WriteLiteral("\';\r\n    var urlGetPrintKartuStok = \'");
            EndContext();
            BeginContext(404, 37, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\LaporanKartuStok.cshtml"
                           Write(Url.Action("GetPrintKartuStok", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(441, 17, true);
            WriteLiteral("\';\r\n\r\n</script>\r\n");
            EndContext();
            BeginContext(458, 82, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8c936afb22fcf70188d01d1eee5437b2c76d11995971", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 14 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\LaporanKartuStok.cshtml"
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
            BeginContext(540, 506, true);
            WriteLiteral(@"
<div id=""wrapperList"">
    <div class=""panel-title"">
        <div class=""col-md-12"" style=""margin-bottom: 15px;"">
            <span class=""fontTitle"">Laporan Kartu Stok</span>
        </div>
    </div>

    <div class=""col-md-12"" style=""margin-bottom: 40px;"">
        <div class=""col-md-12"">
            <div class=""col-md-3"">
                <div class="""">
                    <label class=""col-md-3 col-form-label"">Barang:</label>
                    <div class=""col-md-9 widthcboCustom"">
");
            EndContext();
            BeginContext(1184, 3791, true);
            WriteLiteral(@"                        <input class=""form-control"" value="""" id=""Barang"" />

                    </div>
                </div>
            </div>
            <div class=""col-md-9"">
                <div class="""">
                    <label class=""col-md-1 col-form-label"" style=""margin-left: 10px;"">Period:</label>
                    <div class=""col-md-3 widthcboCustom50"">
                        <select name=""Bulan"" id=""Bulan"" class=""selectpicker width99 "" data-live-search=""true""></select>
                    </div>
                    <div class=""col-md-2 widthcboCustom50"">
                        <select name=""Tahun"" id=""Tahun"" class=""selectpicker width99 "" data-live-search=""true""></select>
                    </div>
                    <div class=""col-md-2 checkbox checkbox-primary "">
                        <input id=""checkbox2"" class=""styled "" type=""checkbox"">
                        <label for=""checkbox2"" class="""">
                            Period Tahun
                        </label");
            WriteLiteral(@">
                    </div>
                    <div class=""col-md-3"">
                        <button id=""print"" class=""btn btn-success pull-right"" onclick=""printData();""><i class=""glyphicon glyphicon-print""></i>&nbsp; Print</button>
                        <button id=""search"" class=""btn btn-info pull-right"" onclick=""getData();""  style=""margin-right: 5px;""><i class=""glyphicon glyphicon-search""></i>&nbsp; Search</button>

                    </div>
                </div>
            </div>
        </div>

    </div>
    <div class=""col-md-12"" style=""border: black solid 3px; margin-bottom:15px; display:none;"" id=""divHeader"">
        <div class=""panel-body page-content  container-fluid request-viewrequest"" style=""margin-left:0px;"">
            <div class=""col-md-12 text-right"" style=""margin-left:-53px; margin-top:15px;"">
                <label id=""lblPeriod""></label>

            </div>
            <div class=""col-md-12 text-center"">
                <label style=""font-size: 20px;font-weight:");
            WriteLiteral(@" bold !important;"">KARTU STOK</label>

            </div>
            <div class=""col-md-12"">
                <div class=""col-md-6 text-left"">
                    <div class=""col-md-2 "" style=""margin-left: 15px;"">
                        <label style=""font-size: 14px;"">Nama Barang:</label>
                    </div>
                    <div class=""col-md-8 "">
                        <label style=""font-size: 14px;font-weight: bold !important;"" id=""lblNamaBarang""></label>
                    </div>
                </div>
            </div>

            <div class=""col-md-12"">
                <div class=""col-md-6 text-left"">
                    <div class=""col-md-2 "" style=""margin-left: 15px;"">
                        <label style=""font-size: 14px;"">Kode Barang:</label>
                    </div>
                    <div class=""col-md-8 "">
                        <label style=""font-size: 14px;font-weight: bold !important;"" id=""lblKodeBarang""></label>
                    </div>
             ");
            WriteLiteral(@"   </div>
                <div class=""col-md-6 "">
                    <div class=""col-md-10 text-right"" style=""margin-left: 15px;"">
                        <label style=""font-size: 14px;"">Saldo Awal:</label>
                    </div>
                    <div class=""col-md-1 text-right"">
                        <label style=""font-size: 14px;font-weight: bold !important;"" id=""lblSaldoAwal""></label>
                    </div>
                </div>
            </div>
            </div>
        </div>



        <div class=""col-md-12"" style=""display:inline-block;"">

            <div id=""kartustokGrid""></div>
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
