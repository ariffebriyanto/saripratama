#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "184b67bcf0031f4ef1fa83e3ca7317446e0a1a02"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Inventory_TerimaBebas), @"mvc.1.0.view", @"/Views/Inventory/TerimaBebas.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Inventory/TerimaBebas.cshtml", typeof(AspNetCore.Views_Inventory_TerimaBebas))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"184b67bcf0031f4ef1fa83e3ca7317446e0a1a02", @"/Views/Inventory/TerimaBebas.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Inventory_TerimaBebas : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IFA.Domain.Models.INV_GUDANG_IN>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/Inventory/TerimaBarang/TerimaBebas.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/expand-navigation-icon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("navigatortoggler-icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
  
    ViewData["Title"] = "Penerimaan Bebas";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(139, 41, true);
            WriteLiteral("\r\n<script>\r\n  \r\n  \r\n    var urlCreate = \'");
            EndContext();
            BeginContext(181, 38, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                Write(Url.Action("TerimaBebas", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(219, 23, true);
            WriteLiteral("\';\r\n    var urlSave = \'");
            EndContext();
            BeginContext(243, 42, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
              Write(Url.Action("SaveTerimaBebas", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(285, 25, true);
            WriteLiteral("\';\r\n    var urlSatuan = \'");
            EndContext();
            BeginContext(311, 37, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                Write(Url.Action("GetSatuanCbo", "MSatuan"));

#line default
#line hidden
            EndContext();
            BeginContext(348, 28, true);
            WriteLiteral("\';\r\n    var urlGetBarang = \'");
            EndContext();
            BeginContext(377, 39, false);
#line 13 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                   Write(Url.Action("GetBarangCbo", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(416, 28, true);
            WriteLiteral("\';\r\n    var UrlGetGudang = \'");
            EndContext();
            BeginContext(445, 36, false);
#line 14 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                   Write(Url.Action("GetGudang", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(481, 32, true);
            WriteLiteral("\';\r\n    var urlGetDataTerima = \'");
            EndContext();
            BeginContext(514, 36, false);
#line 15 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                       Write(Url.Action("GetTerima", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(550, 34, true);
            WriteLiteral("\';\r\n    var urlGetDetailTerima = \'");
            EndContext();
            BeginContext(585, 42, false);
#line 16 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                         Write(Url.Action("GetDetailTerima", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(627, 25, true);
            WriteLiteral("\';\r\n    var BranchUser =\'");
            EndContext();
            BeginContext(653, 18, false);
#line 17 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                Write(ViewBag.BranchUser);

#line default
#line hidden
            EndContext();
            BeginContext(671, 30, true);
            WriteLiteral("\';\r\n   \r\n   \r\n    var Mode = \'");
            EndContext();
            BeginContext(702, 12, false);
#line 20 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
           Write(ViewBag.Mode);

#line default
#line hidden
            EndContext();
            BeginContext(714, 18, true);
            WriteLiteral("\';\r\n    var id = \'");
            EndContext();
            BeginContext(733, 10, false);
#line 21 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
         Write(ViewBag.Id);

#line default
#line hidden
            EndContext();
            BeginContext(743, 25, true);
            WriteLiteral("\';\r\n    \r\n\r\n\r\n</script>\r\n");
            EndContext();
            BeginContext(768, 119, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "184b67bcf0031f4ef1fa83e3ca7317446e0a1a028909", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 26 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
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
            BeginContext(887, 2622, true);
            WriteLiteral(@"
<div class=""panel panel-default"">
    <div class=""panel-heading"">
        <div class=""col-md-12 paddingleft0 divTitle"">
            <div class=""col-md-4 paddingleft5 "">
                <h6 class=""panel-title"">
                    Penerimaan Bebas
                </h6>
            </div>
            <div class="" floatright"" style=""float:right;"">
            </div>
        </div>
    </div>

    <div class="""" style=""min-height:450px;"">
        <div class=""panel-body page-content  container-fluid request-viewrequest"" style=""margin-left:0px;"">
            <div class=""col-xs-12 col-sm-9 col-lg-11 page-left-content"" style=""min-height:450px;"">
                <div class=""row"" id=""form"">
                    <div class=""col-md-12"">
                        <div class=""col-md-6"">
                            <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                <div class=""row"">
                                    <label class=""col-md-2 col-form-label"">Gudang Tuju");
            WriteLiteral(@"an<span class=""red"">*</span></label>
                                    <div class=""col-md-10"">

                                        <select name=""IdGudang"" id=""IdGudang"" class=""selectpicker width99"" data-live-search=""true""></select>

                                    </div>
                                </div>
                            </div>


                            <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                <div class=""row"">
                                    <label class=""col-md-2 col-form-label"">Keterangan<span class=""red"">*</span></label>
                                    <div class=""col-md-10"">
                                        <textarea class=""form-control"" rows=""4"" id=""Keterangan"" name=""Keterangan"" style=""resize:none;max-width:50%""></textarea>
                                    </div>

                                </div>
                            </div>

                        </div>
                   ");
            WriteLiteral(@"     <div class=""col-md-6"">
                            <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                <div class=""row"">
                                    <label class=""col-md-2 col-form-label"">No Transaksi<span class=""red"">*</span></label>
                                    <div class=""col-md-10"">
                                        <input class=""form-control"" value="""" id=""no_trans"" disabled />
                                    </div>
                                </div>
                            </div>
");
            EndContext();
            BeginContext(4127, 1038, true);
            WriteLiteral(@"                            <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                <div class=""row"">
                                    <label class=""col-md-2 col-form-label"">Tanggal<span class=""red"">*</span></label>
                                    <div class=""col-xs-12 col-sm-9 col-lg-4"">
                                        <div class='input-group date' id=""divtanggal"">
                                            <input id='tanggal' type='text' class=""form-control datepicker"" value="""" />
                                            <span class=""input-group-addon"">
                                                <span class=""glyphicon glyphicon-calendar""></span>
                                            </span>
                                        </div>


                                    </div>

                                </div>
                            </div>


                        </div>
                    </div>

          ");
            WriteLiteral("      </div>\r\n");
            EndContext();
#line 111 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                 if (ViewBag.Mode == "NEW")
                {

#line default
#line hidden
            BeginContext(5229, 449, true);
            WriteLiteral(@"                    <div class=""col-md-12 form-c-inline"">
                        <div class=""row"">
                            <div class=""col-md-12"">
                                <button id=""add"" class=""btn btn-primary pull-right"" onclick=""onAddNewRow(); return false;""><i class=""glyphicon glyphicon-plus""></i>&nbsp; Tambah Item(F2)</button>

                            </div>
                        </div>
                    </div>
");
            EndContext();
#line 121 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                }

#line default
#line hidden
            BeginContext(5697, 580, true);
            WriteLiteral(@"                <div class=""row"">
                    <div class=""col-md-12"">
                        <div id=""GridPODetail"" class=""font10""></div>

                    </div>
                </div>

            </div>
            <div class=""col-xs-12 col-sm-3 col-lg-1 page-right-content"">
                <div class="" row-navandbuttons"">
                    <div class=""col-xs-12"">
                        <div id=""right-content-buttons"" class="" right-buttons"">
                            <div class=""col-xs-12"">
                                <div class="" "">

");
            EndContext();
#line 143 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                                     if (ViewBag.Mode == "NEW" || ViewBag.Mode == "EDIT")
                                    {

#line default
#line hidden
            BeginContext(7046, 416, true);
            WriteLiteral(@"                                        <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;"">
                                            <button id=""save"" class=""btn btn-primary right-button"" onclick=""onSaveClicked(); return false;""><i class=""glyphicon glyphicon-floppy-disk paddingRight10"" aria-hidden=""true""></i><span>Simpan</span></button>
                                        </div>
");
            EndContext();
#line 148 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                                    }

#line default
#line hidden
            BeginContext(7501, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 150 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                                     if (ViewBag.Mode == "EDIT" || ViewBag.Mode == "VIEW")
                                    {

#line default
#line hidden
            BeginContext(7634, 411, true);
            WriteLiteral(@"                                        <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;"">
                                            <button id=""print"" class=""btn btn-success right-button"" onclick=""onPrintClicked(); return false;""><i class=""glyphicon glyphicon-print paddingRight10"" aria-hidden=""true""></i><span>Print</span></button>
                                        </div>
");
            EndContext();
#line 155 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                                    }

#line default
#line hidden
            BeginContext(8084, 36, true);
            WriteLiteral("                                    ");
            EndContext();
#line 156 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                                     if (ViewBag.Mode == "EDIT" || ViewBag.Mode == "VIEW")
                                    {

#line default
#line hidden
            BeginContext(8215, 412, true);
            WriteLiteral(@"                                        <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;"">
                                            <button id=""back"" class=""btn btn-danger right-button"" onclick=""showlist(); return false;""><i class=""glyphicon glyphicon-arrow-left paddingRight10"" aria-hidden=""true""></i><span>Kembali</span></button>

                                        </div>
");
            EndContext();
#line 162 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebas.cshtml"
                                    }

#line default
#line hidden
            BeginContext(8666, 516, true);
            WriteLiteral(@"                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class=""row row-navigatortoggler"">
                    <div class=""col-xs-12 row-right-button"">
                        <button class=""btn btn-default"" data-toggle=""collapse"" data-target=""#right-content-navigator, #right-content-buttons"" onclick=""return false;"">
                            <span>
                                ");
            EndContext();
            BeginContext(9182, 77, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "184b67bcf0031f4ef1fa83e3ca7317446e0a1a0220263", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(9259, 192, true);
            WriteLiteral("\r\n                            </span>\r\n                        </button>\r\n                    </div>\r\n                </div>\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IFA.Domain.Models.INV_GUDANG_IN> Html { get; private set; }
    }
}
#pragma warning restore 1591
