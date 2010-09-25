using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web;

namespace TheMemorableMoments.UI.Models.Views.Photos
{
    public class EditPhotoView : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditPhotoView"/> class.
        /// </summary>
        public EditPhotoView()
        {
            Month = EditPhotoHelper.GetMonths();
            Day = EditPhotoHelper.GetDays();
        }
        
        /// <summary>
        /// Gets or sets the media keys.
        /// </summary>
        /// <value>The media keys.</value>
        public string MediaKeys { get; set; }
        
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>The month.</value>
        public IEnumerable<SelectListItem> Month { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        public IEnumerable<SelectListItem> Day { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

        /// <summary>
        /// Gets the list items.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetListItems()
        {
            List<SelectListItem> items = PhotoHtmlHelper.PrivacyList();
            return items;
        }
    }
}