using System;
using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Web.Feeds
{
    public class AtomService
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
            Atom atom = new Atom
                            {
                                Id = Guid.NewGuid().ToString(),
                                AuthorName = user.DisplayName,
                                Title = user.DisplayName + "'s Memorable Moments",
                                Link = host + user.Username,
                                UpdatedDate = DateTime.UtcNow
                            };


            PopulateAtomEntry(host, media, atom, user);
            return atom.ToString();
        }

        /// <summary>
        /// Populates the atom entry.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="media">The media.</param>
        /// <param name="atom">The atom.</param>
        /// <param name="user">The user.</param>
        private static void PopulateAtomEntry(string host, IEnumerable<Media> media, Atom atom, User user)
        {
            foreach (Media list in media)
            {
                AtomEntry entry = new AtomEntry
                                      {
                                          Id = Guid.NewGuid().ToString(),
                                          Link = host + user.Username + "/photos/show/" + list.MediaId,
                                          Summary = list.Description,
                                          Title = (string.IsNullOrEmpty(list.Title) ? "Untitled" : list.Title)
                                      };
                //MediaFile file = RetrieveByMediaType(list, PhotoType.Websize);
                atom.Entries.Add(entry);
            }
        }

        /// <summary>
        /// Retrieves the type of the by media.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="photoType">Type of the photo.</param>
        /// <returns></returns>
        private static MediaFile RetrieveByMediaType(Media media, PhotoType photoType)
        {
            MediaFile mediaFile = new MediaFile();

            foreach (MediaFile file in media.MediaFiles)
            {
                if (file.PhotoType == photoType)
                {
                    mediaFile = file;
                    break;
                }
            }

            return mediaFile;
        }
    }
}
