namespace TheMemorableMoments.UI.Models.Views
{
    public class EditTagModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        /// <value>The name of the tag.</value>
        public string TagText { get; set; }

        /// <summary>
        /// Gets or sets the orginal tag text.
        /// </summary>
        /// <value>The orginal tag text.</value>
        public string OrginalTagText { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string TagDescription { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }
    }
}