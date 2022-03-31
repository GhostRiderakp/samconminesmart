using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading;
using WebEye.Controls.WinForms.StreamPlayerControl;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace minesmart.Helper
{
    public class ImageCapture
    {
        public static System.IO.StreamWriter objWriter;
         

        /// <summary>
        /// Captures Image from IP Camera and returns image in Base64 string
        /// </summary>
        /// <param name="player"></param>
        /// <returns>Image encoded in Base64</returns>
        public static String CaptureImageFromIpCamera(StreamPlayerControl player)
        {
            try
            {
                Bitmap image = player.GetCurrentFrame(); //Capturing current frame
                Bitmap imgNew = UPscaleImage(image);
                if (!IsImageResolutionCorrect(imgNew))
                {
                    throw new Exception("Error: Camera image resolution is less than 1280x720 pixels.");
                }
                using (Bitmap newbmp = image.Clone(new Rectangle(5, 5, imgNew.Width, imgNew.Height), System.Drawing.Imaging.PixelFormat.Format16bppRgb555))
                {
                    var newImage = MakeGrayscale(newbmp); // Converting bitmap into grayscale.
                    // now checking image size if size if <100 then again creating high quality grayscal image bitmap
                    using (var mem = new MemoryStream())
                    {
                        long size;
                        int count = 0;
                        do
                        {
                            newImage.Save(mem, ImageFormat.Jpeg);
                            size = mem.Length / 1000;
                            if (size >= 500)
                            {
                                newImage = ReduceBrightness(newImage);
                            }
                            else if (size < 100)
                            {
                                newImage = IncreaseBrightness(newImage);
                            }
                            count++;
                        } while ((size < 100 || size > 500) && (count < 5));
                        if (size < 100 || size > 500)
                            throw new Exception("Size of captured image is not with in 100 to 500 KB, the size of image is " + size + ".");
                    }
                    return GetBase64(newImage); //returning grayscale image.
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Custom Error  in try catch block CaptureImageFromIpCamera function " + ex.Message);

            }
        }

        /// <summary>
        /// Checks the resolution of the image
        /// </summary>
        /// <param name="image"></param>
        /// <returns>returns true if resolution is minimum 1280x720p, false otherwie</returns>
        public static bool IsImageResolutionCorrect(Bitmap image)
        {
            return (image.Width >= 1280 && image.Height >= 720);

        }

        private Bitmap Resize(Image img, int iWidth, int iHeight)
        {
            Bitmap bmp = new Bitmap(iWidth, iHeight);
            Graphics graphic = Graphics.FromImage(((Image)(bmp)));
            graphic.DrawImage(img, 0, 0, iWidth, iHeight);
            return bmp;
        }

        /// <summary>
        /// convert jpg from image Path to Base64 string
        /// </summary>
        /// <param name="imgPath">Image location</param>
        /// <returns></returns>
        protected static string GetBase64StringForImage(string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
        /// <summary>
        /// Converts Bitmap image to Base64 string.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string GetBase64(Bitmap image)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var bitmap = new Bitmap(image))
                    {
                        bitmap.Save(ms, ImageFormat.Jpeg);
                        var base64String = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                        return base64String;
                    }
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Makes any color Bitmap to Grayscale image
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Bitmap MakeGrayscale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(
               new float[][]
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });
            //create some image attributes
            System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();
            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);
            //draw the original image on the new image
            //using the grayscale color matrix


            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        public static Bitmap IncreaseBrightness(Bitmap original)
        {
            //create a blank bitmap the same size as original
            var newBitmap = new Bitmap(original);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            var colorMatrix = new ColorMatrix(
               new float[][]
              {

                 new float[]  {1,1,1, 0, 0},
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });
            //create some image attributes
            var attributes = new ImageAttributes();
            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            original.Dispose();
            return newBitmap;
        }
        private static Bitmap ReduceBrightness(Bitmap original)
        {
            //create a blank bitmap the same size as original
            var newBitmap = new Bitmap(original);
            //get a graphics object from the new image
            var g = Graphics.FromImage(newBitmap);
            //create the grayscale ColorMatrix
            var colorMatrix = new ColorMatrix(
               new float[][]
              {
                  new float[] {.05f, .05f, .05f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });
            //create some image attributes
            var attributes = new ImageAttributes();
            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            original.Dispose();
            return newBitmap;
        }

        public static Bitmap UPscaleImage(Bitmap imageData)
        {
            MemoryStream ms = new MemoryStream();
            imageData.Save(ms, ImageFormat.Jpeg);
            MemoryStream resizedPhotoStream = new MemoryStream();
            float width = 1800;
            float height = 720;
            var brush = new SolidBrush(Color.White);
            var rawImage = Image.FromStream(ms);
            float scale = Math.Min(width / rawImage.Width, height / rawImage.Height);
            var scaledBitmap = new Bitmap(1280, 720);
            Graphics graph = Graphics.FromImage(scaledBitmap);
            graph.InterpolationMode = InterpolationMode.High;
            graph.CompositingQuality = CompositingQuality.HighQuality;
            graph.SmoothingMode = SmoothingMode.AntiAlias;
            graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
            graph.DrawImage(rawImage, new Rectangle(0, 0, 1280, 720));
            return scaledBitmap;
        }
    }
}
