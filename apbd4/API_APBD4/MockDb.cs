namespace API_APBD4;

public interface IMockDb
{
    public ICollection<Animal> GetAllAnimals();
    public Animal? GetAnimalDetails(int id);
    public bool AddAnimal(Animal animal);
    public Animal? RemoveAnimal(int id);
}

public class MockDb : IMockDb
{
    private ICollection<Animal> _animals;

    public MockDb()
    {
        _animals = new List<Animal>
        {
            new Animal
            {
                Id = 1,
                Name = "mariusz",
                Category = "pies",
                Weight = 20.0,
                CoatColor = "Black"
            },
            new Animal
            {
                Id = 2,
                Name = "zdzislaw",
                Category = "kot",
                Weight = 15.0,
                CoatColor = "Gray"
            },

        };
    }

    public ICollection<Animal> GetAllAnimals()
    {
        return _animals;
    }

    public Animal? GetAnimalDetails(int id)
    {
        return _animals.FirstOrDefault(e => e.Id == id);
    }

    public bool AddAnimal(Animal animal)
    {
        _animals.Add(animal);
        return true;
    }

    public Animal? RemoveAnimal(int id)
    {
        var animalToRemove = _animals.FirstOrDefault(e => e.Id == id);
        if (animalToRemove is null)
        {
            return null;
        }

        _animals.Remove(animalToRemove);
        return animalToRemove;


    }
}