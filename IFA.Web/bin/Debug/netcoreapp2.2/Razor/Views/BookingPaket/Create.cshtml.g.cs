#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "889196eecc2a09c964f8cd2dd7431bcb592bf29d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BookingPaket_Create), @"mvc.1.0.view", @"/Views/BookingPaket/Create.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BookingPaket/Create.cshtml", typeof(AspNetCore.Views_BookingPaket_Create))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"889196eecc2a09c964f8cd2dd7431bcb592bf29d", @"/Views/BookingPaket/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_BookingPaket_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/BookingPaket/Create.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "Y", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "T", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/expand-navigation-icon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("navigatortoggler-icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
  
    ViewData["Title"] = "Create BO Paket";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(100, 64, true);
            WriteLiteral("\r\n<script type=\"text/javascript\">\r\n    var urlGetHargaBarang = \'");
            EndContext();
            BeginContext(165, 44, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
                        Write(Url.Action("GetHargaBarang", "BookingPaket"));

#line default
#line hidden
            EndContext();
            BeginContext(209, 23, true);
            WriteLiteral("\';\r\n    var urlSave = \'");
            EndContext();
            BeginContext(233, 34, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
              Write(Url.Action("Save", "BookingPaket"));

#line default
#line hidden
            EndContext();
            BeginContext(267, 25, true);
            WriteLiteral("\';\r\n    var urlCreate = \'");
            EndContext();
            BeginContext(293, 36, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
                Write(Url.Action("Create", "BookingPaket"));

#line default
#line hidden
            EndContext();
            BeginContext(329, 21, true);
            WriteLiteral("\';\r\n     var Mode = \'");
            EndContext();
            BeginContext(351, 12, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
            Write(ViewBag.Mode);

#line default
#line hidden
            EndContext();
            BeginContext(363, 20, true);
            WriteLiteral("\';\r\n    var idDO = \'");
            EndContext();
            BeginContext(384, 10, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
           Write(ViewBag.Id);

#line default
#line hidden
            EndContext();
            BeginContext(394, 26, true);
            WriteLiteral("\';\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(421, 38, false);
#line 13 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
                 Write(Url.Action("GetPaket", "BookingPaket"));

#line default
#line hidden
            EndContext();
            BeginContext(459, 19, true);
            WriteLiteral("\';\r\n\r\n</script>\r\n\r\n");
            EndContext();
            BeginContext(478, 81, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "889196eecc2a09c964f8cd2dd7431bcb592bf29d7742", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 17 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
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
            BeginContext(559, 430, true);
            WriteLiteral(@"

<div class=""panel panel-default"" id=""createForm"">
    <div class=""panel-heading"">
        <div class=""col-md-12 paddingleft0 divTitle"">
            <div class=""col-md-4 paddingleft5 "">
                <h6 class=""panel-title"">
                    FORM Booking Paket
                </h6>
            </div>

            <div class="" floatright"" style=""float:right;"">

            </div>
        </div>
    </div>
");
            EndContext();
#line 33 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
     using (Html.BeginForm("Save", "DO", FormMethod.Post, new { id = "form1" }))
    {

#line default
#line hidden
            BeginContext(1078, 2031, true);
            WriteLiteral(@"<div class="""" style=""min-height:450px;"">
    <div class=""panel-body page-content  container-fluid request-viewrequest"" style=""margin-left:0px;"">
        <div class=""col-xs-12 col-sm-9 col-lg-11 page-left-content"" style=""min-height:450px;"">
            <div class=""row"">
                <div class=""col-md-12"">
                    <div class=""col-md-6"">
                        <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                            <div class=""row"">
                                <label class=""col-md-2 col-form-label"">Nomor Paket<span class=""red"">*</span></label>
                                <div class=""col-md-10"">
                                    <input class=""form-control inputPink"" value=""AUTO GENERATED"" id=""DONumber"" disabled />
                                    <input class=""form-control inputPink"" value="""" id=""id"" type=""hidden"" />

                                </div>
                            </div>
                        </div>
                  ");
            WriteLiteral(@"      <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"" id=""divdp"">
                            <div class=""row"">
                                <label class=""col-md-2 col-form-label"">Nama Paket<span class=""red"">*</span></label>
                                <div class=""col-md-10"">
                                    <input class=""form-control"" value="""" id=""Nama_Paket"" type=""text"" autocomplete=""off"" />
                                </div>

                            </div>
                        </div>
                        <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                            <div class=""row"">
                                <label class=""col-md-2 col-form-label"">Status<span class=""red"">*</span></label>
                                <div class=""col-md-10"">
                                    <select name=""kategori"" id=""Status"" class=""selectpicker width99 inputGreen"" data-live-search=""true"">
                                        ");
            EndContext();
            BeginContext(3109, 32, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "889196eecc2a09c964f8cd2dd7431bcb592bf29d12627", async() => {
                BeginContext(3127, 5, true);
                WriteLiteral("Aktif");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3141, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(3183, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "889196eecc2a09c964f8cd2dd7431bcb592bf29d14044", async() => {
                BeginContext(3201, 11, true);
                WriteLiteral("Tidak Aktif");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3221, 4399, true);
            WriteLiteral(@"
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=""col-md-6"">
                        <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                            <div class=""row"">
                                <label class=""col-md-2 col-form-label"">Tanggal Mulai<span class=""red"">*</span></label>
                                <div class=""col-xs-12 col-sm-9 col-lg-4"">
                                    <div class='input-group date' id=""divtanggal"">
                                        <input id='tanggal' type='text' class=""form-control datepicker"" value="""" />
                                        <span class=""input-group-addon"">
                                            <span class=""glyphicon glyphicon-calendar""></span>
                                        </span>
                                    </div>
     ");
            WriteLiteral(@"                           </div>
                            </div>
                        </div>
                        <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                            <div class=""row"">
                                <label class=""col-md-2 col-form-label"">Tanggal Berakhir<span class=""red"">*</span></label>
                                <div class=""col-xs-12 col-sm-9 col-lg-4"">
                                    <div class='input-group date' id=""divtanggalakhir"">
                                        <input id='tanggalakhir' type='text' class=""form-control datepicker"" value="""" disabled />
                                        <span class=""input-group-addon"">
                                            <span class=""glyphicon glyphicon-calendar""></span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
   ");
            WriteLiteral(@"                     <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                            <div class=""row"">
                                <div class=""col-md-6"">
                                    <button id=""addKota"" class=""btn btn-primary pull-right"" onclick=""onAddNewRow(); return false;""><i class=""glyphicon glyphicon-plus""></i>&nbsp; Tambah Item(F2)</button>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class=""row"">
                <div class=""col-md-12"">
                    <div id=""GridPaket""></div>
                </div>
            </div>
        </div>
        <div class=""col-xs-12 col-sm-3 col-lg-1 page-right-content"">
            <div class="" row-navandbuttons"">
                <div class=""col-xs-12"">
                    <div id=""right-content-buttons"" class="" right-buttons"">
                        <div class=""col-");
            WriteLiteral(@"xs-12"">
                            <div class="" "">
                                <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;margin-top: 126px;"">
                                    <button id=""back"" class=""btn btn-success right-button"" onclick=""showCreate(); return false;""><i class=""glyphicon glyphicon-file paddingRight10"" aria-hidden=""true""></i><span>NEW(F7)</span></button>
                                </div>
                                <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;"">
                                    <button id=""save"" class=""btn btn-primary right-button"" onclick=""onSaveClicked(); return false;""><i class=""glyphicon glyphicon-floppy-disk paddingRight10"" aria-hidden=""true""></i><span>Simpan(F4)</span></button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class=""row row");
            WriteLiteral(@"-navigatortoggler"">
                <div class=""col-xs-12 row-right-button"">
                    <button class=""btn btn-default"" data-toggle=""collapse"" data-target=""#right-content-navigator, #right-content-buttons"" onclick=""return false;"">
                        <span>
                            ");
            EndContext();
            BeginContext(7620, 77, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "889196eecc2a09c964f8cd2dd7431bcb592bf29d20070", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(7697, 154, true);
            WriteLiteral("\r\n                        </span>\r\n                    </button>\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n    \r\n</div>\r\n");
            EndContext();
#line 150 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\BookingPaket\Create.cshtml"
    }

#line default
#line hidden
            BeginContext(7858, 10, true);
            WriteLiteral("</div>\r\n\r\n");
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
