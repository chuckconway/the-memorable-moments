using Joel.Net;

namespace Momntz.Infrastructure.Services.Spam
{
    public class CommentSpamService : ICommentSpamService
    {
        private static readonly Akismet Service;
        private static readonly bool VerifyKey;

        /// <summary>
        /// Initializes the <see cref="CommentSpamService"/> class.
        /// </summary>
        static CommentSpamService()
        {
            Service = new Akismet("key", "http://www.momntz.com", "Momntz/2.0");
            VerifyKey = Service.VerifyKey();
        }

        /// <summary>
        /// Determines whether the specified comment is spam.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>
        ///   <c>true</c> if the specified comment is spam; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSpam(CheckCommentForSpam comment)
        {
            if (VerifyKey)
            {
                AkismetComment akismetComment = ConvertCommentToAkismetComment(comment);
                return Service.CommentCheck(akismetComment);
            }

            return false;
        }

        /// <summary>
        /// This call is intended for the marking of false positives, things that were incorrectly marked as spam.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void SubmitHam(CheckCommentForSpam comment)
        {
            if (VerifyKey)
            {
                AkismetComment akismetComment = ConvertCommentToAkismetComment(comment);
                Service.SubmitHam(akismetComment);
            }
        }

        /// <summary>
        /// This call is for submitting comments that weren't marked as spam but should've been.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void SubmitSpam(CheckCommentForSpam comment)
        {
            if (VerifyKey)
            {
                AkismetComment akismetComment = ConvertCommentToAkismetComment(comment);
                Service.SubmitSpam(akismetComment);
            }
        }

        /// <summary>
        /// Converts the comment to akismet comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        private AkismetComment ConvertCommentToAkismetComment(CheckCommentForSpam comment)
        {
            AkismetComment akismet = new AkismetComment
                                         {
                                             Blog = "http://momntz.com",
                                             CommentAuthor = comment.Author,
                                             CommentAuthorEmail = comment.AuthorEmail,
                                             CommentAuthorUrl = comment.AuthorUrl,
                                             CommentContent = comment.Body,
                                             CommentType = "comment",
                                             UserAgent = comment.UserAgent,
                                             UserIp = comment.UserIP
                                         };

            return akismet;
        }
    }
}