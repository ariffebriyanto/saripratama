#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca446d412f1d70d1849403074ff3fa99c0f9ff8d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PO_ReturnPO), @"mvc.1.0.view", @"/Views/PO/ReturnPO.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PO/ReturnPO.cshtml", typeof(AspNetCore.Views_PO_ReturnPO))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca446d412f1d70d1849403074ff3fa99c0f9ff8d", @"/Views/PO/ReturnPO.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_PO_ReturnPO : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/PO/ReturnPO.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/expand-navigation-icon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("navigatortoggler-icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
  
    ViewData["Title"] = "Return PO";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(94, 12, true);
            WriteLiteral("\r\n<script>\r\n");
            EndContext();
            BeginContext(297, 17, true);
            WriteLiteral("     var Mode = \'");
            EndContext();
            BeginContext(315, 12, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
            Write(ViewBag.Mode);

#line default
#line hidden
            EndContext();
            BeginContext(327, 20, true);
            WriteLiteral("\';\r\n    var idDO = \'");
            EndContext();
            BeginContext(348, 10, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
           Write(ViewBag.Id);

#line default
#line hidden
            EndContext();
            BeginContext(358, 23, true);
            WriteLiteral("\';\r\n    var urlSave = \'");
            EndContext();
            BeginContext(382, 29, false);
#line 13 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
              Write(Url.Action("Saveretur", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(411, 30, true);
            WriteLiteral("\';\r\n    var urlGetReturInv = \'");
            EndContext();
            BeginContext(442, 31, false);
#line 14 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
                     Write(Url.Action("GetReturInv", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(473, 29, true);
            WriteLiteral("\';\r\n    var urlGetReturPO = \'");
            EndContext();
            BeginContext(503, 30, false);
#line 15 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
                    Write(Url.Action("GetReturPO", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(533, 25, true);
            WriteLiteral("\';\r\n    var urlCreate = \'");
            EndContext();
            BeginContext(559, 28, false);
#line 16 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
                Write(Url.Action("ReturnPO", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(587, 27, true);
            WriteLiteral("\';\r\n    var urlGetRetur = \'");
            EndContext();
            BeginContext(615, 28, false);
#line 17 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
                  Write(Url.Action("GetRetur", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(643, 19, true);
            WriteLiteral("\';\r\n\r\n</script>\r\n\r\n");
            EndContext();
            BeginContext(662, 73, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ca446d412f1d70d1849403074ff3fa99c0f9ff8d7314", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 21 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
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
            BeginContext(735, 441, true);
            WriteLiteral(@"

<div class=""panel panel-default"" id=""createForm"">
    <div class=""panel-heading"">
        <div class=""col-md-12 paddingleft0 divTitle"">
            <div class=""col-md-4 paddingleft5 "">
                <h6 class=""panel-title"">
                    FORM Retur Purchasing Order
                </h6>
            </div>

            <div class="" floatright"" style=""float:right;"">

            </div>
        </div>
    </div>

");
            EndContext();
#line 38 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
     using (Html.BeginForm("Save", "Retur", FormMethod.Post, new { id = "form1" }))
    {

#line default
#line hidden
            BeginContext(1268, 6491, true);
            WriteLiteral(@"        <div class="""" style=""min-height:450px;"">
            <div class=""panel-body page-content  container-fluid request-viewrequest"" style=""margin-left:0px;"">
                <div class=""col-xs-12 col-sm-9 col-lg-11 page-left-content"" style=""min-height:450px;"">
                    <div class=""row"">
                        <div class=""col-md-12"">
                            <div class=""col-md-6"">
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">No. Retur<span class=""red"">*</span></label>
                                        <div class=""col-md-10"">
                                            <input class=""form-control inputPink"" value=""AUTO GENERATED"" id=""NoRetur"" disabled />
                                        </div>
                                    </div>
                                </div>

                  ");
            WriteLiteral(@"              <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">No. PO<span class=""red"">*</span></label>
                                        <div class=""col-md-10"">
                                            <select name=""noDO"" id=""noDO"" class=""selectpicker width99 inputGreen"" data-live-search=""true"" onchange=""onNoDOChanged();""></select>
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">No. Inv</label>
                                        <div class=""col-md-10"">
                                            <input class=""form-control"" value="""" id=""NoInv"" disa");
            WriteLiteral(@"bled />
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">Supplier</label>
                                        <div class=""col-md-10"">
                                            <input class=""form-control"" value="""" id=""Supplier"" disabled />
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">Tanggal<span class=""red"">*</span></label>
                                        <div class=""col-xs-12 col-sm-");
            WriteLiteral(@"9 col-lg-4"">
                                            <div class='input-group date' id=""divtanggal"">
                                                <input id='tanggal' type='text' class=""form-control datepicker"" value="""" />
                                                <span class=""input-group-addon"">
                                                    <span class=""glyphicon glyphicon-calendar""></span>
                                                </span>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">No Referensi</label>
                                        <div class=""col-md-10"">
                                            <input cl");
            WriteLiteral(@"ass=""form-control"" value="""" id=""NoReferensi"" />
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">Keterangan</label>
                                        <div class=""col-md-10"">
                                            <input class=""form-control"" value="""" id=""Keterangan"" />
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
                                    <div class=""row"">
                                        <label class=""col-md-2 col-form-label"">Status</label>
                                        <div class=""col-md-10");
            WriteLiteral(@""">
                                            <input class=""form-control"" value="""" id=""Status"" disabled />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-md-12"">
                            <div id=""GridDetails""></div>
                        </div>
                    </div>
                </div>

                <div class=""col-xs-12 col-sm-3 col-lg-1 page-right-content"">
                    <div class="" row-navandbuttons"">
                        <div class=""col-xs-12"">
                            <div id=""right-content-buttons"" class="" right-buttons"">
                                <div class=""col-xs-12"">
                                    <div class="" "">
                                        <div class=""col-xs-6 col-sm-12 row-rig");
            WriteLiteral(@"ht-button"" style=""text-align: left;margin-top: 126px;"">
                                            <button id=""back"" class=""btn btn-success right-button"" onclick=""showCreate(); return false;""><i class=""glyphicon glyphicon-file paddingRight10"" aria-hidden=""true""></i><span>NEW(F7)</span></button>
                                        </div>
");
            EndContext();
#line 136 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
                                         if (ViewBag.Mode == "VIEW")
                                        {

#line default
#line hidden
            BeginContext(7872, 425, true);
            WriteLiteral(@"                                            <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;"">
                                                <button id=""print"" class=""btn btn-success right-button"" onclick=""onPrintClicked(); return false;""><i class=""glyphicon glyphicon-print paddingRight10"" aria-hidden=""true""></i><span>Print</span></button>

                                            </div>
");
            EndContext();
#line 142 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
                                        }

#line default
#line hidden
            BeginContext(8340, 40, true);
            WriteLiteral("                                        ");
            EndContext();
#line 143 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
                                         if (ViewBag.Mode == "EDIT" || ViewBag.Mode == "NEW")
                                        {

#line default
#line hidden
            BeginContext(8478, 446, true);
            WriteLiteral(@"                                            <div class=""col-xs-6 col-sm-12 row-right-button"" style=""text-align: left;"">
                                                <button id=""save"" type=""button"" class=""btn btn-primary right-button"" onclick=""onSaveClicked(); return false;""><i class=""glyphicon glyphicon-floppy-disk paddingRight10"" aria-hidden=""true""></i><span>Simpan(F4)</span></button>
                                            </div>
");
            EndContext();
#line 148 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
                                        }

#line default
#line hidden
            BeginContext(8967, 556, true);
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
            BeginContext(9523, 77, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ca446d412f1d70d1849403074ff3fa99c0f9ff8d19625", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(9600, 204, true);
            WriteLiteral("\r\n                                </span>\r\n                            </button>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 168 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\PO\ReturnPO.cshtml"
    }

#line default
#line hidden
            BeginContext(9811, 12, true);
            WriteLiteral("</div>\r\n\r\n\r\n");
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
