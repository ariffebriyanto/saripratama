#pragma checksum "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MBarang\EditBrg.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f425e28aeb95f0e134aaaad902e40d616802e74c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MBarang_EditBrg), @"mvc.1.0.view", @"/Views/MBarang/EditBrg.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/MBarang/EditBrg.cshtml", typeof(AspNetCore.Views_MBarang_EditBrg))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f425e28aeb95f0e134aaaad902e40d616802e74c", @"/Views/MBarang/EditBrg.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d7fe01f2d4927fabe17f67d3f0d0e011f6f30d2", @"/Views/_ViewImports.cshtml")]
    public class Views_MBarang_EditBrg : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IFA.Domain.Models.SIF_Barang>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/expand-navigation-icon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("navigatortoggler-icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(37, 411, true);
            WriteLiteral(@"
<div class=""panel panel-default"">
    <div class=""panel-heading"">
        <div class=""col-md-12 paddingleft0 divTitle"">
            <div class=""col-md-4 paddingleft5 "">
                <h6 class=""panel-title"">
                    Master Kota
                </h6>

            </div>

            <div class="" floatright"" style=""float:right;"">

            </div>
        </div>

    </div>

");
            EndContext();
#line 20 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MBarang\EditBrg.cshtml"
     using (Html.BeginForm("UpdateBrg", "MBarang", FormMethod.Post, new { id = "form1" }))
    {

#line default
#line hidden
            BeginContext(547, 638, true);
            WriteLiteral(@"        <div class="""" style=""min-height:450px;"">
            <div class=""panel-body page-content  container-fluid request-viewrequest"" style=""margin-left:0px;"">
                <div class=""col-xs-12 col-sm-9 col-lg-10 page-left-content"" style=""min-height:450px;"">
                    <div class=""row"" id=""form"">

                        <div class=""col-md-6 form-c-inline"">
                            <div class=""row"">
                                <label class=""col-md-2 col-form-label"">Kode Barang<span class=""red"">*</span></label>
                                <div class=""col-md-10"">
                                    ");
            EndContext();
            BeginContext(1186, 96, false);
#line 31 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MBarang\EditBrg.cshtml"
                               Write(Html.TextBoxFor(model => model.Kode_Barang, new { @class = "form-control", id = "Kode_Barang" }));

#line default
#line hidden
            EndContext();
            BeginContext(1282, 432, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>

                        <div class=""col-md-6 form-c-inline"">
                            <div class=""row"">
                                <label class=""col-md-2 col-form-label"">Nama Barang<span class=""red"">*</span></label>
                                <div class=""col-md-10"">
                                    ");
            EndContext();
            BeginContext(1715, 96, false);
#line 40 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MBarang\EditBrg.cshtml"
                               Write(Html.TextBoxFor(model => model.Nama_Barang, new { @class = "form-control", id = "Nama_Barang" }));

#line default
#line hidden
            EndContext();
            BeginContext(1811, 430, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-6 form-c-inline"">
                            <div class=""row"">
                                <label class=""col-md-2 col-form-label"">Nama Barang<span class=""red"">*</span></label>
                                <div class=""col-md-10"">
                                    ");
            EndContext();
            BeginContext(2242, 92, false);
#line 48 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MBarang\EditBrg.cshtml"
                               Write(Html.TextBoxFor(model => model.Kd_Satuan, new { @class = "form-control", id = "Kd_Satuan" }));

#line default
#line hidden
            EndContext();
            BeginContext(2334, 428, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-6 form-c-inline"">
                            <div class=""row"">
                                <label class=""col-md-2 col-form-label"">Keterangan<span class=""red""></span></label>
                                <div class=""col-md-10"">
                                    ");
            EndContext();
            BeginContext(2763, 144, false);
#line 56 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MBarang\EditBrg.cshtml"
                               Write(Html.TextAreaFor(model => model.Keterangan, new { @class = "form-control", id = "Keterangan", Value = "", @rows = 4, @style = "resize: none;" }));

#line default
#line hidden
            EndContext();
            BeginContext(2907, 935, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""col-xs-12 col-sm-3 col-lg-2 page-right-content"">
                    <div class=""row row-navandbuttons"">
                        <div class=""col-xs-12"">
                            <div id=""right-content-buttons"" class=""row right-buttons"">
                                <div class=""col-xs-12"">
                                    <div class=""row "">

                                        <div class=""col-xs-6 col-sm-12 row-right-button"">
                                            <button id=""back"" class=""btn btn-danger right-button"" onclick=""showlist(); return false;""><i class=""glyphicon glyphicon-arrow-left paddingRight10"" aria-hidden=""true""></i><span>Kembali</span></button>

                                        </div>
");
            EndContext();
            BeginContext(4236, 976, true);
            WriteLiteral(@"                                        <div class=""col-xs-6 col-sm-12 row-right-button"">
                                            <button id=""update"" class=""btn btn-primary right-button"" onclick=""onUpdateClicked(); return false;"" style=""display:none;""><i class=""glyphicon glyphicon-floppy-disk paddingRight10"" aria-hidden=""true""></i><span>Simpan</span></button>

                                        </div>

                                    </div>
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
            BeginContext(5212, 77, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "f425e28aeb95f0e134aaaad902e40d616802e74c10500", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5289, 204, true);
            WriteLiteral("\r\n                                </span>\r\n                            </button>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 100 "D:\project\liquiderpspt\liquiderpspt\IFA.Web\Views\MBarang\EditBrg.cshtml"
    }

#line default
#line hidden
            BeginContext(5500, 8, true);
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IFA.Domain.Models.SIF_Barang> Html { get; private set; }
    }
}
#pragma warning restore 1591