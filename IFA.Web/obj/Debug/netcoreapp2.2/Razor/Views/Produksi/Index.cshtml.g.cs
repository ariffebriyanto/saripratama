#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "044a6622957a66409bdc468991cd62a802ba3177"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Produksi_Index), @"mvc.1.0.view", @"/Views/Produksi/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Produksi/Index.cshtml", typeof(AspNetCore.Views_Produksi_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"044a6622957a66409bdc468991cd62a802ba3177", @"/Views/Produksi/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Produksi_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Script/Produksi/Index.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml"
  
    ViewData["Title"] = "Monitoring Rencana Pengiriman";

#line default
#line hidden
            BeginContext(67, 34, true);
            WriteLiteral("\r\n<script>\r\n    var urlGetData = \'");
            EndContext();
            BeginContext(102, 45, false);
#line 7 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml"
                 Write(Url.Action("GetMonrcnkrmPartial", "Produksi"));

#line default
#line hidden
            EndContext();
            BeginContext(147, 32, true);
            WriteLiteral("\';\r\n    var urlGetDetailData = \'");
            EndContext();
            BeginContext(180, 38, false);
#line 8 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml"
                       Write(Url.Action("GetDetrcnkrm", "Produksi"));

#line default
#line hidden
            EndContext();
            BeginContext(218, 25, true);
            WriteLiteral("\';\r\n    var urlCreate = \'");
            EndContext();
            BeginContext(244, 32, false);
#line 9 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml"
                Write(Url.Action("Create", "Produksi"));

#line default
#line hidden
            EndContext();
            BeginContext(276, 24, true);
            WriteLiteral("\';\r\n    var urlPrint = \'");
            EndContext();
            BeginContext(301, 39, false);
#line 10 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml"
               Write(Url.Action("PrintrcnKirim", "Produksi"));

#line default
#line hidden
            EndContext();
            BeginContext(340, 25, true);
            WriteLiteral("\';\r\n    var urlDelete = \'");
            EndContext();
            BeginContext(366, 40, false);
#line 11 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml"
                Write(Url.Action("Deletercnkirim", "Produksi"));

#line default
#line hidden
            EndContext();
            BeginContext(406, 24, true);
            WriteLiteral("\';\r\n    var urlIndex = \'");
            EndContext();
            BeginContext(431, 31, false);
#line 12 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml"
               Write(Url.Action("Index", "Produksi"));

#line default
#line hidden
            EndContext();
            BeginContext(462, 31, true);
            WriteLiteral("\';\r\n     var urlGetCustomer = \'");
            EndContext();
            BeginContext(494, 31, false);
#line 13 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml"
                      Write(Url.Action("GetCustomer", "DO"));

#line default
#line hidden
            EndContext();
            BeginContext(525, 17, true);
            WriteLiteral("\';\r\n\r\n</script>\r\n");
            EndContext();
            BeginContext(542, 99, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "044a6622957a66409bdc468991cd62a802ba31776960", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 16 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\Produksi\Index.cshtml"
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
            BeginContext(641, 4615, true);
            WriteLiteral(@"

<div id=""wrapperList"">
    <div class=""panel-title"">
        <div class=""col-md-12"" style=""margin-bottom: 15px;"">
            <span class=""fontTitle"">Monitoring Rencana Pengiriman</span>
        </div>
    </div>

    <div class=""col-md-12"" style=""margin-bottom: 15px;"">
        <div class=""col-md-8"">
            <div class=""col-md-4"">
                <div class="""">
                    <label class=""col-md-4 col-form-label"">SO Number:</label>
                    <div class=""col-md-8"">
                        <input class=""form-control"" value="""" id=""SONo"" />

                    </div>
                </div>
            </div>
            <div class=""col-md-8"">


                <label class=""col-md-2 col-form-label"">Customer</label>
                <div class=""col-md-5"">
                    <select name=""Kd_Customer"" id=""Kd_Customer"" class=""selectpicker w-100 inputGreen"" data-live-search=""true""></select>



                </div>
                <div class=""col-md-3"">
         ");
            WriteLiteral(@"           <button id=""addCustomer"" class=""btn btn-info pull-right"" onclick=""getByCust();""><i class=""glyphicon glyphicon-search""></i>&nbsp; Search</button>
                </div>

            </div>
            <div class=""col-md-9"">
                <div class="""">
                    <label class=""col-md-1 col-form-label"" style=""margin-left: 10px;"">Tanggal:</label>
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
                        <div class=");
            WriteLiteral(@"'input-group date' id=""divtanggalto"">
                            <input id='tanggalto' type='text' class=""form-control datepicker"" value="""" />
                            <span class=""input-group-addon"">
                                <span class=""glyphicon glyphicon-calendar""></span>
                            </span>
                        </div>
                    </div>
                    <div class=""col-md-2"">
                        <button id=""addPO"" class=""btn btn-info pull-right"" onclick=""searchPO();""><i class=""glyphicon glyphicon-search""></i>&nbsp; Search</button>
                    </div>
                </div>
            </div>
            <div class=""col-md-6 hidden"">

                <label class=""col-md-2 col-form-label"">Barang:</label>
                <div class=""col-md-8"">
                    <input class=""form-control w-100"" value="""" id=""barang"" />
                </div>
                <div class=""col-md-2"">
                    <button id=""addCustomer"" type=""butto");
            WriteLiteral(@"n"" class=""btn btn-primary"" onclick=""getByStok(); ""><i class=""glyphicon glyphicon-search""></i>Search</button>
                </div>




            </div>
        </div>
        <div class=""col-md-4"">
            <button id=""addPO"" class=""btn btn-primary pull-right"" onclick=""addnewRcn();""><i class=""glyphicon glyphicon-plus""></i>&nbsp; Tambah Rencana Pengiriman</button>
            <button id=""btnDelete"" class=""btn btn-info pull-right"" onclick=""DeletePage();"" style=""margin-right: 15px;display:none;""><i class=""glyphicon glyphicon-print""></i>&nbsp; Cetak</button>
        </div>
    </div>



    <div class=""col-md-12"" style=""margin-bottom: 15px;"">
        <div class=""col-md-8"">
            <div class=""col-md-4"">

            </div>
        </div>

    </div>



    <div class=""col-md-12"" style=""display:inline-block;"">
        <div id=""GridTerima"" class=""font10""></div>
        <script type=""text/x-kendo-template"" id=""template"">
            <div class=""tabstrip"">
                <ul>");
            WriteLiteral(@"
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


<style>

    /*horizontal Grid scrollbar should appear if the browser window is shrinked too much*/
    #grid table {
        min-width: 1190px;
    }
</style>
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