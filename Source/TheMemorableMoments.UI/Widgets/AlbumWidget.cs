using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Widgets
{
    public static class AlbumWidget
    {
        private static readonly IUserUrlService _urlService = UserUrlService.GetInstance();

        /// <summary>
        /// Renders the specified albums.
        /// </summary>
        /// <param name="albums">The albums.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public static string Render(IList<Album> albums, string username)
        {
            
            StringBuilder builder = new StringBuilder();

            if (albums == null) { throw new System.ArgumentNullException("albums", "Parameter 'albums' is null, value expected"); }

            if (albums.Count > 0)
            {
                string albumUrl = _urlService.CreateUrl(username, "albums");
                builder.AppendLine(string.Format("<h3><a href=\"{0}\" >albums</a></h3>", albumUrl));
                string val =  BuildAlbum(albums);

                builder.AppendLine("<table class=\"albumwidget\" ><tbody>");
                builder.AppendLine(val);
                builder.AppendLine("</tbody></table>");
                builder.AppendLine(albums.Count > 4
                                       ? string.Format("<p class=\"clearboth marginbottom40\" ><a href=\"{0}/#/\" title=\"view more albums\">more...</a></p>", albumUrl)
                                       : "<br class=\"clearboth\" />");
            }
            
            return builder.ToString();
        }

        /// <summary>
        /// Builds the album.
        /// </summary>
        /// <param name="albums">The albums.</param>
        /// <returns></returns>
        private static string BuildAlbum(IList<Album> albums)
        {
            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < albums.Count; index++)
            {
                if (index < 4)
                {
                    builder.AppendLine(GetAlbumImageThumbnailForUserHomepage(albums[index]));
                }
                else
                {
                    break;
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns></returns>
        public static string GetAlbumImageThumbnailForUserHomepage(Album album)
        {
            string val = string.Empty;

            if (album.Media != null)
            {
                MediaFile thumbnail = album.Media[0].GetImageByPhotoType(PhotoType.Thumbnail);
                const string linkFormat = @"<tr>
                                                <td><span class=""imagecontainer"" ><a class=""showimage"" href=""{0}"" title=""{1}""><img src=""{2}"" alt=""{3}"" /></a></span></td>
                                                <td>
                                                    <ul class=""albummetadata"" >
                                                        <li><label class=""albumtitle"" ><span>{4}</span></label></li>
                                                        <li><label>photos </label><span>{5}</span></li>
                                                        <li><label>sub albums </label><span>{6}</span></li>  
                                                    </ul>              
                                                </td>
                                            </tr>";
                val = string.Format(linkFormat,
                    _urlService.UserUrl(album.Media[0].Owner.Username, string.Format("albums/#/show/{0}", album.AlbumId)),
                    album.Name,
                    _urlService.UserUrl(album.Media[0].Owner.Username, "services/grayscale/" + thumbnail.MediaId),
                    album.Media[0].Title,
                    album.Name, album.PhotoCount, album.ChildAlbumCount);
            }
            return val;
        }
    }
}
