#pragma checksum "D:\Subin\Tech\Asp.Net Core\Security\Dec_2018\Social_logins\WebApp_SocialLogins\WebApp_SocialLogins\Views\Home\UserInformation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3bc62907ed556f66a880922dca77c7814fc7cc40"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_UserInformation), @"mvc.1.0.view", @"/Views/Home/UserInformation.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/UserInformation.cshtml", typeof(AspNetCore.Views_Home_UserInformation))]
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
#line 1 "D:\Subin\Tech\Asp.Net Core\Security\Dec_2018\Social_logins\WebApp_SocialLogins\WebApp_SocialLogins\Views\_ViewImports.cshtml"
using WebApp_SocialLogins;

#line default
#line hidden
#line 2 "D:\Subin\Tech\Asp.Net Core\Security\Dec_2018\Social_logins\WebApp_SocialLogins\WebApp_SocialLogins\Views\_ViewImports.cshtml"
using WebApp_SocialLogins.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bc62907ed556f66a880922dca77c7814fc7cc40", @"/Views/Home/UserInformation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e4554f6b6f5298aaf581e76fcd743dddb59909f", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_UserInformation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 50, true);
            WriteLiteral("\r\n<div class=\"starter-template\">\r\n    <h1>Welcome ");
            EndContext();
            BeginContext(51, 18, false);
#line 3 "D:\Subin\Tech\Asp.Net Core\Security\Dec_2018\Social_logins\WebApp_SocialLogins\WebApp_SocialLogins\Views\Home\UserInformation.cshtml"
           Write(User.Identity.Name);

#line default
#line hidden
            EndContext();
            BeginContext(69, 64, true);
            WriteLiteral("</h1>\r\n    <p class=\"lead\">Here are your claims:</p>\r\n    <ul>\r\n");
            EndContext();
#line 6 "D:\Subin\Tech\Asp.Net Core\Security\Dec_2018\Social_logins\WebApp_SocialLogins\WebApp_SocialLogins\Views\Home\UserInformation.cshtml"
         foreach (var claim in User.Claims)
        {

#line default
#line hidden
            BeginContext(189, 38, true);
            WriteLiteral("            <li>\r\n                <em>");
            EndContext();
            BeginContext(228, 10, false);
#line 9 "D:\Subin\Tech\Asp.Net Core\Security\Dec_2018\Social_logins\WebApp_SocialLogins\WebApp_SocialLogins\Views\Home\UserInformation.cshtml"
               Write(claim.Type);

#line default
#line hidden
            EndContext();
            BeginContext(238, 7, true);
            WriteLiteral("</em>: ");
            EndContext();
            BeginContext(246, 11, false);
#line 9 "D:\Subin\Tech\Asp.Net Core\Security\Dec_2018\Social_logins\WebApp_SocialLogins\WebApp_SocialLogins\Views\Home\UserInformation.cshtml"
                                 Write(claim.Value);

#line default
#line hidden
            EndContext();
            BeginContext(257, 21, true);
            WriteLiteral("\r\n            </li>\r\n");
            EndContext();
#line 11 "D:\Subin\Tech\Asp.Net Core\Security\Dec_2018\Social_logins\WebApp_SocialLogins\WebApp_SocialLogins\Views\Home\UserInformation.cshtml"
        }

#line default
#line hidden
            BeginContext(289, 17, true);
            WriteLiteral("    </ul>\r\n</div>");
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
