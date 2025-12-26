using System;
using System.IO;
using System.Xml.Serialization;
using AnimalLibrary;

namespace SerializationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal[] animals = new Animal[]
            {
                new Cow("Россия", false, "Коровка"),
                new Lion("Африка", true, "Львенок"),
                new Pig("США", false, "Свинка")
            };
            for (int i = 0; i < animals.Length; i++)
            {
                string fileName = $"animal_{i + 1}.xml";
                bool success = SerializeAnimal(animals[i], fileName);
                
                if (success)
                {
                    Console.WriteLine($"Сериализован: {animals[i]}");
                    Console.WriteLine($"Файл: {fileName}\n");
                }
                else
                {
                    Console.WriteLine($"Ошибка сериализации для: {animals[i]}\n");
                }
            }

            Console.WriteLine("Сериализация завершена!");
            
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //путь к рабочему столу пользователя 
            string serializationFolder = Path.Combine(desktopPath, "AnimalSerialization"); //формируем путь к папке 
            Console.WriteLine($"Папка: {serializationFolder}");
            
            if (Directory.Exists(serializationFolder))
            {
                foreach (string file in Directory.GetFiles(serializationFolder, "animal_*.xml"))
                {
                    Console.WriteLine($"→ {Path.GetFileName(file)}");
                }
            }
        }

        static bool SerializeAnimal(Animal animal, string fileName) //сериализация конкретного животного 
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fullPath = Path.Combine(desktopPath, "AnimalSerialization", fileName);
                
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                
                XmlSerializer serializer = new XmlSerializer(animal.GetType());
                
                using (FileStream stream = new FileStream(fullPath, FileMode.Create)) //файловый поток для записи 
                {
                    serializer.Serialize(stream, animal);
                }
                
                Console.WriteLine($"Файл создан: {fullPath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return false;
            }
        }
    }
}
