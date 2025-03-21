// Importing required namespaces for working with images, machine learning models, and tensors
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.ML.OnnxRuntime; // For ONNX model inference
using Microsoft.ML.OnnxRuntime.Tensors; // For working with tensors

namespace RmbgSharp
{
    // This class is responsible for removing the background from an image using a machine learning model
    public class BackgroundRemover
    {
        // Private member to hold the ONNX inference session, which is used to run the model
        private InferenceSession _inferenceSession;

        // Constructor for the BackgroundRemover class. It initializes the inference session using an ONNX model
        public BackgroundRemover(string modelPath, bool useFP16, bool useGPU)
        {
            // Create a session options object to configure the ONNX model session
            SessionOptions sessionOptions = new SessionOptions();
            sessionOptions.LogSeverityLevel = OrtLoggingLevel.ORT_LOGGING_LEVEL_FATAL;

            // If the user wants to use FP16 precision, set the optimization level for FP16 support
            if (useFP16)
            {
                sessionOptions.GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_EXTENDED;
            }

            // If the user wants to use the GPU, try to enable the DirectML (DML) execution provider
            if (useGPU)
            {
                try
                {
                    // Attempt to append the DML execution provider to the session options
                    sessionOptions.AppendExecutionProvider_DML();
                }
                catch
                {
                    // In case of any failure (e.g., no GPU or incompatible hardware), the exception is silently caught
                }
            }

            // Initialize the inference session with the provided model path and session options
            _inferenceSession = new InferenceSession(modelPath, sessionOptions);
        }

        // Overloaded method to remove the background from a bitmap and save it to an output file
        public void RemoveBackground(Bitmap bitmap, string outputPath)
        {
            // Calls the RemoveBackground method and saves the result to the specified output path
            RemoveBackground(bitmap).Save(outputPath);
        }

        // Overloaded method to load an image from a file, remove its background, and save it to an output file
        public void RemoveBackground(string inputPath, string outputPath)
        {
            // Load the image from the file, convert it to a Bitmap, and remove its background
            RemoveBackground((Bitmap)Image.FromFile(inputPath), outputPath);
        }

        // Method to remove the background from an image represented by a Bitmap
        public Bitmap RemoveBackground(Bitmap bitmap)
        {
            // Preprocess the image (resize and normalize) and extract pixel data
            float[,,]? image = LoadAndPreprocessImage(bitmap, out var width, out var height);

            // Create a new tensor to hold the image data, reshaped for the model input
            DenseTensor<float>? inputTensor = new DenseTensor<float>(new[] { 1, 3, width, height });

            // Fill the tensor with the preprocessed image data
            for (int c = 0; c < 3; c++) // Loop over color channels (RGB)
            {
                for (int y = 0; y < height; y++) // Loop over rows
                {
                    for (int x = 0; x < width; x++) // Loop over columns
                    {
                        // Set the corresponding value in the tensor
                        inputTensor[0, c, y, x] = image[c, y, x];
                    }
                }
            }

            // Create the input tensor for the model, associating it with the name expected by the model
            List<NamedOnnxValue> inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("pixel_values", inputTensor)
            };

            // Run the model on the input tensor and get the results
            IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = _inferenceSession.Run(inputs);

            // Extract the resulting tensor from the model's output
            Tensor<float>? resultTensor = results.First().AsTensor<float>();

            // Convert the result tensor into a 1D array
            float[]? resultArray = resultTensor.ToArray();

            // Create a 2D mask to store the model's output (binary mask for background removal)
            float[,]? mask = new float[height, width];
            int idx = 0;

            // Populate the mask with the values from the result array
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    mask[y, x] = resultArray[idx++];
                }
            }

            // Apply the mask to the image and return the result
            return ApplyMaskToImage(bitmap, mask);
        }

        // Helper method to preprocess the image by resizing it and normalizing the pixel values
        private float[,,] LoadAndPreprocessImage(Bitmap bitmap, out int width, out int height)
        {
            // Set the target width and height for the image resizing
            width = 1024;
            height = 1024;

            // Resize the image to match the target dimensions
            Bitmap? resizedImage = new Bitmap(bitmap, new Size(width, height));

            // Initialize a 3D array to store the normalized RGB pixel values
            float[,,]? data = new float[3, height, width];

            // Lock the bits of the resized image for efficient processing
            BitmapData bmpData = resizedImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            IntPtr ptr = bmpData.Scan0; // Pointer to the image data in memory
            int bytesPerPixel = Image.GetPixelFormatSize(bmpData.PixelFormat) / 8; // Number of bytes per pixel (4 for ARGB format)
            int stride = bmpData.Stride; // Stride (number of bytes per row)

            // Using unsafe code to directly manipulate image memory for faster processing
            unsafe
            {
                byte* dataPtr = (byte*)ptr;

                // Loop through each pixel in the image
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int offset = y * stride + x * bytesPerPixel; // Calculate the offset for the pixel

                        // Extract the color channels (Blue, Green, Red) from the ARGB pixel
                        byte b = dataPtr[offset];
                        byte g = dataPtr[offset + 1];
                        byte r = dataPtr[offset + 2];

                        // Normalize the color channels to the model's expected range
                        data[0, y, x] = (r / 255f - 0.485f) / 0.229f; // Normalize Red channel
                        data[1, y, x] = (g / 255f - 0.456f) / 0.224f; // Normalize Green channel
                        data[2, y, x] = (b / 255f - 0.406f) / 0.225f; // Normalize Blue channel
                    }
                }
            }

            // Unlock the bits after processing
            resizedImage.UnlockBits(bmpData);

            // Return the preprocessed data
            return data;
        }

        // Helper method to apply a mask to an image and return the resulting image with background removed
        private Bitmap ApplyMaskToImage(Bitmap bitmap, float[,] mask)
        {
            // Get the dimensions of the original image
            int width = bitmap.Width;
            int height = bitmap.Height;

            // Create a new bitmap to store the result
            Bitmap? resultImage = new Bitmap(width, height);

            // Resize the mask to match the dimensions of the original image
            float[,]? resizedMask = ResizeMask(mask, width, height);

            // Lock the bits of the original image and the result image for efficient processing
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resultData = resultImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            IntPtr ptr = bmpData.Scan0; // Pointer to the original image data
            IntPtr resultPtr = resultData.Scan0; // Pointer to the result image data
            int bytesPerPixel = Image.GetPixelFormatSize(bmpData.PixelFormat) / 8; // Bytes per pixel
            int stride = bmpData.Stride; // Stride (number of bytes per row)

            // Using unsafe code for efficient pixel manipulation
            unsafe
            {
                byte* dataPtr = (byte*)ptr;
                byte* resultDataPtr = (byte*)resultPtr;

                // Loop through each pixel in the image
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int offset = y * stride + x * bytesPerPixel; // Calculate the pixel offset

                        // Get the original color channels (Blue, Green, Red)
                        byte b = dataPtr[offset];
                        byte g = dataPtr[offset + 1];
                        byte r = dataPtr[offset + 2];

                        // Get the mask value at the current pixel
                        var maskValue = resizedMask[y, x];

                        // Calculate the alpha channel based on the mask (0 for transparent, 255 for opaque)
                        var alpha = (int)(maskValue * 255);

                        // Set the pixel values in the result image (RGBA format)
                        resultDataPtr[offset] = b;
                        resultDataPtr[offset + 1] = g;
                        resultDataPtr[offset + 2] = r;
                        resultDataPtr[offset + 3] = (byte)alpha;
                    }
                }
            }

            // Unlock the bits after processing
            bitmap.UnlockBits(bmpData);
            resultImage.UnlockBits(resultData);

            // Return the result image with the background removed
            return resultImage;
        }

        // Helper method to resize the mask to match the target dimensions (width and height)
        private float[,] ResizeMask(float[,] mask, int targetWidth, int targetHeight)
        {
            // Get the original dimensions of the mask
            int maskHeight = mask.GetLength(0);
            int maskWidth = mask.GetLength(1);

            // Create a new array to hold the resized mask
            float[,]? resizedMask = new float[targetHeight, targetWidth];

            // Loop through each pixel in the target size
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    // Calculate the corresponding position in the original mask
                    float origY = (float)y / targetHeight * maskHeight;
                    float origX = (float)x / targetWidth * maskWidth;

                    // Find the nearest neighboring pixels (bilinear interpolation)
                    int y0 = (int)origY;
                    int x0 = (int)origX;

                    // Clamp the values to ensure they are within bounds
                    int y1 = Math.Min(y0 + 1, maskHeight - 1);
                    int x1 = Math.Min(x0 + 1, maskWidth - 1);

                    // Get the values of the neighboring pixels in the original mask
                    float v00 = mask[y0, x0];
                    float v01 = mask[y0, x1];
                    float v10 = mask[y1, x0];
                    float v11 = mask[y1, x1];

                    // Calculate the fractional differences
                    float xFrac = origX - x0;
                    float yFrac = origY - y0;

                    // Perform bilinear interpolation
                    float interpolatedValue = (1 - xFrac) * (1 - yFrac) * v00 +
                                              xFrac * (1 - yFrac) * v01 +
                                              (1 - xFrac) * yFrac * v10 +
                                              xFrac * yFrac * v11;

                    // Set the interpolated value in the resized mask
                    resizedMask[y, x] = interpolatedValue;
                }
            }

            // Return the resized mask
            return resizedMask;
        }
    }
}
