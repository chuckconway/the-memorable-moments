using System.Collections.Generic;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views
{
    public interface IBaseModel
    {
        /// <summary>
        /// Gets or sets the bread crumbs.
        /// </summary>
        /// <value>The bread crumbs.</value>
        List<BreadCrumb> BreadCrumbs { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        Authorization Authorization { get; set; }

        /// <summary>
        /// Gets the URL service.
        /// </summary>
        /// <value>The URL service.</value>
        IUserUrlService UrlService { get; set; }

        /// <summary>
        /// Renders the bread crumbs.
        /// </summary>
        /// <returns></returns>
        string RenderBreadCrumbs();
    }
}