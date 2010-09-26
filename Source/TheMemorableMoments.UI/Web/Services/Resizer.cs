namespace TheMemorableMoments.UI.Web.Services
{
    public class Resizer
    {
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; set; }

        public void Resize(int currentWidth, int currentHeight, int maxWidth, int maxHeight)
        {
            if (currentWidth > maxWidth || currentHeight > maxHeight)
            {
                //Landscape
                if (currentWidth > currentHeight)
                {
                    decimal ratio = (decimal)maxWidth / currentWidth;
                    Width = (int)(currentWidth * ratio);
                    Height = (int)(currentHeight * ratio);

                }
                //portrait
                else
                {
                    decimal ratio = (decimal)maxHeight / currentHeight;
                    Height = (int)(currentHeight * ratio);
                    Width = (int)(currentWidth * ratio);
                }

            }
            else
            {
                Width = currentWidth;
                Height = currentHeight;
            }
        }
    }
}