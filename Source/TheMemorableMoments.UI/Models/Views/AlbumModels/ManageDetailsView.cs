using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class ManageDetailsView : BaseModel
    {
        /// <summary>
        /// Gets or sets the album crumbs.
        /// </summary>
        /// <value>The album crumbs.</value>
        public string AlbumCrumbs { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

        /// <summary>
        /// Gets or sets the cover media.
        /// </summary>
        /// <value>The cover media.</value>
        public Media CoverMedia { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public Album Album { get; set; }

        /// <summary>
        /// Renders the cover media.
        /// </summary>
        /// <returns></returns>
        public string RenderCoverMedia()
        {
            string html = string.Empty;

            if(CoverMedia != null)
            {

                MediaFile websize = CoverMedia.GetImageByPhotoType(PhotoType.Websize);
                html = string.Format("<label class=\"instructions\" style=\"margin-bottom:20px;\" >cover image</label><img  class=\"covermedia topten\"  src=\"{0}\" alt=\"{1}\" />", UrlService.CreateImageUrl(websize.FilePath), CoverMedia.Title );
            }

            return html;
        }
    }
}