#pragma checksum "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Carts/OrderConfirmation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "67a0693ff190228e4daa845f09d8fb85c06ccdb5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Authenticated_Views_Carts_OrderConfirmation), @"mvc.1.0.view", @"/Areas/Authenticated/Views/Carts/OrderConfirmation.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67a0693ff190228e4daa845f09d8fb85c06ccdb5", @"/Areas/Authenticated/Views/Carts/OrderConfirmation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f415ee49bb0220cbbb4109f666031a661c7df190", @"/Areas/Authenticated/Views/_ViewImports.cshtml")]
    public class Areas_Authenticated_Views_Carts_OrderConfirmation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""container row"">
    <div class=""col-12 text-center"">
        <h1 class=""text-primary text-center"">Order Submitted Successfully</h1><br />
        <img src=""https://bsscommerce.com/blog/magento-2-order-success-page/"" width=""100%"" />
    </div>
    <div class=""col-12 text-center"" style=""color:green"">
        <br />
            Thank you for your order! <br />
            We have received your order and we will send a follow up email shortly!
        <br />
        Your Order Number is : ");
#nullable restore
#line 12 "/home/phong-zorin/work/dotnet-projects/BookSellingAuthorization/Areas/Authenticated/Views/Carts/OrderConfirmation.cshtml"
                          Write(Model);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int> Html { get; private set; }
    }
}
#pragma warning restore 1591