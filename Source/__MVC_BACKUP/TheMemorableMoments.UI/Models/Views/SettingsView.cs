using System;
using System.ComponentModel.DataAnnotations;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Models.Views
{
    public class SettingsView : BaseModel
    {

        /// <summary>
        /// Gets or sets a value indicating whether [enable emails].
        /// </summary>
        /// <value><c>true</c> if [enable emails]; otherwise, <c>false</c>.</value>
        public bool EnableReceivingOfEmails { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

        /// <summary>
        /// Gets or sets the width of the web view.
        /// </summary>
        /// <value>The width of the web view max.</value>
        [Required(ErrorMessage = "Max. width is required")]
        [RegularExpression(@"^(\d{1,4})$", ErrorMessage = "A number is expected")]
        public string WebViewMaxWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the web view.
        /// </summary>
        /// <value>The height of the web view max.</value>
        [Required(ErrorMessage = "Max height is required")]
        [RegularExpression(@"^(\d{1,4})$", ErrorMessage = "A number is expected")]
        public string WebViewMaxHeight { get; set; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public User GetSettings(User user)
        {
            user.Settings = new UserSettings
                                {
                                    EnableReceivingOfEmails = EnableReceivingOfEmails,
                                    WebViewMaxHeight = Convert.ToInt16(WebViewMaxHeight),
                                    WebViewMaxWidth = Convert.ToInt16(WebViewMaxWidth)
                                };
            return user;
        }

        /// <summary>
        /// Populates the settings.
        /// </summary>
        /// <param name="user">The user.</param>
        public void PopulateSettings(User user)
        {
            EnableReceivingOfEmails = user.Settings.EnableReceivingOfEmails;
            WebViewMaxHeight = Convert.ToString(user.Settings.WebViewMaxHeight);
            WebViewMaxWidth = Convert.ToString(user.Settings.WebViewMaxWidth);
        }


    }
}
