using OrchardCore.ResourceManagement;

namespace FlyingRat.Braksn
{
    class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(IResourceManifestBuilder builder)
        {
            var root = "~/FlyingRat.Braksn";
            var manifest = builder.Add();
            manifest.DefineScript("Braksn-bootstrap")
                .SetUrl($"{root}/js/bootstrap.min.js")
                .SetVersion("4.0.0");

            #region js
            manifest.DefineScript("Braksn-animsition")
                .SetUrl($"{root}/js/animsition.min.js")
                .SetVersion("4.0.2");

            manifest.DefineScript("Braksn-custom_jquery")
                .SetUrl($"{root}/js/custom_jquery.js")
                .SetVersion("1.0.0");

            manifest.DefineScript("Braksn-jquery")
                .SetUrl($"{root}/js/jquery-3.2.1.min.js")
                .SetVersion("3.2.1");

            manifest.DefineScript("Braksn-popper")
                .SetUrl($"{root}/js/popper.js")
                .SetVersion("1.12.5");

            #endregion

            #region css
            manifest.DefineStyle("Braksn-stylesheet")
                .SetUrl($"{root}/css/stylesheet.css")
                .SetVersion("1.0.0");

            manifest.DefineStyle("Braksn-util")
                .SetUrl($"{root}/css/util.min.css")
                .SetVersion("1.0.0");

            manifest.DefineStyle("Braksn-animate")
                .SetUrl($"{root}/css/animate.css")
                .SetVersion("1.0.0");

            manifest.DefineStyle("Braksn-animstion")
                .SetUrl($"{root}/css/animsition.min.css")
                .SetVersion("1.0.0");

            manifest.DefineStyle("Braksn-bootstrap")
                .SetUrl($"{root}/css/bootstrap.min.css")
                .SetVersion("4.0.0");

            manifest.DefineStyle("Braksn-hamburgers")
                .SetUrl($"{root}/css/hamburgers.min.css")
                .SetVersion("1.0.0");
            #endregion

            #region font
            manifest.DefineStyle("Braksn-font-awesome")
                .SetUrl($"{root}/fonts/fontawesome-5.0.8/css/fontawesome-all.min.css")
                .SetVersion("5.0.8");

            manifest.DefineStyle("Braksn-font-iconic")
                .SetUrl($"{root}/fonts/iconic/css/material-design-iconic-font.min.css")
                .SetVersion("1.0.0");

            #endregion

            #region bundle assts
            manifest.DefineScript("website-bundle")
                .SetUrl($"{root}/bundle/website.min.js")
                .SetVersion("1.0");

            manifest.DefineScript("braksn-bundle")
                .SetUrl($"{root}/bundle/bundle.min.js")
                .SetVersion("1.0");

            manifest.DefineStyle("braksn-bundle")
                .SetUrl($"{root}/bundle/bundle.min.css")
                .SetVersion("1.0");

            #region page

            manifest.DefineScript("braksn-atlas")
                .SetUrl($"{root}/bundle/atlas.min.js")
                .SetVersion("1.0");

            #endregion

            #endregion
        }
    }
}
