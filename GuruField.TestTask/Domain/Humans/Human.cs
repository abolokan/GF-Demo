using Domain.Animals;

namespace Domain.Humans;

public class Human : Entity
{
    internal Human(Guid id, string name) : base(id)
    {
        Name = name;
    }

    private Human() { }
    public string Name { get; private set; }

    public Guid? MostFavoriteAnimalId { get; set; }
    public Animal MostFavoriteAnimal { get; set; } = default!;

    public Guid? LeastFavoriteAnimalId { get; set; }
    public Animal LeastFavoriteAnimal { get; set; } = default!;

    public static Human Create(string name)
    {
        var person = new Human(Guid.NewGuid(), name);
        person.Raise(new PersonCreatedDomainEvent(person.Id));
        return person;
    }

    public void SetMostFavoriteAnimalId(Guid id)
    {
        MostFavoriteAnimalId = id;
    }

    public void SetLeastFavoriteAnimalId(Guid id)
    {
        LeastFavoriteAnimalId = id;
    }
}
