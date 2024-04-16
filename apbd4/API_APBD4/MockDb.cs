namespace API_APBD4;

public interface IMockDb
{
    //Animals
    public ICollection<Animal> GetAllAnimals();
    public Animal? GetAnimalDetails(int id);
    public bool AddAnimal(Animal animal);
    public Animal? RemoveAnimal(int id);
    
    //Visits
    public ICollection<Visit> GetVisitsForAnimal(int animalId);
    public bool AddVisit(Visit visit);
}

public class MockDb : IMockDb
{
    private ICollection<Animal> _animals;
    private ICollection<Visit> _visits;
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

        _visits = new List<Visit>
        {
            new Visit
            {
                Id = 1,
                AnimalId = 1,
                VisitDate = DateTime.Now.AddDays(-10), 
                Description = "Badanie rutynowe",
                Price = 42.0 
            },
            new Visit
            {
                Id = 2,
                AnimalId = 1,
                VisitDate = DateTime.Now.AddDays(-5),
                Description = "Szczepienie przeciwko wsciekliznie",
                Price = 21.37
            },
            new Visit
            {
                Id = 3,
                AnimalId = 2,
                VisitDate = DateTime.Now.AddDays(-15),
                Description = "Zabieg kastracji",
                Price = 6.9
            }
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

    public ICollection<Visit> GetVisitsForAnimal(int animalId)
    {
        return _visits.Where(v => v.AnimalId == animalId).ToList();
    }

    public bool AddVisit(Visit visit)
    {
        _visits.Add(visit);
        return true;
    }
}