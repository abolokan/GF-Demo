using Domain.Humans;

namespace Domain.Animals;

public class Animal : Entity
{
    internal Animal(Guid id, string name) : base(id)
    {
        Name = name;
    }

    private Animal() { }

    private readonly List<Human> _humansWithMostFavoritAnimals = new();
    private readonly List<Human> _humansWithLeastFavoritAnimals = new();

    private readonly List<Predator> _predators = new();
    private readonly List<Predator> _preys = new();

    public string Name { get; private set; }

    public IReadOnlyList<Human> HumansWithMostFavoritAnimals => _humansWithMostFavoritAnimals;
    public IReadOnlyList<Human> HumansWithLeastFavoritAnimals => _humansWithLeastFavoritAnimals;

    public IReadOnlyList<Predator> Predators => _predators;
    public IReadOnlyList<Predator> Preys => _preys;

    public static Animal Create(string name)
    {
        var animal = new Animal(Guid.NewGuid(), name);

        return animal;
    }
}
