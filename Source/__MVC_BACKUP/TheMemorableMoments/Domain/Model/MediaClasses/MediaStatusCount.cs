namespace TheMemorableMoments.Domain.Model.MediaClasses
{
   public class StatusCount<T>
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
       public int Count { get; set; }

       /// <summary>
       /// Gets or sets the media status.
       /// </summary>
       /// <value>The media status.</value>
       public T Status { get; set; }
    }
}
