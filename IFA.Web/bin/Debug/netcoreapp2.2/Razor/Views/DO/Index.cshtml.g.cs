#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5ec6ee73ad8b6c90259983379734758ab14f85e4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DO_Index), @"mvc.1.0.view", @"/Views/DO/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DO/Index.cshtml", typeof(AspNetCore.Views_DO_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5ec6ee73ad8b6c90259983379734758ab14f85e4", @"/Views/DO/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_DO_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/DO/Index.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "PERSIAPAN BARANG", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "REGULER", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "TOP URGENT", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "URGENT", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "CASH", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "BOOKING ORDER", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
  
    ViewData["Title"] = "Monitoring SO";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(98, 59, true);
            WriteLiteral("\r\n\r\n<script type=\"text/javascript\">\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(158, 32, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                 Write(Url.Action("GetDOPartial", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(190, 25, true);
            WriteLiteral("\';\r\n    var urlCreate = \'");
            EndContext();
            BeginContext(216, 26, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                Write(Url.Action("Create", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(242, 28, true);
            WriteLiteral("\';\r\n   // var urlGetData = \'");
            EndContext();
            BeginContext(271, 29, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                   Write(Url.Action("GetDO_mon", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(300, 26, true);
            WriteLiteral("\';\r\n    var urlGetByDO = \'");
            EndContext();
            BeginContext(327, 25, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                 Write(Url.Action("GetDO", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(352, 28, true);
            WriteLiteral("\';\r\n    var urlGetByStok = \'");
            EndContext();
            BeginContext(381, 29, false);
#line 13 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                   Write(Url.Action("GetByStok", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(410, 28, true);
            WriteLiteral("\';\r\n    var urlGetByCust = \'");
            EndContext();
            BeginContext(439, 29, false);
#line 14 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                   Write(Url.Action("GetByCust", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(468, 31, true);
            WriteLiteral("\';\r\n    var urlGetDataInden = \'");
            EndContext();
            BeginContext(500, 30, false);
#line 15 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                      Write(Url.Action("GetIndenDO", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(530, 23, true);
            WriteLiteral("\';\r\n    var salesID = \'");
            EndContext();
            BeginContext(554, 15, false);
#line 16 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
              Write(ViewBag.salesID);

#line default
#line hidden
            EndContext();
            BeginContext(569, 23, true);
            WriteLiteral("\';\r\n    var RoleName= \'");
            EndContext();
            BeginContext(593, 16, false);
#line 17 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
              Write(ViewBag.RoleName);

#line default
#line hidden
            EndContext();
            BeginContext(609, 30, true);
            WriteLiteral("\';\r\n    var urlDeleteInden = \'");
            EndContext();
            BeginContext(640, 31, false);
#line 18 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                     Write(Url.Action("DeleteInden", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(671, 32, true);
            WriteLiteral("\';\r\n    var urlGetDetailData = \'");
            EndContext();
            BeginContext(704, 31, false);
#line 19 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                       Write(Url.Action("GetDODetail", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(735, 28, true);
            WriteLiteral("\';\r\n    var urlDeleteData= \'");
            EndContext();
            BeginContext(764, 26, false);
#line 20 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                   Write(Url.Action("Delete", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(790, 33, true);
            WriteLiteral("\';\r\n    var urlGetDetailInden = \'");
            EndContext();
            BeginContext(824, 28, false);
#line 21 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                        Write(Url.Action("GetInden", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(852, 28, true);
            WriteLiteral("\';\r\n        var urlInden = \'");
            EndContext();
            BeginContext(881, 25, false);
#line 22 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                   Write(Url.Action("Inden", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(906, 34, true);
            WriteLiteral("\';\r\n        var urlGetCustomer = \'");
            EndContext();
            BeginContext(941, 31, false);
#line 23 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                         Write(Url.Action("GetCustomer", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(972, 34, true);
            WriteLiteral("\';\r\n          var urlGetBarang = \'");
            EndContext();
            BeginContext(1007, 29, false);
#line 24 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
                         Write(Url.Action("GetBarang", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(1036, 17, true);
            WriteLiteral("\';\r\n</script>\r\n\r\n");
            EndContext();
            BeginContext(1053, 70, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ec6ee73ad8b6c90259983379734758ab14f85e412074", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 27 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\DO\Index.cshtml"
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
            BeginContext(1123, 251, true);
            WriteLiteral("\r\n\r\n\r\n<div id=\"wrapperList\">\r\n    <div class=\"panel-title\">\r\n        <div class=\"col-md-12\" style=\"margin-bottom: 15px;\">\r\n            <span class=\"fontTitle\">Monitoring Delivery Order</span>\r\n        </div>\r\n    </div>\r\n    <div class=\"panel-body\">\r\n");
            EndContext();
            BeginContext(1394, 4042, true);
            WriteLiteral(@"        <div class=""col-xs-12 col-sm-9 col-lg-12 form-c-inline"">
            <div class=""row"">
                <div class=""col-md-12"">
                    <div class=""col-md-6"">
                        <label class=""col-md-1 col-form-label"" style=""margin-left: 10px;"">NO SO<span></span></label>
                        <div class=""col-md-3"">


                            <input class=""form-control form-control"" value="""" id=""no_sp"" />
                            <input type=""text"" id=""p_np"" name=""p_np"" hidden=""hidden"" />

                        </div>
                        <div class=""col-md-2"">
                            <button id=""addPO"" class=""btn btn-info pull-right"" onclick=""getByDO();""><i class=""glyphicon glyphicon-search""></i>&nbsp; Search</button>
                        </div>
                    </div>

                    <div class=""col-md-6"">


                        <label class=""col-md-2 col-form-label"">Customer<span class=""red"">*</span></label>
                        <");
            WriteLiteral(@"div class=""col-md-8"">
                            <select name=""Kd_Customer"" id=""Kd_Customer"" class=""selectpicker w-100 inputGreen"" data-live-search=""true""></select>



                        </div>
                        <div class=""col-md-2"">
                            <button id=""addCustomer"" type=""button"" class=""btn btn-primary"" onclick=""getByCust(); ""><i class=""glyphicon glyphicon-search""></i>Search</button>
                        </div>

                    </div>
                    <div class=""col-md-12"" style=""margin-bottom: 15px;"">
                        <div class=""col-md-6"">

                            <div class="""">
                                <label class=""col-md-1 col-form-label"" style=""margin-left: 10px;"">Tanggal:</label>
                                <div class=""col-md-3"">
                                    <div class='input-group date' id=""divtanggalfrom"">
                                        <input id='tanggalfrom' type='text' class=""form-control datepicker");
            WriteLiteral(@""" value="""" />
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
                                    </div>
                                </div>
                                ");
            WriteLiteral(@"<div class=""col-md-2"">
                                    <button id=""addCustomer"" type=""button"" class=""btn btn-primary"" onclick=""getByDO(); ""><i class=""glyphicon glyphicon-search""></i>Search</button>
                                </div>
                            </div>

                        </div>
                        <div class=""col-md-6 hidden"">

                            <label class=""col-md-2 col-form-label"">Barang:</label>
                            <div class=""col-md-8"">
                                <input class=""form-control w-100"" value="""" id=""barang"" />
                            </div>
                            <div class=""col-md-2"">
                                <button id=""addCustomer"" type=""button"" class=""btn btn-primary"" onclick=""getByStok(); ""><i class=""glyphicon glyphicon-search""></i>Search</button>
                            </div>




                        </div>

                    </div>
");
            EndContext();
            BeginContext(5468, 538, true);
            WriteLiteral(@"                    <div class=""col-md-12"" style=""margin-bottom: 15px;"">
                        <div class=""col-md-8"">

                            <div class="""">
                                <label class=""col-md-1 col-form-label"" style=""margin-left: 10px;"">Status:</label>
                                <div class=""col-md-3"">
                                    <select name=""status"" id=""status"" class=""selectpicker width170"" data-live-search=""true"" onchange=""oncboFilterChanged();"">
                                        ");
            EndContext();
            BeginContext(6006, 29, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ec6ee73ad8b6c90259983379734758ab14f85e419376", async() => {
                BeginContext(6023, 3, true);
                WriteLiteral("ALL");
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
            BeginContext(6035, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(6077, 58, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ec6ee73ad8b6c90259983379734758ab14f85e420791", async() => {
                BeginContext(6110, 16, true);
                WriteLiteral("PERSIAPAN BARANG");
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
            BeginContext(6135, 472, true);
            WriteLiteral(@"
                                    </select>
                                </div>
                                <label class=""col-md-1 col-form-label"" style=""margin-left: 10px;"">Jenis DO:</label>
                                <div class=""col-md-3"">
                                    <select name=""jenisDO"" id=""jenisDO"" class=""selectpicker width99 inputGreen"" data-live-search=""true"" onchange=""oncboFilterChanged()"">
                                        ");
            EndContext();
            BeginContext(6607, 29, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ec6ee73ad8b6c90259983379734758ab14f85e422666", async() => {
                BeginContext(6624, 3, true);
                WriteLiteral("ALL");
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
            BeginContext(6636, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(6678, 40, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ec6ee73ad8b6c90259983379734758ab14f85e424081", async() => {
                BeginContext(6702, 7, true);
                WriteLiteral("REGULER");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6718, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(6760, 46, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ec6ee73ad8b6c90259983379734758ab14f85e425500", async() => {
                BeginContext(6787, 10, true);
                WriteLiteral("TOP URGENT");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6806, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(6848, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ec6ee73ad8b6c90259983379734758ab14f85e426923", async() => {
                BeginContext(6871, 6, true);
                WriteLiteral("URGENT");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6886, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(6928, 34, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ec6ee73ad8b6c90259983379734758ab14f85e428341", async() => {
                BeginContext(6949, 4, true);
                WriteLiteral("CASH");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6962, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(7004, 52, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ec6ee73ad8b6c90259983379734758ab14f85e429757", async() => {
                BeginContext(7034, 13, true);
                WriteLiteral("BOOKING ORDER");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(7056, 442, true);
            WriteLiteral(@"
                                    </select>
                                </div>
                            </div>

                        </div>
                        <div class=""col-md-4"">
                            <button id=""addPO"" class=""btn btn-primary pull-right"" onclick=""addnewDO();""><i class=""glyphicon glyphicon-plus""></i>&nbsp; Tambah DO</button>
                        </div>


                    </div>
");
            EndContext();
            BeginContext(7528, 2355, true);
            WriteLiteral(@"                    <div class=""col-md-12"" style=""display:inline-block;"">

                        <ul class=""nav nav-tabs"">
                            <li class=""tabpage active""><a data-toggle=""tab"" href=""#detail1"">Sales Order</a></li>
                            <li class=""tabpage""><a data-toggle=""tab"" href=""#uraian"">Inden</a></li>
                        </ul>

                        <div class=""tab-content"">

                            <div id=""detail1"" class=""tab-pane fade in active show"">
                                <div id=""GridDO""></div>
                                <script type=""text/x-kendo-template"" id=""template"">
                                    <div class=""tabstrip"">
                                        <ul>
                                            <li class=""k-state-active"">
                                                Details
                                            </li>
                                        </ul>
                                   ");
            WriteLiteral(@"     <div>
                                            <div class=""detail""></div>
                                        </div>

                                    </div>

                                </script>
                            </div>
                            <div id=""uraian"" class=""tab-pane in fade"">
                                <div id=""DPMGrid""></div>
                                <script type=""text/x-kendo-template"" id=""templateInden"">
                                    <div class=""tabstrip"">
                                        <ul>
                                            <li class=""k-state-active"">
                                                Details
                                            </li>
                                        </ul>
                                        <div>
                                            <div class=""detail""></div>
                                        </div>

                                    </div>");
            WriteLiteral(@"

                                </script>
                            </div>
                        </div>

                    </div>
                </div>



            </div>
            <div id=""editForm""></div>
            <div id=""dialog""></div>

        </div>
    </div>
</div>");
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
