#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e35b3ef843f0cb37d83f40782dd402d263059556"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_QC_Index), @"mvc.1.0.view", @"/Views/QC/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/QC/Index.cshtml", typeof(AspNetCore.Views_QC_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e35b3ef843f0cb37d83f40782dd402d263059556", @"/Views/QC/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_QC_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/QC/QC.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
  
    ViewData["Title"] = " DAFTAR PENERIMAAN BARANG";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(110, 34, true);
            WriteLiteral("\r\n<script>\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(145, 25, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                 Write(Url.Action("GetQC", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(170, 25, true);
            WriteLiteral("\';\r\n     var urlGetQC = \'");
            EndContext();
            BeginContext(196, 29, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                Write(Url.Action("GetdataQC", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(225, 28, true);
            WriteLiteral("\';\r\n    var urlGetGudang = \'");
            EndContext();
            BeginContext(254, 29, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                   Write(Url.Action("GetGudang", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(283, 32, true);
            WriteLiteral("\';\r\n    var urlGetDetailData = \'");
            EndContext();
            BeginContext(316, 31, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                       Write(Url.Action("GetDetailQC", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(347, 23, true);
            WriteLiteral("\';\r\n    var urlForm = \'");
            EndContext();
            BeginContext(371, 26, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
              Write(Url.Action("EditQC", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(397, 23, true);
            WriteLiteral("\';\r\n    var urlSave = \'");
            EndContext();
            BeginContext(421, 26, false);
#line 13 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
              Write(Url.Action("SaveQC", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(447, 26, true);
            WriteLiteral("\';\r\n    var NoTransList = ");
            EndContext();
            BeginContext(475, 25, false);
#line 14 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                  Write(Html.Raw(ViewBag.NoTrans));

#line default
#line hidden
            EndContext();
            BeginContext(501, 25, true);
            WriteLiteral(";\r\n     var GudangList = ");
            EndContext();
            BeginContext(528, 28, false);
#line 15 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                  Write(Html.Raw(ViewBag.GudangList));

#line default
#line hidden
            EndContext();
            BeginContext(557, 24, true);
            WriteLiteral(";\r\n    var BarangList = ");
            EndContext();
            BeginContext(583, 24, false);
#line 16 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                 Write(Html.Raw(ViewBag.Barang));

#line default
#line hidden
            EndContext();
            BeginContext(608, 19, true);
            WriteLiteral(";\r\n    var Mode = \'");
            EndContext();
            BeginContext(628, 12, false);
#line 17 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
           Write(ViewBag.Mode);

#line default
#line hidden
            EndContext();
            BeginContext(640, 25, true);
            WriteLiteral("\';\r\n     var urlPrint = \'");
            EndContext();
            BeginContext(666, 30, false);
#line 18 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                Write(Url.Action("GetPrintQC", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(696, 25, true);
            WriteLiteral("\';\r\n    var urlCreate = \'");
            EndContext();
            BeginContext(722, 25, false);
#line 19 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                Write(Url.Action("Index", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(747, 22, true);
            WriteLiteral("\';\r\n    var idData = \'");
            EndContext();
            BeginContext(770, 10, false);
#line 20 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
             Write(ViewBag.Id);

#line default
#line hidden
            EndContext();
            BeginContext(780, 29, true);
            WriteLiteral("\';\r\n     var urlInventory = \'");
            EndContext();
            BeginContext(810, 33, false);
#line 21 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                    Write(Url.Action("Create", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(843, 19, true);
            WriteLiteral("\';\r\n\r\n\r\n</script>\r\n");
            EndContext();
            BeginContext(862, 90, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e35b3ef843f0cb37d83f40782dd402d26305955610066", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 25 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
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
            BeginContext(952, 1194, true);
            WriteLiteral(@"
<div class=""panel panel-default"">
    <div class=""panel-heading"">
        <div class=""col-md-12 paddingleft0 divTitle"">
            <div class=""col-md-4 paddingleft5 "">
                <h6 class=""panel-title"">
                    DAFTAR PENERIMAAN BARANG
                </h6>
            </div>
            <div class="" floatright"" style=""float:right;"">
            </div>
        </div>
    </div>
    <div class=""panel-body"">
        <div class="""" style=""min-height:450px;"">
            <div class=""panel-body page-content  container-fluid request-viewrequest"" style=""margin-left:0px;"">
                <div class=""col-xs-12 col-sm-9 col-lg-11 page-left-content"" style=""min-height:450px;"">
                    <div class=""row"" id=""form"">
                        <div class=""col-md-12"">
                            <div class=""col-md-6"">
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                  ");
            WriteLiteral("                      <label class=\"col-md-2 col-form-label\">Nomor PO<span class=\"red\">*</span></label>\r\n                                        <div class=\"col-md-10\">\r\n");
            EndContext();
#line 50 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                             if (ViewBag.Mode == "NEW")
                                            {

#line default
#line hidden
            BeginContext(2369, 173, true);
            WriteLiteral("                                                <select name=\"PONumber\" id=\"PONumber\" class=\"selectpicker width99\" data-live-search=\"true\" onchange=\"onChange();\"></select>\r\n");
            EndContext();
#line 53 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                            }

#line default
#line hidden
            BeginContext(2589, 44, true);
            WriteLiteral("                                            ");
            EndContext();
#line 54 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                             if (ViewBag.Mode == "VIEW")
                                            {

#line default
#line hidden
            BeginContext(2710, 165, true);
            WriteLiteral("                                                <input class=\"form-control form-control inputPink\" value=\"\" id=\"vPONumber\" style=\"width:50%\" readonly=\"readonly\" />\r\n");
            EndContext();
#line 57 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                                //<select name="PONumber" id="PONumber" class="selectpicker width99" data-live-search="true" onchange="onChange();"></select>
                                            }

#line default
#line hidden
            BeginContext(3097, 5804, true);
            WriteLiteral(@"
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">Set Lokasi Simpan<span class=""red"">*</span></label>
                                        <div class=""col-md-5"">
                                            <input class=""form-control"" value="""" id=""lok_simpan"" />
                                        </div>
                                        <div class=""col-md-5"">



                                        </div>
                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=");
            WriteLiteral(@"""col-md-2 col-form-label"">SJ Supplier<span class=""red"">*</span></label>
                                        <div class=""col-md-10"">
                                            <input class=""form-control"" value="""" id=""sj_supplier"" />
                                            <input type=""text"" id=""p_np"" name=""p_np"" hidden=""hidden"" />
                                        </div>

                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">Nama Penyerah</label>
                                        <div class=""col-md-10"">
                                            <input class=""form-control"" value="""" id=""nm_penyerah""  style=""width:50%"" />
                                        </div>

                                    </div>
                  ");
            WriteLiteral(@"              </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">Keterangan</label>
                                        <div class=""col-md-10"">
                                            <textarea class=""form-control"" rows=""4"" id=""Keterangan"" name=""Keterangan"" style=""resize:none;max-width:50%""></textarea>
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div class=""col-md-6"">
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">No Transaksi<span class=""red"">*</span></label>
                                ");
            WriteLiteral(@"        <div class=""col-md-10"">
                                            <input class=""form-control inputPink"" value=""AUTO GENERATED"" id=""NoTransaksi"" disabled />
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">Supplier<span class=""red"">*</span></label>
                                        <div class=""col-md-10"">


                                            <input class=""form-control form-control inputPink"" value="""" id=""Supplier"" style=""width:50%"" disabled />
                                            <input type=""text"" id=""p_np"" name=""p_np"" hidden=""hidden"" />
                                        </div>
                                    </div>
                                </div>
 ");
            WriteLiteral(@"                               <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
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


                     ");
            WriteLiteral(@"       </div>
                        </div>

                    </div>
                    <div class=""row"">
                        <div class=""col-md-12"">
                            <div id=""GridPO""></div>

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
#line 163 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                         if (ViewBag.Mode == "NEW" || ViewBag.Mode == "VIEW" || ViewBag.Mode == "EDIT")
                                        {

#line default
#line hidden
            BeginContext(9065, 432, true);
            WriteLiteral(@"                                            <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;margin-top: 126px;"">
                                                <button id=""back"" class=""btn btn-danger right-button"" onclick=""showCreate(); return false;""><i class=""glyphicon glyphicon-file paddingRight10"" aria-hidden=""true""></i><span>NEW</span></button>
                                            </div>
");
            EndContext();
#line 168 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                        }

#line default
#line hidden
            BeginContext(9540, 40, true);
            WriteLiteral("                                        ");
            EndContext();
#line 169 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                         if (ViewBag.Mode == "NEW" || ViewBag.Mode == "EDIT")
                                        {

#line default
#line hidden
            BeginContext(9678, 428, true);
            WriteLiteral(@"                                            <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;"">
                                                <button id=""save"" class=""btn btn-primary right-button"" onclick=""onSaveClicked(); return false;""><i class=""glyphicon glyphicon-floppy-disk paddingRight10"" aria-hidden=""true""></i><span>Simpan</span></button>
                                            </div>
");
            EndContext();
#line 174 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                        }

#line default
#line hidden
            BeginContext(10149, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 176 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                         if (ViewBag.Mode == "EDIT" || ViewBag.Mode == "VIEW")
                                        {

#line default
#line hidden
            BeginContext(10290, 423, true);
            WriteLiteral(@"                                            <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;"">
                                                <button id=""print"" class=""btn btn-success right-button"" onclick=""onPrintClicked(); return false;""><i class=""glyphicon glyphicon-print paddingRight10"" aria-hidden=""true""></i><span>Print</span></button>
                                            </div>
");
            EndContext();
#line 181 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                        }

#line default
#line hidden
            BeginContext(10756, 40, true);
            WriteLiteral("                                        ");
            EndContext();
#line 182 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                         if (ViewBag.Mode == "EDIT" || ViewBag.Mode == "VIEW")
                                        {

#line default
#line hidden
            BeginContext(10895, 424, true);
            WriteLiteral(@"                                            <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;"">
                                                <button id=""back"" class=""btn btn-danger right-button"" onclick=""showlist(); return false;""><i class=""glyphicon glyphicon-arrow-left paddingRight10"" aria-hidden=""true""></i><span>Kembali</span></button>

                                            </div>
");
            EndContext();
#line 188 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\QC\Index.cshtml"
                                        }

#line default
#line hidden
            BeginContext(11362, 556, true);
            WriteLiteral(@"                                    </div>
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
            BeginContext(11918, 77, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "e35b3ef843f0cb37d83f40782dd402d26305955626162", async() => {
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
            BeginContext(11995, 284, true);
            WriteLiteral(@"
                                </span>
                            </button>
                        </div>
                    </div>

                </div>
            </div>
        </div>


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
