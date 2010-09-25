using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class ManageViewBase : BaseModel
    {
        private readonly ITagService _tagService = DependencyInjection.Resolve<ITagService>();

        /// <summary>
        /// Renders the details.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        public string RenderDetails(Media m)
        {
            return string.Format("<span class=\"title\" >{0}</span>", m.Title) +
                   string.Format("<p>{0}</p>", m.Description) + (string.IsNullOrEmpty(m.Tags) ? string.Empty : "<p style=\"margin-bottom:0px;\" >tags:" + _tagService.HyperlinkTheTags(m.Tags, Authorization.Owner.Username) + " </p>");
        }
    }
}