using System;
using System.Xml.Serialization;

namespace AnimalLibrary
{
    [Serializable]
    [XmlInclude(typeof(Cow))]
    [XmlInclude(typeof(Lion))]
    [XmlInclude(typeof(Pig))]
    [Comment("Абстрактный базовый класс для всех животных")]
    public abstract class Animal
    {
        public string Country { get; set; }
        public bool HideFromOtherAnimals { get; set; }
        public string Name { get; set; }
        
        public string WhatAnimal { get; set; }

        protected Animal(string country, bool hideFromOtherAnimals, string name, string whatAnimal)
        {
            Country = country;
            HideFromOtherAnimals = hideFromOtherAnimals;
            Name = name;
            WhatAnimal = whatAnimal;
        }
        
        protected Animal() 
        {
            WhatAnimal = "Animal";
        }

        public void Deconstruct(out string country, out bool hideFromOtherAnimals, out string name, out string whatAnimal)
        {
            country = Country;
            hideFromOtherAnimals = HideFromOtherAnimals;
            name = Name;
            whatAnimal = WhatAnimal;
        }

        public abstract eClassificationAnimal GetClassificationAnimal();
        
        public abstract eFavoriteFood GetFavouriteFood();
        
        public abstract void SayHello();

        public override string ToString()
        {
            return $"{WhatAnimal} '{Name}' из {Country}, " +
                   $"Прячется: {HideFromOtherAnimals}, " +
                   $"Классификация: {GetClassificationAnimal()}, " +
                   $"Любимая еда: {GetFavouriteFood()}";
        }
    }
}
