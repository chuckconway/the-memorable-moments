using System.Collections.Generic;
using System.Web.Mvc;
using SquishIt.Framework;
using SquishIt.Framework.Css;
using SquishIt.Framework.JavaScript;
using SquishIt.Framework.JavaScript.Minifiers;

namespace TheMemorableMoments.UI.Web.Helpers
{
    public static class Compressor
    {
        /// <summary>
        /// Minify the javascript into one file
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="filenames">The filenames.</param>
        /// <param name="outputPathandFilename">The output pathand filename.</param>
        /// <returns></returns>
        public static string Js(this HtmlHelper helper, IEnumerable<string> filenames, string outputPathandFilename)
        {
            IJavaScriptBundle bundle = Bundle.JavaScript();
            IJavaScriptBundleBuilder builder = null;
            foreach (string filename in filenames)
            {
                if(builder == null)
                {
                    builder = bundle.Add(filename).WithMinifier(JavaScriptMinifiers.Yui);
                }
                else
                {
                    builder.Add(filename).WithMinifier(JavaScriptMinifiers.Yui);
                }
            }

            return (builder != null ? builder.Render(outputPathandFilename) : string.Empty);
        }

        /// <summary>
        /// Minify the css into one file
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="filenames">The filenames.</param>
        /// <param name="outputPathandFilename">The output pathand filename.</param>
        /// <returns></returns>
        public static string Css(this HtmlHelper helper, IEnumerable<string> filenames, string outputPathandFilename)
        {
            ICssBundle bundle = Bundle.Css();
            ICssBundleBuilder builder = null;

            foreach (string filename in filenames)
            {
                if (builder == null)
                {
                    builder = bundle.Add(filename);
                }
                else
                {
                    builder.Add(filename);
                }
            }

            return (builder != null ? builder.Render(outputPathandFilename) : string.Empty);
        }

    }
}