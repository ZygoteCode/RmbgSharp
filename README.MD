# RmbgSharp - Background removal in C#

![RmbgSharp](https://img.shields.io/badge/.NET-9.0-blue.svg) ![ML.NET](https://img.shields.io/badge/ML.NET-Supported-brightgreen.svg) ![ONNXRuntime](https://img.shields.io/badge/ONNXRuntime-Supported-blue.svg) ![DirectML](https://img.shields.io/badge/DirectML-Supported-orange.svg) ![PRs Welcome](https://img.shields.io/badge/PRs-Welcome-brightgreen.svg)

## 🚀 RmbgSharp: The World's Fastest AI Background Removal Library for C#

**The first and only C# library leveraging the cutting-edge "RMBG-2.0" AI model by BRIA AI for incredibly efficient, effective, and precise background removal!** ⚡

This library is built upon the foundation of the remarkable [BRIA AI](https://huggingface.co/briaai/RMBG-2.0) (🙏 Thank you BRIA AI for making this model accessible for non-commercial fair use!), providing unparalleled background removal capabilities for developers working within the C# ecosystem.

By harnessing the power of the optimized ONNX Runtime and its seamless integration with DirectML, RmbgSharp achieves near-instant processing on a diverse range of hardware, including GPUs from NVIDIA, AMD, and Intel. This capability extends to images of any resolution, ensuring flexibility and consistent performance across various applications.

Furthermore, the availability of ONNX models in both FP32 and FP16 formats allows developers to fine-tune the library for optimal precision or speed, depending on their specific requirement.

RmbgSharp stands out as the sole C# library that integrates the efficiency of the RMBG-2.0 AI model for background removal in .NET projects, marking a significant advancement in the tools available to C# developers.

## ✨ Key Features

- **🚀 Unmatched Speed and Performance:** Experience ultra-fast background removal with AI-powered efficiency, thanks to the optimized ONNX Runtime and GPU-accelerated DirectML.
- **🎯 High Accuracy:** RmbgSharp delivers exceptional background segmentation using the advanced RMBG-2.0 model, ensuring highly accurate results across diverse image categories.
- **⚙️ ONNX Runtime Power:** RmbgSharp harnesses ONNX Runtime's cross-platform efficiency for seamless, optimized background removal performance in C#.
- **⚡ DirectML Acceleration:** RmbgSharp delivers near-instant background removal by leveraging DirectML for high-performance GPU acceleration across a wide range of DirectX 12-compatible devices (NVIDIA, AMD, Intel).
- **🖼️ Flexible Resolution Support:** RmbgSharp processes images of any size with flawless performance and accuracy, making it versatile for diverse applications.
- **💾 FP32 & FP16 Models:** RmbgSharp offers flexibility with Full Precision (FP32) for maximum accuracy and Half Precision (FP16) for faster speeds and lower memory use, optimizing performance for diverse hardware.
- **🥇 First and Only C# Solution:** RmbgSharp is the first C# library to integrate the powerful RMBG-2.0 model for AI-driven background removal, filling a crucial gap in .NET image processing.

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
   git clone https://github.com/your-username/RmbgSharp.git
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
