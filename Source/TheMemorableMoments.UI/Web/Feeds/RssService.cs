using System;
using System.Collections.Generic;
using System.Threading;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Web.Feeds
{
    public class RssService
    {
        /// <summary>
        /// Renders the specified media.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="user">The user.</param>
        /// <param name="host">The host.</param>
        /// <returns></returns>
        public string Render(List<Media> media, User user, string host)
        {
            Rss rss = new Rss
                          {
                              Title = user.DisplayName + "'s Memorable Moments",
                              Link = host + user.Username,
                              PublishDate = DateTime.UtcNow,
                              Language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName,
                              Description = string.Empty
                          };

            PopulateRssEntry(host, media, rss, user);

            return rss.ToString();
        }

        /// <summary>
        /// Populates the atom entry.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="media">The media.</param>
        /// <param name="rss">The RSS.</param>
        /// <param name="user">The user.</param>
        private static void PopulateRssEntry(string host, IEnumerable<Media> media, Rss rss, User user)
        {
            rss.Items = new List<RssItem>();

            foreach (Media list in media)
            {
                RssItem entry = new RssItem();
                //MediaFile file = RetrieveByMediaType(list, PhotoType.Websize);
                //rss.Id = Guid.NewGuid().ToString();
                entry.PublishDate = list.CreateDate;
                entry.Link = host + user.Username + "/photos/show/" + list.MediaId;
                entry.Description = list.Description; //string.Format("{0}<br /> <img src=\"{1}Images/{2}\" />", list.Description, host, user.Username + "/" + file.FilePath.Replace("\\", "/"));
                entry.Title = (string.IsNullOrEmpty(list.Title) ? "Untitled" : list.Title);
                rss.Items.Add(entry);
            }
        }
    }
}
