using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganizer
{
    public class FileService
    {
        public string? DirPath { get; set; }

        public void ArrangeFiles(string[] files)
        {
            List<List<string>> processedFiles = IdentifyFileType(files);

            foreach (var file in processedFiles)
            {
                try
                {
                    if (!string.IsNullOrEmpty(DirPath))
                    {
                        string targetDirectory = Path.Combine(DirPath, file[1]);

                        if (!Directory.Exists(targetDirectory))
                        {
                            Directory.CreateDirectory(targetDirectory);
                            Console.WriteLine($"Folder created: {file[1]}");
                        }

                        string targetFilePath = Path.Combine(targetDirectory, Path.GetFileName(file[0]));

                     
                        if (File.Exists(targetFilePath))
                        {
                            string newFileName = Path.GetFileNameWithoutExtension(file[0]) + "_copy" + Path.GetExtension(file[0]);
                            targetFilePath = Path.Combine(targetDirectory, newFileName);
                        }

                        File.Move(file[0], targetFilePath);
                        Console.WriteLine($"Successfully moved {file[0]} to {file[1]}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Directory path is not set.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private List<List<string>> IdentifyFileType(string[] files)
        {
            List<List<string>> fileDetails = new();

            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    string fileExtension = Path.GetExtension(file).TrimStart('.');
                    List<string> fileInfo = new()
                            {
                                file,
                                fileExtension
                            };
                    fileDetails.Add(fileInfo);
                }
            }
            return fileDetails;
        }
    }
}
