using System.Collections.ObjectModel;

namespace Momntz.Domain.Model
{
    public class IdentityCollection<T> : Collection<T> where T : IPrimaryKey<int>
    {
        public int Seed { get; private set; }

        /// <summary>
        /// Adds an object to the end of the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
        /// </summary>
        /// <param name="item">The object to be added to the end of the <see cref="T:System.Collections.ObjectModel.Collection`1"/>. The value can be null for reference types.</param>
        public new void Add(T item)
        {
            Seed++;

            item.Id = Seed;
            base.Add(item);
        }
    }
}
