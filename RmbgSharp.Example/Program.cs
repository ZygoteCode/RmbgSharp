using RmbgSharp;

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