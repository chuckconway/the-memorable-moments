using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Momntz.Infrastructure.Services.Spam
{
    public interface ICommentSpamService
    {
        /// <summary>
        /// Determines whether the specified comment is spam.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>
        ///   <c>true</c> if the specified comment is spam; otherwise, <c>false</c>.
        /// </returns>
        bool IsSpam(CheckCommentForSpam comment);

        /// <summary>
        /// This call is intended for the marking of false positives, things that were incorrectly marked as spam.
        /// </summary>
        /// <param name="comment">The comment.</param>
        void SubmitHam(CheckCommentForSpam comment);

        /// <summary>
        /// This call is for submitting comments that weren't marked as spam but should've been. 
        /// </summary>
        /// <param name="comment">The comment.</param>
        void SubmitSpam(CheckCommentForSpam comment);

    }
}
