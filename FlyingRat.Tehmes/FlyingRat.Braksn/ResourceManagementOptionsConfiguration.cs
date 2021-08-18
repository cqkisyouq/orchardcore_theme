using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace FlyingRat.Braksn
{
    class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;
        static ResourceManagementOptionsConfiguration()
        {
            var root = "~/FlyingRat.Braksn";
            _manifest = new ResourceManifest();
            _manifest.DefineScript("Braksn-bootstrap")
                .SetUrl($"{root}/js/bootstrap.min.js")
                .SetVersion("4.0.0");

            #region js
            _manifest.DefineScript("Braksn-animsition")
                .SetUrl($"{root}/js/animsition.min.js")
                .SetVersion("4.0.2");

            _manifest.DefineScript("Braksn-custom_jquery")
                .SetUrl($"{root}/js/custom_jquery.js")
                .SetVersion("1.0.0");

            _manifest.DefineScript("Braksn-jquery")
                .SetUrl($"{root}/js/jquery-3.2.1.min.js")
                .SetVersion("3.2.1");

            _manifest.DefineScript("Braksn-popper")
                .SetUrl($"{root}/js/popper.js")
                .SetVersion("1.12.5");

            #endregion

            #region css
            _manifest.DefineStyle("Braksn-stylesheet")
                .SetUrl($"{root}/css/stylesheet.css")
                .SetVersion("1.0.0");

            _manifest.DefineStyle("Braksn-util")
                .SetUrl($"{root}/css/util.min.css")
                .SetVersion("1.0.0");

            _manifest.DefineStyle("Braksn-animate")
                .SetUrl($"{root}/css/animate.css")
                .SetVersion("1.0.0");

            _manifest.DefineStyle("Braksn-animstion")
                .SetUrl($"{root}/css/animsition.min.css")
                .SetVersion("1.0.0");

            _manifest.DefineStyle("Braksn-bootstrap")
                .SetUrl($"{root}/css/bootstrap.min.css")
                .SetVersion("4.0.0");

            _manifest.DefineStyle("Braksn-hamburgers")
                .SetUrl($"{root}/css/hamburgers.min.css")
                .SetVersion("1.0.0");
            #endregion

            #region font
            _manifest.DefineStyle("Braksn-font-awesome")
                .SetUrl($"{root}/fonts/fontawesome-5.0.8/css/fontawesome-all.min.css")
                .SetVersion("5.0.8");

            _manifest.DefineStyle("Braksn-font-iconic")
                .SetUrl($"{root}/fonts/iconic/css/material-design-iconic-font.min.css")
                .SetVersion("1.0.0");

            #endregion

            #region bundle assts
            _manifest.DefineScript("website-bundle")
                .SetUrl($"{root}/bundle/website.min.js")
                .SetVersion("1.0");

            _manifest.DefineScript("braksn-validate")
                .SetUrl($"{root}/bundle/validate.min.js")
                .SetVersion("1.0");

            _manifest.DefineScript("braksn-bundle")
                .SetUrl($"{root}/bundle/bundle.min.js")
                .SetVersion("1.0");

            _manifest.DefineStyle("braksn-bundle")
                .SetUrl($"{root}/bundle/bundle.min.css")
                .SetVersion("1.0");

            #region page

            _manifest.DefineScript("braksn-atlas")
                .SetUrl($"{root}/bundle/atlas.min.js")
                .SetVersion("1.0");

            #endregion

            #endregion
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
