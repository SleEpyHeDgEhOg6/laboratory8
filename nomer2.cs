using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace FileSearchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Поиск файла");
                Console.WriteLine("2 - Выход");
                Console.Write("Ваш выбор: ");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        SearchAndProcessFile();
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void SearchAndProcessFile()
        {
            try
            {
                Console.Write("Введите путь для поиска: ");
                string searchPath = Console.ReadLine();
                
                if (!Directory.Exists(searchPath))
                {
                    Console.WriteLine("Указанный путь не существует!");
                    return;
                }
                
                Console.Write("Введите имя файла для поиска: ");
                string fileName = Console.ReadLine();
                
                string[] foundFiles = Directory.GetFiles(searchPath, fileName, SearchOption.AllDirectories);
                
                if (foundFiles.Length == 0)
                {
                    Console.WriteLine("Файл не найден!");
                    return;
                }

                Console.WriteLine($"\nНайдено файлов: {foundFiles.Length}");
                
                for (int i = 0; i < foundFiles.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {foundFiles[i]}");
                }
                
                Console.Write($"\nВыберите файл для обработки (1-{foundFiles.Length}): ");
                if (int.TryParse(Console.ReadLine(), out int fileIndex) && fileIndex >= 1 && fileIndex <= foundFiles.Length)
                {
                    string selectedFile = foundFiles[fileIndex - 1];
                    ProcessFile(selectedFile);
                }
                else
                {
                    Console.WriteLine("Неверный выбор!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ProcessFile(string filePath)
        {
            try
            {
                Console.WriteLine($"\nОбработка файла: {filePath}");

                FileInfo fileInfo = new FileInfo(filePath);
                Console.WriteLine($"Размер: {fileInfo.Length} байт");
                Console.WriteLine($"Создан: {fileInfo.CreationTime}");

                Console.WriteLine("\nСодержание файла");
                DisplayFileContent(filePath);

                Console.Write("\nХотите сжать файл? (y/n): ");
                string compressChoice = Console.ReadLine();
                
                if (compressChoice?.ToLower() == "y")
                {
                    CompressFile(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки файла: {ex.Message}");
            }
        }

        static void DisplayFileContent(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    int lineCount = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null && lineCount < 20)
                    {
                        Console.WriteLine(line);
                        lineCount++;
                    }
                    
                    if (line != null)
                    {
                        Console.WriteLine("... (файл слишком большой, показаны первые 20 строк)");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            }
        }

        static void CompressFile(string filePath)
        {
            try
            {
                string compressedFilePath = filePath + ".gz";
                
                using (FileStream originalFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (FileStream compressedFileStream = File.Create(compressedFilePath))
                using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                {
                    originalFileStream.CopyTo(compressionStream); //копирование со сжатием 
                }

                FileInfo originalInfo = new FileInfo(filePath);
                FileInfo compressedInfo = new FileInfo(compressedFilePath);
                
                double compressionRatio = (1 - (double)compressedInfo.Length / originalInfo.Length) * 100;
                
                Console.WriteLine($"Файл сжат успешно!");
                Console.WriteLine($"Исходный размер: {originalInfo.Length} байт");
                Console.WriteLine($"Сжатый размер: {compressedInfo.Length} байт");
                Console.WriteLine($"Степень сжатия: {compressionRatio:F2}%");
                Console.WriteLine($"Сжатый файл: {compressedFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сжатия: {ex.Message}");
            }
        }
    }
}
