using System;
using System.IO;
using System.Xml.Serialization;
using AnimalLibrary;
namespace DeserializationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = { "animal_1.xml", "animal_2.xml", "animal_3.xml" };
            
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    Animal animal = DeserializeAnimal(file);
                    if (animal != null)
                    {
                        Console.WriteLine($"Десериализован: {animal}");
                        Console.WriteLine("Демонстрация методов:");
                        animal.SayHello();
                        Console.WriteLine($"Классификация: {animal.GetClassificationAnimal()}");
                        Console.WriteLine($"Любимая еда: {animal.GetFavouriteFood()}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Файл {file} не найден!");
                }
            }
        }

        static Animal DeserializeAnimal(string fileName)
        {
            try
            {
                Type[] animalTypes = { typeof(Cow), typeof(Lion), typeof(Pig) };
                
                foreach (Type type in animalTypes)
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(type);
                        
                        using (FileStream stream = new FileStream(fileName, FileMode.Open))
                        {
                            Animal animal = (Animal)serializer.Deserialize(stream);
                            Console.WriteLine($"Успешно десериализован {type.Name} из {fileName}");
                            return animal;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                
                Console.WriteLine($"Не удалось десериализовать файл {fileName}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка десериализации {fileName}: {ex.Message}");
                return null;
            }
        }
    }
}
