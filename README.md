# PDF2Image

PDF2Image is a command-line application developed in C# that converts PDF pages into PNG images. It uses the **PdfiumViewer** library to render and save each PDF page as a high-quality image.

## Features

- Converts PDF files into PNG images, saving each page as a separate image.
- Supports both file and folder input:
    - **PDF File**: Converts a single PDF file into images.
    - **Folder with PDFs**: Converts all PDF files in a folder into images.
- Creates a unique output folder for each PDF file, organizing images by PDF.

## Usage

The application can be run via command line with the following syntax:

```bash
PDF2Image.exe <input> <output>
```

## Parameters
- input: Path to the PDF file or folder containing PDF files.
- output: Path to the folder where the images will be saved.

## Usage Examples

1. To convert a single PDF file into images:

```bash
Copy code
PDF2Image.exe "C:\path\to\file.pdf" "C:\path\to\output"
```

2. To convert all PDF files in a folder:

```bash
Copy code
PDF2Image.exe "C:\path\to\pdf_folder" "C:\path\to\output"
```

## Output Structure
The application creates a subfolder within the output directory, named with the prefix [PDF] followed by the PDF file name. Images are saved with sequential names, e.g., Page_1.png, Page_2.png, etc.

## Log Messages
During execution, the application displays messages in the console to indicate progress and results:

- Number of PDF files found in the input folder.
- Creation of output folders.
- Location of the generated PNG files.

## Prerequisites
- .NET: Make sure .NET 8 or later is installed to run the application.
- Libraries: The application uses the PdfiumViewer library for PDF rendering.

## Common Errors
1. No PDF in input folder: When the input path is a folder without any PDF files, the application will notify that no PDFs are available.
2. File not found: If the PDF or folder path is incorrect, the application won’t be able to access the file.

## Contribution
Feel free to contribute by reporting issues or suggesting improvements.