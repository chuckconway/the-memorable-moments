using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheMemorableMoments.Domain.Model.Tags
{
    public class TagCollection : IEnumerable
    {
        private readonly List<Tag> _tags;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="tags">The tags.</param>
        public TagCollection(string tags)
        {
            List<Tag> tagCollection = new List<Tag>();

            if (!string.IsNullOrEmpty(tags))
            {
                string[] tagArray = tags.Trim().Split(',', ' ');
                tagCollection.AddRange(tagArray.Select(t => new Tag(t.ToLower())).Where(tag => !string.IsNullOrEmpty(tag.TagText)));
            }

            _tags = tagCollection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagCollection"/> class.
        /// </summary>
        public TagCollection()
        {
            _tags = new List<Tag>();
        }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public List<Tag> Tags
        {
            get
            {
                return _tags;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < _tags.Count; index++)
            {
                builder.Append(_tags[index].TagText);
                
                if(index != _tags.Count -1)
                {
                    builder.Append(" ");
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
           return  _tags.GetEnumerator();
        }
    }
}