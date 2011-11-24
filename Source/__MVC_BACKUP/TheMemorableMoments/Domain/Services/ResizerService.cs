using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Chucksoft.Core.Drawing;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public class ResizerService : IResizerService
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly Configuration _configuration;


        /// <summary>
        /// Initializes a new instance of the <see cref="ResizerService"/> class.
        /// </summary>
        /// <param name="configurationRepository">The configuration repository.</param>
        public ResizerService(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
            _configuration = _configurationRepository.RetrieveByPrimaryKey(new Guid("1e3bc0f8-ddad-4433-a370-81c8830c7ae0"));
        }

        /// <summary>
        /// Finds the image perspective.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        private static Size ImageSize(Image photo, Size size)
        {
            Size imageSize = new Size();

            //determine if it's a portrait or landscape
            if (photo.Height > photo.Width)
            {
                //portrait
                decimal ratio = (photo.Width / (decimal)photo.Height);
                imageSize.Height = (photo.Height < size.Height ? photo.Height : size.Height);
                imageSize.Width = (int)(ratio * imageSize.Height);
            }
            else
            {
                //landscape
                decimal ratio = (photo.Height / (decimal)photo.Width);
                imageSize.Width = (photo.Width < size.Width ? photo.Width : size.Width);
                imageSize.Height = (int)(ratio * imageSize.Width);
            }

            return imageSize;
        }

        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public byte[] ResizeImage(byte[] image, PhotoType type)
        {
            byte[] resizeImage = image;

            if(type != PhotoType.Original)
            {
                Bitmap bitmap = image.ConvertByteArrayToBitmap();
                Size size = GetSize(type);
                
                Size imageSize = ImageSize(bitmap, size);
                resizeImage = image.Resize(imageSize.Width, imageSize.Height, ImageFormat.Jpeg);
            }

            return resizeImage;
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private Size GetSize(PhotoType type)
        {
            IEnumerable<Size> sizes = GetAllSizes();
            Size size = new Size();

            foreach (Size list in sizes.Where(list => list.PhotoType == type))
            {
                size = list;
                break;
            }

            return size;
        }

        /// <summary>
        /// Gets all sizes.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Size> GetAllSizes()
        {
            return new List<Size>
                      {
                          new Size{Height = _configuration.ThumbNailHeight, Width = _configuration.ThumbNailWidth, PhotoType = PhotoType.Thumbnail},
                          new Size{Height = _configuration.WebHeight, Width = _configuration.WebWidth, PhotoType = PhotoType.Websize}
                      };
        }

        private class Size
        {
            /// <summary>
            /// Gets or sets the width.
            /// </summary>
            /// <value>The width.</value>
            public int Width { get; set; }

            /// <summary>
            /// Gets or sets the height.
            /// </summary>
            /// <value>The height.</value>
            public int Height { get; set; }

            /// <summary>
            /// Gets or sets the type of the photo.
            /// </summary>
            /// <value>The type of the photo.</value>
            public PhotoType PhotoType { get; set; }
        }
    }
}
