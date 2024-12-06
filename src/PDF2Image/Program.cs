using System.Drawing.Imaging;
using System.Reflection;
using System.Web;
using PdfiumViewer;

namespace PDF2Image;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            ShowHelp();
            return;
        }
        
        string inputPath = null;
        string outputPath = null;
        var prefix = string.Empty;
        var clearOutput = false;
        foreach (var arg in args)
        {
            if (inputPath == null)
            {
                inputPath = arg;
            }
            else if (outputPath == null)
            {
                outputPath = arg;
            }
            else if (arg.StartsWith("--prefix="))
            {
                prefix = HttpUtility.UrlDecode(arg["--prefix=".Length..]);
            }
            else if (arg == "--clear")
            {
                clearOutput = true;
            }
        }
        
        if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
        {
            Console.WriteLine("Error: Both --input and --output parameters are required.");
            ShowHelp();
            return;
        }
        
        Console.WriteLine("PDF2Image - version 1.1.0");
        Console.WriteLine("Starting PDF conversion with the following options:");
        Console.WriteLine($"Input Path: {inputPath}");
        Console.WriteLine($"Output Path: {outputPath}");
        Console.WriteLine($"Prefix: {(prefix ?? "None")}");
        Console.WriteLine($"Clear Output Folder: {clearOutput}");
        
        if (PathValidator.IsDirectoryPath(inputPath!))
        {
            var files = Directory.GetFiles(inputPath!, "*.pdf");
            var pdfExists = files.Length > 0;
            if (!pdfExists)
            {  
                Console.WriteLine("Folder doesn't contains PDF files.");
                return;
            }  
            Console.WriteLine($"Found {files.Length} PDF files.");
            foreach (var file in files)
            {
                var outputFolder = CreateOutputFolderIfNotExists(file, outputPath!, prefix, clearOutput);
                ConvertPdfToImages(file, outputFolder);
            }
            
        }
        else if (PathValidator.IsFilePath(inputPath!))
        {
            var outputFolder = CreateOutputFolderIfNotExists(inputPath!, outputPath!, prefix, clearOutput);
            ConvertPdfToImages(inputPath!, outputFolder);
        }
    }

    private static void ShowHelp()
    {
        Console.WriteLine("PDF2Image Converter - Help");
        Console.WriteLine("Usage:");
        Console.WriteLine("  PDF2Image.exe [input] [output] [options]");
        Console.WriteLine();
        Console.WriteLine("Arguments:");
        Console.WriteLine("  --input=<input_path>      Path to the folder containing PDF files or a single PDF file.");
        Console.WriteLine("  --output=<output_path>    Path to the folder where images will be saved.");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  --prefix=<prefix>         Add a prefix to the output folder (e.g., [new] ).");
        Console.WriteLine("  --clear                   Clear the output folder before conversion.");
        Console.WriteLine("  --help                    Show this help message.");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  PDF2Image.exe \"C:\\PDFs\" \"C:\\Images\" --clear");
        Console.WriteLine("  PDF2Image.exe \"C:\\PDFs\\file.pdf\" \"C:\\Images\" --prefix=[new]");
    }

    private static void ConvertPdfToImages(string pdfFilePath, string outputFolder)
    {
        using (var pdfDocument = PdfDocument.Load(pdfFilePath))
        {
            var totalPages = pdfDocument.PageCount;

            for (var page = 0; page < totalPages; page++)
            {
                using (var image = pdfDocument.Render(page, 300, 300, PdfRenderFlags.Annotations))
                {
                    var outputPath = Path.Combine(outputFolder, $"Page_{page + 1}.png");

                    image.Save(outputPath, ImageFormat.Png);
                }
            }
            var directoryName = new DirectoryInfo(outputFolder).Name;
            Console.WriteLine($"Images saved at folder called \"{directoryName}\"");
        }
    }

    private static string CreateOutputFolderIfNotExists(string pdfFilePath, string outputFolder, string prefix, bool clearOutput = false)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pdfFilePath);
        var newOutputFolder = Path.Combine(outputFolder, prefix + fileNameWithoutExtension);
        
        if (Directory.Exists(newOutputFolder))
        {
            ClearOutputFolder(newOutputFolder);
            return newOutputFolder;
        }
        
        Directory.CreateDirectory(newOutputFolder);
        Console.WriteLine($"Output folder \"{new DirectoryInfo(newOutputFolder).Name}\" created with success");
        
        return newOutputFolder;
    }

    private static void ClearOutputFolder(string outputPath)
    {
        Console.WriteLine("Clearing the output folder...");
        try
        {
            Directory.GetFiles(outputPath).ToList().ForEach(File.Delete);
            Console.WriteLine("Output folder cleared successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing the output folder: {ex.Message}");
        }
    }
}