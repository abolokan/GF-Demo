namespace Domain.Animals;

public class Predator : Entity
{
    internal Predator(Guid id, Guid predatorId, Guid preyId) : base(id)
    {
        PredatorId = predatorId;
        FavoritePreyId = preyId;
    }

    private Predator() { }

    public Guid PredatorId { get; private set; }
    public Animal PredatorAnimal { get; private set; } = default!;

    public Guid FavoritePreyId { get; private set; }
    public Animal FavoritePrey { get; private set; } = default!;

    public static Predator Create(Guid predatorId, Guid preyId)
    {
        var predator = new Predator(Guid.NewGuid(), predatorId, preyId);

        return predator;
    }
}
