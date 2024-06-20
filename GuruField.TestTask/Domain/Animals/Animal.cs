using Domain.Humans;

namespace Domain.Animals;

public class Animal : Entity
{
    internal Animal(Guid id, string name)
    {
        Name = name;
    }

    private Animal() { }

    private readonly List<Human> _humans = new();
    public string Name { get; private set; }

    public IReadOnlyList<Human> Humans => _humans;

    public static Animal Create(string name)
    {
        var animal = new Animal(Guid.NewGuid(), name);

        return animal;
    }
}
