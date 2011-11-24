using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web;

namespace TheMemorableMoments.UI.Models.Views.Upload
{
    public class UserUploadView : BaseModel
    {
        /// <summary>
        /// Gets or sets the batch id.
        /// </summary>
        /// <value>The batch id.</value>
        public Guid BatchId { get; set; }

        /// <summary>
        /// Gets the list items.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public List<SelectListItem> GetListItems(MediaStatus status)
        {
            List<SelectListItem> items = PhotoHtmlHelper.PrivacyList();
            items.ForEach(o => o.Selected = o.Value.Equals(status.ToString(), StringComparison.InvariantCultureIgnoreCase));
            return items;
        }

        /// <summary>
        /// Gets or sets the story.
        /// </summary>
        /// <value>The story.</value>
        public string Story { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int? Year { get; set; }

        /// <summary>
        /// Gets the month.
        /// </summary>
        /// <value>The month.</value>
        public IEnumerable<SelectListItem> Month
        {
            get
            {
                return EditPhotoHelper.GetMonths();
            }
        }

        /// <summary>
        /// Gets the day.
        /// </summary>
        /// <value>The day.</value>
        public IEnumerable<SelectListItem> Day
        {
            get
            {
                return EditPhotoHelper.GetDays();
            }
        }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

        /// <summary>
        /// Gets or sets the photo title.
        /// </summary>
        /// <value>The photo title.</value>
        public string PhotoTitle { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public MediaStatus Status { get; set; }

    }
}


