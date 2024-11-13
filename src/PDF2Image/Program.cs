using System.Drawing.Imaging;
using PdfiumViewer;

namespace PDF2Image;

class Program
{
    static void Main(string[] args)
    {
        if (Console.GetCursorPosition() == (0, 0) && args.Length == 0)
        {
            Console.WriteLine("Usage: PDF2Image.exe input output");
            Console.WriteLine("");
            Console.WriteLine("Input can be a PDF file or folder path");
            Console.WriteLine("Output must be a folder path");
            FinishApplication();
            return;
        }
        
        var inputPath = args[0];
        var outputPath = args[1];
        
        if (PathValidator.IsDirectoryPath(inputPath))
        {
            var files = Directory.GetFiles(inputPath, "*.pdf");
            bool pdfExists = files.Length > 0;
            if (!pdfExists)
            {  
                Console.WriteLine("Folder doesn't contains PDF files.");
                return;
            }  
            Console.WriteLine($"Found {files.Length} PDF files.");  
            
            foreach (var file in files)
                ConvertPdfToImages(file, outputPath);
            
        }
        else if (PathValidator.IsFilePath(inputPath))
        {
            ConvertPdfToImages(inputPath, outputPath);
        }

        FinishApplication();
    }

    private static void FinishApplication()
    {
        Console.WriteLine("Press enter to close...");
        Console.ReadLine();
    }


    private static void ConvertPdfToImages(string pdfFilePath, string outputFolder)
    {
        outputFolder = CreateOutputFolderIfNotExists(pdfFilePath, outputFolder);
        
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

    private static string CreateOutputFolderIfNotExists(string pdfFilePath, string outputFolder)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pdfFilePath);
        var newOutputFolder = Path.Combine(outputFolder, "[PDF] " + fileNameWithoutExtension);
        
        if (Directory.Exists(newOutputFolder)) return newOutputFolder;
        
        Directory.CreateDirectory(newOutputFolder);
        Console.WriteLine($"Output folder \"{new DirectoryInfo(newOutputFolder).Name}\" created with success");
        
        return newOutputFolder;
    }
}