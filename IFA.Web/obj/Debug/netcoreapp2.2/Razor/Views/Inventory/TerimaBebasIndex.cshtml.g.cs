#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebasIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "30109dc98de6fd19d53705314c8d07065d222141"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Inventory_TerimaBebasIndex), @"mvc.1.0.view", @"/Views/Inventory/TerimaBebasIndex.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Inventory/TerimaBebasIndex.cshtml", typeof(AspNetCore.Views_Inventory_TerimaBebasIndex))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"30109dc98de6fd19d53705314c8d07065d222141", @"/Views/Inventory/TerimaBebasIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Inventory_TerimaBebasIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/Inventory/TerimaBarang/Index.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "ENTRY", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "REVISE", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "OPEN", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "OUTSTANDING", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "CLOSE", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "BATAL", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebasIndex.cshtml"
  
    ViewData["Title"] = "Monitoring Terima Barang";

#line default
#line hidden
            BeginContext(62, 34, true);
            WriteLiteral("\r\n<script>\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(97, 41, false);
#line 7 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebasIndex.cshtml"
                 Write(Url.Action("GetTerimaBebas", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(138, 32, true);
            WriteLiteral("\';\r\n    var urlGetDetailData = \'");
            EndContext();
            BeginContext(171, 47, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebasIndex.cshtml"
                       Write(Url.Action("GetDetailTerimaBebas", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(218, 25, true);
            WriteLiteral("\';\r\n    var urlCreate = \'");
            EndContext();
            BeginContext(244, 33, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebasIndex.cshtml"
                Write(Url.Action("Create", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(277, 24, true);
            WriteLiteral("\';\r\n    var urlPrint = \'");
            EndContext();
            BeginContext(302, 41, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebasIndex.cshtml"
               Write(Url.Action("GetPrintTerima", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(343, 17, true);
            WriteLiteral("\';\r\n\r\n</script>\r\n");
            EndContext();
            BeginContext(360, 113, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30109dc98de6fd19d53705314c8d07065d2221418184", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 13 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\TerimaBebasIndex.cshtml"
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
            BeginContext(473, 3020, true);
            WriteLiteral(@"

<div id=""wrapperList"">
    <div class=""panel-title"">
        <div class=""col-md-12"" style=""margin-bottom: 15px;"">
            <span class=""fontTitle"">Monitoring Terima Barang</span>
        </div>
    </div>

    <div class=""col-md-12"" style=""margin-bottom: 15px;"">
        <div class=""col-md-8"">
            <div class=""col-md-3"">
                <div class="""">
                    <label class=""col-md-4 col-form-label"">PO Number:</label>
                    <div class=""col-md-8"">
                        <input class=""form-control"" value="""" id=""PONo"" />

                    </div>
                </div>
            </div>
            <div class=""col-md-9"">
                <div class="""">
                    <label class=""col-md-1 col-form-label"" style=""margin-left: 10px;"">Tanggal:</label>
                    <div class=""col-md-3"">
                        <div class='input-group date' id=""divtanggalfrom"">
                            <input id='tanggalfrom' type='text' class=""form-contro");
            WriteLiteral(@"l datepicker"" value="""" />
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
                    <div class=""col-md-2"">
                        <button id=""addPO"" class=""btn btn-info pull-right"" onclick=""getData();""><i class=""glyphicon glyphicon-search""></i>&nbsp;");
            WriteLiteral(@" Search</button>
                    </div>
                </div>
            </div>
        </div>
        <div class=""col-md-4"">
            <button id=""addPO"" class=""btn btn-primary pull-right"" onclick=""addnewPO();""><i class=""glyphicon glyphicon-plus""></i>&nbsp; Tambah PO</button>
            <button id=""btnPrint"" class=""btn btn-info pull-right"" onclick=""printpage();"" style=""margin-right: 15px;display:none;""><i class=""glyphicon glyphicon-print""></i>&nbsp; Cetak</button>

        </div>
    </div>

    <div class=""col-md-12"" style=""margin-bottom: 15px;"">
        <div class=""col-md-8"">
            <div class=""col-md-4"">
                <div class="""">
                    <label class=""col-md-3 col-form-label"">Status:</label>
                    <div class=""col-md-8"">
                        <select name=""status"" id=""status"" class=""selectpicker width170"" data-live-search=""true"" onchange=""oncboFilterChanged();"">
                            ");
            EndContext();
            BeginContext(3493, 29, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30109dc98de6fd19d53705314c8d07065d22214113459", async() => {
                BeginContext(3510, 3, true);
                WriteLiteral("ALL");
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
            BeginContext(3522, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(3552, 36, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30109dc98de6fd19d53705314c8d07065d22214114862", async() => {
                BeginContext(3574, 5, true);
                WriteLiteral("ENTRY");
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
            BeginContext(3588, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(3618, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30109dc98de6fd19d53705314c8d07065d22214116267", async() => {
                BeginContext(3641, 6, true);
                WriteLiteral("REVISE");
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
            BeginContext(3656, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(3686, 34, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30109dc98de6fd19d53705314c8d07065d22214117673", async() => {
                BeginContext(3707, 4, true);
                WriteLiteral("OPEN");
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
            BeginContext(3720, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(3750, 48, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30109dc98de6fd19d53705314c8d07065d22214119077", async() => {
                BeginContext(3778, 11, true);
                WriteLiteral("OUTSTANDING");
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
            BeginContext(3798, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(3828, 36, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30109dc98de6fd19d53705314c8d07065d22214120489", async() => {
                BeginContext(3850, 5, true);
                WriteLiteral("CLOSE");
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
            BeginContext(3864, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(3894, 36, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30109dc98de6fd19d53705314c8d07065d22214121894", async() => {
                BeginContext(3916, 5, true);
                WriteLiteral("BATAL");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3930, 699, true);
            WriteLiteral(@"
                        </select>
                    </div>
                </div>
            </div>
        </div>

    </div>


    <div class="""" style=""display:inline-block;"">
        <div id=""GridTerima""></div>
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