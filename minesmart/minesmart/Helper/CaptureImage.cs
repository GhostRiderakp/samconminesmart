using System;
using System.Text;
using System.IO.Ports;
using System.Management;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text.RegularExpressions;
using OpenCvSharp;
using OpenCvSharp.Extensions;



using WebEye.Controls.WinForms.StreamPlayerControl;
using System.Drawing.Drawing2D;

namespace minesmart.Helper
{
    public static class CaptureImage
    {
        public static Bitmap Bitmap;
        public static string Base64Bitmap;
        private static Image imgSource;

        private static Image imgOutput;
        public static string ToBase64(Bitmap bImage)
        {
            MemoryStream memoryStream = new MemoryStream();
            bImage.Save((Stream)memoryStream, ImageFormat.Jpeg);
            byte[] array = memoryStream.ToArray();
            if (array.Length > 500000)
                return VaryQualityLevel((Image)bImage, 50L);
            return array.Length < 100000 ? VaryQualityLevel((Image)bImage, 200L) : Convert.ToBase64String(array);
        }
        public static string VaryQualityLevel(Image bmp1, long quality)
        {
            ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder quality1 = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters encoderParams = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(quality1, quality);
            encoderParams.Param[0] = encoderParameter;
            MemoryStream memoryStream = new MemoryStream();
            bmp1.Save((Stream)memoryStream, encoder, encoderParams);
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            foreach (ImageCodecInfo imageDecoder in ImageCodecInfo.GetImageDecoders())
            {
                if (imageDecoder.FormatID == format.Guid)
                    return imageDecoder;
            }
            return (ImageCodecInfo)null;
        }
        public static Bitmap ScaleImage(Bitmap image)
        {
            int newWidth = (int)(image.Width * 2.5);
            int newHeight = (int)(image.Height * 2.5);
            Bitmap newBmp = new Bitmap(image.Width, image.Height);
            Graphics g = Graphics.FromImage(newBmp);
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
                   new float[] {.3f, .3f, .3f, 0, 0},
                   new float[] {.59f, .59f, .59f, 0, 0},
                   new float[] {.11f, .11f, .11f, 0, 0},
                   new float[] {0, 0, 0, 1, 0},
                   new float[] {0, 0, 0, 0, 1}
               });
            ImageAttributes img = new ImageAttributes();
            img.SetColorMatrix(colorMatrix);
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, img);
            g.Dispose();
            return newBmp;
            
            //int newWidth = (int)(image.Width * 2.5);
            //int newHeight = (int)(image.Height * 2.5);


            //Bitmap result = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            //result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //using (Graphics g = Graphics.FromImage(result))
            //{
            //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    g.CompositingQuality = CompositingQuality.HighQuality;
            //    g.SmoothingMode = SmoothingMode.HighQuality;
            //    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //    g.DrawImage(image, 0, 0, result.Width, result.Height);
            //}
            //return GrayScale(result);
        }
        public static Bitmap GrayScale(Bitmap Bmp)
        {
            int rgb;
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return Bmp;
        }

        // <summary>
        // this function Capture the image from IPCam 

        // </summary>


        public static string ipcam_to_string64(WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl ipcam)
        {
            string base64string = "";

            if (ipcam.IsPlaying)
            {

                Image myimage = MakeGrayscale16bit(ipcam.GetCurrentFrame());

                if ((myimage.Width >= 1280) && (myimage.Height >= 720) && size_check(myimage) == true)
                {

                    base64string = picture_to_base64(myimage);
                    return base64string;
                }
                else
                {

                    base64string = picture_to_base64(myimage);
                    return base64string;
                }
            }
            else
                return base64string;


        }


        // <summary>
        // this function convert image to base64string

        // </summary>

        private static string picture_to_base64(Image cam_image)
        {
            MemoryStream stream = new MemoryStream();
            cam_image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return Convert.ToBase64String(stream.ToArray());


        }

        // <summary>
        // This function Convert Image to Grayscale

        // </summary>



        private static Bitmap MakeGrayscale16bit(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);
            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });
            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();
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


        // <summary>
        // This function Check the Size of Image
        // </summary>



        private static bool size_check(Image input)
        {
            MemoryStream stream = new MemoryStream();
            input.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            decimal x = stream.Length / 1024;
            x = Math.Round(x);
            if ((x >= 100) && (x <= 500))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string GetBase64Image2(StreamPlayerControl spc, CameraImageQuality imageQuality)
        {
            string result = string.Empty;
            float contrast = 3f;
            try
            {
                System.Drawing.Size ExpectedSize = new System.Drawing.Size(1280, 720);
                Bitmap currentFrame = spc.GetCurrentFrame();

                if (imageQuality == CameraImageQuality.Good)
                {
                    contrast = 1f;
                    imgSource = MakeSixteenBitGrayscaleAverageQaulity(ResizeImage(currentFrame, ExpectedSize), contrast);

                }

                else if (imageQuality == CameraImageQuality.Average)
                {
                    contrast = .05f;
                    imgSource = MakeSixteenBitGrayscaleAverageQaulity(ResizeImage(currentFrame, ExpectedSize), contrast);
                }
                else
                {
                    contrast = .05f;
                    imgSource = MakeSixteenBitGrayscaleAverageQaulity(ResizeImage(currentFrame, ExpectedSize), contrast);
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    imgSource.Save(memoryStream, ImageFormat.Jpeg);
                    imgOutput = imgSource;
                    double imgSize = (double)memoryStream.Length / 1000.0;

                    if (imgSize >= 500.0)
                    {
                        double newImageSize = imgSize;
                        int reducePercen = ((int)Math.Round(((imgSize - 500) / imgSize) * 100) + 1);
                        Resize1(reducePercen, 0, false);
                    }
                    else if (imgSize < 100.0)
                    {
                        double newImageSize = imgSize;
                        int reducePercen = ((int)Math.Round(((100 - imgSize) / imgSize) * 100) + 1);
                        Resize1(reducePercen, 0, true);
                    }
                }
                result = ConvertBase64bmp((Bitmap)imgOutput);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " :  at CL - ImageCapture - GetBase64Image");
            }
        }
        private static string ConvertBase64bmp(Bitmap bmp)
        {
            try
            {
                string Base64string = string.Empty;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bmp.Save(memoryStream, ImageFormat.Jpeg);
                    byte[] inArray = memoryStream.ToArray();
                    Base64string = Convert.ToBase64String(inArray);
                }
                return Base64string;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " :  at CL - ImageCapture - ConvertBase64bmp");
            }

        }

        public static string GetBase64Image(StreamPlayerControl spc, CameraImageQuality imageQuality)
        {
            string result = string.Empty;
            float contrast = 3f;
            try
            {
                System.Drawing.Size ExpectedSize = new System.Drawing.Size(1280, 720);
                Bitmap currentFrame = spc.GetCurrentFrame();

                if (imageQuality == CameraImageQuality.Good)
                {
                    contrast = 1f;
                    imgSource = MakeSixteenBitGrayscaleAverageQaulity(ResizeImage(currentFrame, ExpectedSize), contrast);

                }

                else if (imageQuality == CameraImageQuality.Average)
                {
                    contrast = .05f;
                    imgSource = MakeSixteenBitGrayscaleAverageQaulity(ResizeImage(currentFrame, ExpectedSize), contrast);
                }
                else
                {
                    contrast = .05f;
                    imgSource = MakeSixteenBitGrayscaleAverageQaulity(ResizeImage(currentFrame, ExpectedSize), contrast);
                }


                using (MemoryStream memoryStream = new MemoryStream())
                {
                    imgSource.Save(memoryStream, ImageFormat.Jpeg);
                    imgOutput = imgSource;
                    double imgSize = (double)memoryStream.Length / 1000.0;

                    if (imgSize >= 500.0)
                    {
                        int reducePercen = ((int)Math.Round(((imgSize - 500) / imgSize) * 100) + 1);
                        contrast = contrast - reducePercen / 100;
                        imgSource = MakeSixteenBitGrayscaleAverageQaulity(ResizeImage(currentFrame, ExpectedSize), contrast);

                    }
                    else if (imgSize < 100.0)
                    {
                        int increasePercen = ((int)Math.Round(((100 - imgSize) / imgSize) * 100) + 1);
                        contrast = contrast + increasePercen / 100;
                        imgSource = MakeSixteenBitGrayscaleAverageQaulity(ResizeImage(currentFrame, ExpectedSize), contrast);
                    }
                }
                result = ConvertBase64bmp((Bitmap)imgSource);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " :  at CL - ImageCapture - GetBase64Image");
            }
        }


        private static Bitmap MakeSixteenBitGrayscaleAverageQaulity(Bitmap original, float contrast)
        {
            //create a blank bitmap the same size as original

            var newBitmap = new Bitmap(original);

            //get a graphics object from the new image
            var g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            var colorMatrix = new ColorMatrix(
               new float[][]
              {

                  new float[] { contrast, contrast, contrast, 0, 0},
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

        private static Bitmap MakeSixteenBitGrayscaleLowQuality(Bitmap original)
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

        private static Bitmap MakeSixteenBitGrayscaleHighQuality(Bitmap original)
        {
            //create a blank bitmap the same size as original
            var newBitmap = new Bitmap(original);
            //get a graphics object from the new image
            var g = Graphics.FromImage(newBitmap);
            //create the grayscale ColorMatrix
            var colorMatrix = new ColorMatrix(
               new float[][]
              {
                  new float[] {1f, 1f, 1, 0, 0},
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


        public static Bitmap ResizeImage(Image imgToResize, System.Drawing.Size size)
        {
            int width = imgToResize.Width;
            int height = imgToResize.Height;
            float i = 0;
            float dWidth = 0;
            float dheight = 0;
            dWidth = (float)size.Width / (float)width;
            dheight = (float)size.Height / (float)height;
            i = (!(dheight < dWidth)) ? dWidth : dheight;

            int width2 = (int)Math.Round(Math.Truncate(width * i));
            int height2 = (int)Math.Round(Math.Truncate(height * i));
            Bitmap bitmap = new Bitmap(width2, height2);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(imgToResize, 0, 0, width2, height2);
            graphics.Dispose();
            return bitmap;

        }

        public static void Resize1(int intPercent, int intType, bool increaseSize)
        {
            int width = 0; int height = 0;
            if (increaseSize)
            {
                width = imgSource.Width + (int)((imgSource.Width * intPercent) / 100);
                height = imgSource.Height + (int)((imgSource.Height * 100) / 100);
            }
            else
            {
                width = imgSource.Width - (int)((imgSource.Width * intPercent) / 100);
                height = imgSource.Height - (int)((imgSource.Height * 100) / 100);
            }

            Bitmap image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);
            switch (intType)
            {
                case 0:
                    graphics.InterpolationMode = InterpolationMode.Default;
                    break;
                case 1:
                    graphics.InterpolationMode = InterpolationMode.High;
                    break;
                case 2:
                    graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    break;
                case 3:
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    break;
            }
            graphics.DrawImage(imgSource, 0, 0, width, height);
            imgOutput = image;

        }

    }
    public enum CameraImageQuality
    {
        Good = 1,
        Average = 2,
        Low = 3
    }
}

