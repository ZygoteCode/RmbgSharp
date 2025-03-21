# RmbgSharp - Background removal in C#

![RmbgSharp](https://img.shields.io/badge/.NET-9.0-blue.svg) ![ML.NET](https://img.shields.io/badge/ML.NET-Supported-brightgreen.svg) ![ONNXRuntime](https://img.shields.io/badge/ONNXRuntime-Supported-blue.svg) ![DirectML](https://img.shields.io/badge/DirectML-Supported-orange.svg) ![PRs Welcome](https://img.shields.io/badge/PRs-Welcome-brightgreen.svg)

## 🚀 RmbgSharp: The World's Fastest AI Background Removal Library for C#

**The first and only C# library leveraging the cutting-edge "RMBG-2.0" AI model by BRIA AI for incredibly efficient, effective, and precise background removal!** ⚡

This library is built upon the foundation of the remarkable [BRIA AI](https://huggingface.co/briaai/RMBG-2.0) (🙏 Thank you BRIA AI for making this model accessible for non-commercial fair use!), providing unparalleled background removal capabilities for developers working within the C# ecosystem.

By harnessing the power of the optimized ONNX Runtime and its seamless integration with DirectML, RmbgSharp achieves near-instant processing on a diverse range of hardware, including GPUs from NVIDIA, AMD, and Intel. This capability extends to images of any resolution, ensuring flexibility and consistent performance across various applications.

Furthermore, the availability of ONNX models in both FP32 and FP16 formats allows developers to fine-tune the library for optimal precision or speed, depending on their specific requirement.

RmbgSharp stands out as the sole C# library that integrates the efficiency of the RMBG-2.0 AI model for background removal in .NET projects, marking a significant advancement in the tools available to C# developers.

## ✨ Key Features

- **🚀 Unmatched Speed and Performance:** Experience remarkably fast background removal, a direct result of the optimized ONNX Runtime working in concert with hardware-accelerated DirectML. The ONNX Runtime is designed as a high-performance engine for executing machine learning models efficiently across different platforms and hardware. DirectML further amplifies this efficiency by enabling the utilization of the GPU's parallel processing capabilities for machine learning tasks. This combination allows RmbgSharp to perform background removal at speeds previously unattainable in C# using AI-based methods.
- **🎯 High Accuracy:** The library's foundation on the state-of-the-art RMBG-2.0 model from BRIA AI guarantees highly accurate background segmentation. This model represents a significant improvement over previous versions, demonstrating enhanced effectiveness in separating foreground from background across a wide variety of image categories. Its performance is comparable to leading source-available models, ensuring that RmbgSharp provides results of exceptional quality.
- **⚙️ ONNX Runtime Power:** RmbgSharp leverages the efficiency and inherent cross-platform capabilities of ONNX Runtime for seamless execution of the background removal model. While RmbgSharp is specifically designed for C#, ONNX Runtime itself is a cross-platform machine learning model accelerator, supporting various operating systems and hardware architectures. This foundational technology ensures robust and optimized performance within the C# environment.
- **⚡ DirectML Acceleration:** RmbgSharp achieves near-instant background removal on a wide array of DirectX 12-compatible GPUs (including those from NVIDIA, AMD, Intel, and Qualcomm) through its seamless integration with DirectML. DirectML is a high-performance, hardware-accelerated DirectX 12 library specifically designed for machine learning tasks on Windows. By utilizing DirectML as an execution provider for ONNX Runtime, RmbgSharp can significantly improve the evaluation time of the background removal model on readily available GPU hardware.
- **🖼️ Flexible Resolution Support:** RmbgSharp is engineered to process images of virtually any size without a noticeable compromise in performance or accuracy. Both RmbgSharp and the underlying RMBG-2.0 model are designed to handle varying image resolutions effectively. This capability ensures that users are not restricted by image dimensions and can seamlessly integrate the library into diverse applications dealing with images of different sizes.
- **💾 FP32 & FP16 Models:** RmbgSharp provides the flexibility to choose between Full Precision (FP32) models for scenarios demanding the highest possible accuracy and Half Precision (FP16) models for applications where reduced memory consumption and potentially faster inference speeds are prioritized. ONNX Runtime supports both of these floating-point formats 3, allowing RmbgSharp to cater to a wide range of hardware capabilities and performance requirements.
- **🥇 First and Only C# Solution:** RmbgSharp distinguishes itself as the pioneering C# library to harness the efficiency of the RMBG-2.0 model for AI-powered background removal directly within .NET applications. This library addresses a previously unmet need within the C# development community, providing a unique and powerful tool for image processing tasks.

## 🛠️ Installation

```sh
# Install ONNXRuntime and ML.NET
Install-Package Microsoft.ML.OnnxRuntime
Install-Package Microsoft.ML
Install-Package Microsoft.ML.OnnxRuntime.DirectML
```

## 🧑‍💻 Example Usage

```csharp
﻿using RmbgSharp;

public class Program
{
    public static void Main()
    {
        Console.Title = "RmbgSharp | Made by https://github.com/ZygoteCode/";
        Console.WriteLine("Removing the background from \"input.png\", please wait a while.");

        BackgroundRemover backgroundRemover = new BackgroundRemover("rmbg_2.0_fp32.onnx", false, true);
        backgroundRemover.RemoveBackground("input.png", "output.png");

        Console.WriteLine("Succesfully removed the background from \"input.png\"!");
        Console.WriteLine("The result image is exported as \"output.png\".");
        Console.WriteLine("Press the ENTER key in order to exit from the application.");

        Console.ReadLine();
    }
}
```

## 🌟 Contributing

Contributions are welcome there! 🚀 Follow these steps to create a **Pull Request (PR):**

1. **Fork the repository**
2. **Clone your fork**:
   ```sh
   git clone https://github.com/your-username/VadSharp.git
   ```
3. **Create a new branch**:
   ```sh
   git checkout -b feature-branch
   ```
4. **Make your changes & commit**:
   ```sh
   git add .
   git commit -m "Your awesome feature!"
   ```
5. **Push the branch & create a PR**:
   ```sh
   git push origin feature-branch
   ```
6. **Open a PR on GitHub**

---

## 🐛 Issues & Bug Reports

If you find a bug or have a feature request, please **open an issue**:

1. Go to the [Issues Tab](https://github.com/ZygoteCode/RmbgSharp/issues).
2. Click on **"New Issue"**.
3. Provide a **clear and concise** description of the problem.
4. If possible, include **screenshots and logs**.

I will review and respond ASAP! 🚀

---

## ✨ Credits

All of my credits go to the original inventor of the [RMBG-2.0](https://huggingface.co/briaai/RMBG-2.0) project,
which has worked hard to the architecture of the algorithm and trained the models!

## 📜 License

RmbgSharp is licensed under the **MIT License**. Feel free to use, modify, and distribute it as you like!

📌 **Made with ❤️ by [ZygoteCode](https://github.com/ZygoteCode/)**
