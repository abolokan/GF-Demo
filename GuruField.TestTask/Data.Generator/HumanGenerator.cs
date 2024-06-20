using Bogus;
using Domain.Humans;

namespace Data.Generator;

public class HumanGenerator
{
    public List<Human> GenerateHumans(int humanCount = 20)
    {
        var humanFaker = new Faker<Human>()
            .CustomInstantiator(f =>
            {
                return Human.Create(f.Person.FullName);
            });

        return humanFaker.Generate(humanCount);
    }
}
