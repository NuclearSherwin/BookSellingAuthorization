#pragma checksum "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c6f805a29bcc4cd524e9382f62c994b85c4ac3b5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Authenticated_Views_Management_Detail), @"mvc.1.0.view", @"/Areas/Authenticated/Views/Management/Detail.cshtml")]
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
#nullable restore
#line 1 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/_ViewImports.cshtml"
using bookselling;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/_ViewImports.cshtml"
using bookselling.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6f805a29bcc4cd524e9382f62c994b85c4ac3b5", @"/Areas/Authenticated/Views/Management/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f415ee49bb0220cbbb4109f666031a661c7df190", @"/Areas/Authenticated/Views/_ViewImports.cshtml")]
    public class Areas_Authenticated_Views_Management_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<bookselling.Models.OrderDetail>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_StatusMessenger", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
  
    Layout = "_Layout";
    ViewData["Title"] = "Order Detail";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"card shadow mb-4\">\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c6f805a29bcc4cd524e9382f62c994b85c4ac3b53897", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 9 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = ViewData["Message"];

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    <div class=""card-body text-dark"" style=""background-color: #d3d4d4"">
        <div class=""table-responsive font-weight-bold"">
            <table class=""table table-bordered"" id=""dataTable"">

                <thead>
                <tr>
                    <th>Image</th>
                    <th>Product Name</th>
                    <th>Price</th>
                    <th>Quantity</th>
                    <th>Total</th>
                </tr>
                </thead>
                
                <tbody>
");
#nullable restore
#line 25 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
                  
                    double sum = 0;
                

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
#nullable restore
#line 29 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
                 foreach (var order in @Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\n                        <td class=\"text-center\">\n                            <img");
            BeginWriteAttribute("src", " src=\"", 977, "\"", 1002, 1);
#nullable restore
#line 33 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
WriteAttributeValue("", 983, order.Book.ImgPath, 983, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top rounded\" style=\"width: 50%\"");
            BeginWriteAttribute("alt", " alt=\"", 1051, "\"", 1057, 0);
            EndWriteAttribute();
            WriteLiteral("/>\n                        </td>\n                        <td>");
#nullable restore
#line 35 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
                       Write(order.Book.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>");
#nullable restore
#line 36 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
                       Write(order.Book.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>");
#nullable restore
#line 37 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
                       Write(order.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n");
#nullable restore
#line 38 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
                          
                            var total = @order.Book.Price * @order.Quantity;
                            sum += total;
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>");
#nullable restore
#line 42 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
                       Write(total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                    </tr>\n");
#nullable restore
#line 44 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Management/Detail.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\n            </table>\n        </div>\n    </div>\n</div>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<bookselling.Models.OrderDetail>> Html { get; private set; }
    }
}
#pragma warning restore 1591
