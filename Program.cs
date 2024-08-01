using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileOrganizer 
{
    public class Program
    {
        private static void Main(string[] args)
        {
            bool validInput =false;
            FileService fileService = new();

            while (!validInput)
            {
                Console.Write("Enter a directory path: ");
                string? dirPath = Console.ReadLine();

                if (string.IsNullOrEmpty(dirPath))
                {
                    Console.WriteLine("Directory path can't be empty or null");
                    continue;
                }

                try
                {
                    string[] files = Directory.GetFiles(dirPath);
                    validInput = true;
                    fileService.DirPath = dirPath;
                    fileService.ArrangeFiles(files);
                    
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("\nDirectory not found. Please enter valid path\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError occurred: {ex.Message}\n");
                }
            }
        }
    }
}
