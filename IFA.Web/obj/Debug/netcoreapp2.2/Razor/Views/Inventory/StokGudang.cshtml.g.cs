#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80abfff740b229483a3d398646ae169b3c3e7bab"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Inventory_StokGudang), @"mvc.1.0.view", @"/Views/Inventory/StokGudang.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Inventory/StokGudang.cshtml", typeof(AspNetCore.Views_Inventory_StokGudang))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"80abfff740b229483a3d398646ae169b3c3e7bab", @"/Views/Inventory/StokGudang.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Inventory_StokGudang : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/Inventory/StokGudang.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
  
    ViewData["Title"] = "Monitoring Stok Gudang";

#line default
#line hidden
            BeginContext(60, 34, true);
            WriteLiteral("\r\n<script>\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(95, 47, false);
#line 7 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
                 Write(Url.Action("GetStokGudangPartial", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(142, 34, true);
            WriteLiteral("\';\r\n    //var urlGetDetailData = \'");
            EndContext();
            BeginContext(177, 42, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
                         Write(Url.Action("GetDetailTerima", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(219, 23, true);
            WriteLiteral("\';\r\n    var urlForm = \'");
            EndContext();
            BeginContext(243, 26, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
              Write(Url.Action("EditPO", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(269, 23, true);
            WriteLiteral("\';\r\n    var urlSave = \'");
            EndContext();
            BeginContext(293, 26, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
              Write(Url.Action("SavePO", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(319, 25, true);
            WriteLiteral("\';\r\n    var urlCreate = \'");
            EndContext();
            BeginContext(345, 33, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
                Write(Url.Action("Create", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(378, 24, true);
            WriteLiteral("\';\r\n    var urlPrint = \'");
            EndContext();
            BeginContext(403, 30, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
               Write(Url.Action("GetPrintPO", "PO"));

#line default
#line hidden
            EndContext();
            BeginContext(433, 29, true);
            WriteLiteral("\';\r\n    var urlGetPeriode = \'");
            EndContext();
            BeginContext(463, 37, false);
#line 13 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
                    Write(Url.Action("GetPeriode", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(500, 37, true);
            WriteLiteral("\';\r\n   \r\n    var urlGetDetailData = \'");
            EndContext();
            BeginContext(538, 46, false);
#line 15 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
                       Write(Url.Action("GetDetailStokGudang", "Inventory"));

#line default
#line hidden
            EndContext();
            BeginContext(584, 28, true);
            WriteLiteral("\';\r\n    var urlGetBarang = \'");
            EndContext();
            BeginContext(613, 29, false);
#line 16 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
                   Write(Url.Action("GetBarang", "QC"));

#line default
#line hidden
            EndContext();
            BeginContext(642, 15, true);
            WriteLiteral("\';\r\n</script>\r\n");
            EndContext();
            BeginContext(657, 105, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80abfff740b229483a3d398646ae169b3c3e7bab7820", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 18 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Inventory\StokGudang.cshtml"
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
            BeginContext(762, 4207, true);
            WriteLiteral(@"
<style>
    /*.k-autocomplete {
        width: 300px;
        height: 35px;
        
    }*/
    .k-input {
        height: 29px;
        width: 250px;
        /*background-color:mediumseagreen;*/
    }

    /*.k-widget.k-autocomplete {
        width: 300px;
    }*/
    .btn-light {
        /*background-color: mediumseagreen;*/
    }
    /*.dropdown-toggle.bs-placeholder {
        width: 150px;
    }*/
    /*.bootstrap-select >.dropdown-toggle {
        width: 150px;
    }
    .bootstrap-select .dropdown-menu {
        width: 150px;
    }*/

    .form-group input[type=""checkbox""] {
        display: none;
    }

        .form-group input[type=""checkbox""] + .btn-group > label span {
            width: 20px;
        }

            .form-group input[type=""checkbox""] + .btn-group > label span:first-child {
                display: none;
            }

            .form-group input[type=""checkbox""] + .btn-group > label span:last-child {
                display: inline-bloc");
            WriteLiteral(@"k;
            }

        .form-group input[type=""checkbox""]:checked + .btn-group > label span:first-child {
            display: inline-block;
        }

        .form-group input[type=""checkbox""]:checked + .btn-group > label span:last-child {
            display: none;
        }


</style>
<div id=""wrapperList"">
    <div class=""panel-title"">
        <div class=""col-md-12"" style=""margin-bottom: 15px;"">
            <span class=""fontTitle"">Monitoring Barang per Gudang</span>
        </div>
    </div>

    <div class=""col-md-12"" style=""margin-bottom: 15px;"">
        <div class=""col-md-12"">
            <div class=""col-md-3"">
                <div class="""">
                    <label class=""col-md-2 col-form-label"">Periode:</label>
                    <div class=""col-md-9 widthcboCustom"">
                        <select name=""periode"" id=""periode"" class=""selectpicker width99"" data-live-search=""true""></select>

                    </div>
                </div>
            </div>
     ");
            WriteLiteral(@"       <div class=""col-md-3"">
                <div class="""">
                    <label class=""col-md-2 col-form-label"">Barang:</label>
                    <div class=""col-md-8"">
                        <input id=""barang"" class=""form-control"" placeholder=""Filter Barang "" />
                    </div>
                </div>
            </div>
            <div class=""col-md-6"">
                <div class="""">
                    <div class=""col-md-3"">
                        <button id=""button"" onclick=""searchBarang();"" class=""btn btn-info""><i class=""glyphicon glyphicon-search""></i>&nbsp; Search</button>

                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>

<div class=""form-row col-md-12"" style=""margin-bottom: 10px;"">
    <div class=""col-md-3"">
        <div class=""[ form-group ]"">
            <input type=""checkbox"" name=""fancy-checkbox-default"" id=""fancy-checkbox-default"" autocomplete=""off"" />
            <div class=""[ btn-group ]"">
 ");
            WriteLiteral(@"               <label for=""fancy-checkbox-default"" class=""[ btn btn-danger ]"" style=""width:85px"">
                    <span class=""[ glyphicon glyphicon-ok ]""></span>
                    <span> </span>
                </label>
                <label for=""fancy-checkbox-default"" class=""[ btn btn-default active ]""  style=""width:135px"">
                    Stock Limit
                </label>
            </div>
        </div>
    </div>
    <div class=""col-md-3"">
        <div class=""[ form-group ]"">
            <input type=""checkbox"" name=""fancy-checkbox-primary"" id=""fancy-checkbox-primary"" autocomplete=""off"" />
            <div class=""[ btn-group ]"">
                <label for=""fancy-checkbox-primary"" class=""[ btn btn-primary ]"" style=""width:85px;margin-left:4px"">
                    <span class=""[ glyphicon glyphicon-ok ]""></span>
                    <span> </span>
                </label>
                <label for=""fancy-checkbox-primary"" class=""[ btn btn-default active ]"" style=""width:164p");
            WriteLiteral("x\">\r\n                    Stock Aman\r\n                </label>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            EndContext();
            BeginContext(6618, 1387, true);
            WriteLiteral(@"
    </div>




    <div class="""" style=""display:inline-block;"">
        <div id=""GridGudangStock"" ></div>
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
    <div class="""" style=""display:inline-block;"">
        <div class=""col-md-12"" style=""margin-bottom: 15px;margin-top: 15px;"">
            <span class=""fontTitle"">Detail Barang per Gudang</span>
        </div>
    </div>

        <div class="""" style=""display:inline-block;"">
            <div id=""GridDetailGudangStock""></div>
            <script type=""text/x-kendo-template"" id=""template"">
                <div class=""tabstrip"">
                    <ul>
                        <li class=""k-state-");
            WriteLiteral(@"active"">
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
