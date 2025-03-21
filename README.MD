# RmbgSharp - Background removal in C#

![RmbgSharp](https://img.shields.io/badge/.NET-9.0-blue.svg) ![ML.NET](https://img.shields.io/badge/ML.NET-Supported-brightgreen.svg) ![ONNXRuntime](https://img.shields.io/badge/ONNXRuntime-Supported-blue.svg) ![DirectML](https://img.shields.io/badge/DirectML-Supported-orange.svg) ![PRs Welcome](https://img.shields.io/badge/PRs-Welcome-brightgreen.svg)

## 🚀 RmbgSharp: The World's Fastest AI Background Removal Library for C#

**The first and only C# library leveraging the cutting-edge "RMBG-2.0" AI model by BRIA AI for incredibly efficient, effective, and precise background removal!** ⚡

This library is built upon the foundation of the remarkable [BRIA AI](https://huggingface.co/briaai/RMBG-2.0) (🙏 Thank you BRIA AI for making this model accessible for non-commercial fair use!), providing unparalleled background removal capabilities for developers working within the C# ecosystem.

By harnessing the power of the optimized ONNX Runtime and its seamless integration with DirectML, RmbgSharp achieves near-instant processing on a diverse range of hardware, including GPUs from NVIDIA, AMD, and Intel. This capability extends to images of any resolution, ensuring flexibility and consistent performance across various applications.

Furthermore, the availability of ONNX models in both FP32 and FP16 formats allows developers to fine-tune the library for optimal precision or speed, depending on their specific requirement.

RmbgSharp stands out as the sole C# library that integrates the efficiency of the RMBG-2.0 AI model for background removal in .NET projects, marking a significant advancement in the tools available to C# developers.

## ✨ Key Features

- **Unmatched Speed and Performance:** Experience remarkably fast background removal, a direct result of the optimized ONNX Runtime working in concert with hardware-accelerated DirectML 1. The ONNX Runtime is designed as a high-performance engine for executing machine learning models efficiently across different platforms and hardware 5. DirectML further amplifies this efficiency by enabling the utilization of the GPU's parallel processing capabilities for machine learning tasks 6. This combination allows RmbgSharp to perform background removal at speeds previously unattainable in C# using AI-based methods.
