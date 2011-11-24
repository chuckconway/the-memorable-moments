using System;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.UI.Models.Views.Photos
{
    public class MediaJson
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaJson"/> class.
        /// </summary>
        /// <param name="media">The media.</param>
        public MediaJson(Media media)
        {
            MediaId = media.MediaId;
            CommentCount = media.CommentCount;
            CreateDate = media.CreateDate;
            Day = media.Day;
            Description = media.Description;
            Month = media.Month;
            Status = media.Status;
            Title = media.Title;
            UserId = media.Owner.UserId;
            Username = media.Owner.Username;
            Year = media.Year;
        }

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        /// <value>The media id.</value>
        public int MediaId { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the times viewed.
        /// </summary>
        /// <value>The times viewed.</value>
        public int TimesViewed { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int? Year { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>The month.</value>
        public int? Month { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        public int? Day { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public MediaStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the comment count.
        /// </summary>
        /// <value>The comment count.</value>
        public int CommentCount { get; set; }

        public string GenerateLinks(TagCollection tagCollection)
        {
            const string linkFormat = @"<a href="""" title=""""> </a>";
            return linkFormat;
        }

        ///// <summary>
        ///// Gets or sets the tags.
        ///// </summary>
        ///// <value>The tags.</value>
        //public TagCollection Tags { get; set; }
    }
}