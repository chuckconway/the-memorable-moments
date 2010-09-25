namespace TheMemorableMoments.Domain.Model
{
   public class UserSettings
    {

        /// <summary>
        /// Gets or sets a value indicating whether [enable emails].
        /// </summary>
        /// <value><c>true</c> if [enable emails]; otherwise, <c>false</c>.</value>
        public bool EnableReceivingOfEmails { get; set; }

        /// <summary>
        /// Gets or sets the height of the web view.
        /// </summary>
        /// <value>The height of the web view max.</value>
       public short WebViewMaxHeight { get; set; }

       /// <summary>
       /// Gets or sets the width of the web view.
       /// </summary>
       /// <value>The width of the web view max.</value>
       public short WebViewMaxWidth { get; set; }
    }
}
