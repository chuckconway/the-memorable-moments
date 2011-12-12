namespace Momntz.Commands.Momentos
{
    public class CreateCommentCommand : ICommand<int>
    {
        public int MomentoId { get; private set; }

        public string Author { get; private set; }
        public string AuthorEmail { get; private set; }
        public string AuthorUrl { get; private set; }
        public string Body { get; private set; }
        public string UserAgent { get; private set; }
        public string UserIp { get; private set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommentCommand"/> class.
        /// </summary>
        /// <param name="momentoId">The momento id.</param>
        /// <param name="author">The author.</param>
        /// <param name="authorEmail">The author email.</param>
        /// <param name="authorUrl">The author URL.</param>
        /// <param name="body">The body.</param>
        /// <param name="userAgent">The user agent.</param>
        /// <param name="userIp">The user ip.</param>
        public CreateCommentCommand(int momentoId, string author, string authorEmail, string authorUrl, string body, string userAgent, string userIp)
        {
            MomentoId = momentoId;
            Author = author;
            AuthorEmail = authorEmail;
            AuthorUrl = authorUrl;
            Body = body;
            UserAgent = userAgent;
            UserIp = userIp;
        }
    }
}
