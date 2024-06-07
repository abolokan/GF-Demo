using Bogus;
using Domain.Companies;
using Domain.Contracts;

namespace Data.Generator;

public class ContractDataGenerator
{
    public List<Company> GenerateCompanies(HashSet<string> usedSuffixes, int companyCount = 5)
    {
        var companyFaker = new Faker<Company>()
        .CustomInstantiator(f =>
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();

            string suffix;
            do
            {
                suffix = new string(Enumerable.Repeat(chars, 3)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            } while (!usedSuffixes.Add(suffix));

            return Company.Create(suffix, f.Company.CompanyName());
        });

        return companyFaker.Generate(companyCount);
    }

    public List<Contract> GenerateContracts(int fromYear, int toYear, List<Company> providers, List<Company> clients)
    {
        DateTime fromStart = new DateTime(fromYear, 1, 1); // 2020
        DateTime fromEnd = new DateTime(toYear, 1, 1); // 2024

        var contractFaker = new Faker<Contract>()
                .CustomInstantiator((f) =>
                {
                    var client = f.PickRandom(providers);
                    var provider = f.PickRandom(clients);

                    var activeFrom = DateOnly.FromDateTime(f.Date.Between(fromStart, fromEnd));
                    var activeTo = activeFrom.AddDays(new Random().Next(10, 365 * 3));
                    if (activeTo > DateOnly.FromDateTime(fromEnd))
                    {
                        activeTo = DateOnly.FromDateTime(fromEnd);
                    }

                    return Contract.Create(
                        client.Id,
                        provider.Id,
                        f.Company.CatchPhrase(),
                        activeFrom,
                        f.Random.Bool() ? activeTo : null
                    );
                })
                .RuleFor(c => c.State, f => f.PickRandom<ContractState>());

        return contractFaker.GenerateBetween(1, 2);
    }

    public List<Agreement> GenerateAgreements(List<Contract> contracts)
    {
        var result = new List<Agreement>();

        var currency = "USD";

        foreach (var contract in contracts)
        {
            DateOnly startDate = contract.ActiveFrom;

            var endDate = contract.ActiveTo ?? DateOnly.FromDateTime(DateTime.UtcNow);
            int max = 0;
            do
            {
                var agreementFaker = new Faker<Agreement>()
                 .CustomInstantiator((f) =>
                 {
                     var hourlyPrice = f.Finance.Amount(50, 150);

                     var agreement = Agreement.Create(contract.Id, hourlyPrice, currency, startDate);

                     startDate = DateOnly.FromDateTime(f.Date.Between(
                         startDate.AddDays(1).ToDateTime(TimeOnly.MinValue),
                         endDate.ToDateTime(TimeOnly.MinValue)));

                     return agreement;
                 });

                max++;
                result.AddRange(agreementFaker.Generate(1));

            } while (startDate < endDate && max <= 3);
        }

        return result;
    }

    public List<WorkHour> GenerateWorkHours(List<Agreement> agreements)
    {
        var result = new List<WorkHour>();

        var groped = agreements.GroupBy(x => x.Contract).ToList();

        foreach (var group in groped)
        {
            var aSorted = group.OrderBy(a => a.StartDate).ToList();

            var random = new Random();

            for (int i = 0; i < aSorted.Count; i++)
            {
                var currentAgreement = aSorted[i];
                var currentStartDate = currentAgreement.StartDate; // 05.01.2021
                var nextStartDate = (i + 1 < aSorted.Count) ?
                    aSorted[i + 1].StartDate :
                    DateOnly.FromDateTime(group.Key.ActiveTo.HasValue ? group.Key.ActiveTo.Value.ToDateTime(TimeOnly.MinValue) : DateTime.UtcNow);

                var dates = GetDatesBetween(currentStartDate, nextStartDate);

                foreach (var date in dates)
                {
                    int currentYear = date.Year;
                    int currentMonth = date.Month;

                    var workHour = WorkHour.Create(currentAgreement.Id, random.Next(8, 24), currentYear, currentMonth);
                    result.Add(workHour);
                }
            }
        }

        return result;
    }

    public static List<DateOnly> GetDatesBetween(DateOnly startDate, DateOnly endDate)
    {
        var months = new List<DateOnly>();

        int currentYear = startDate.Year;
        int currentMonth = startDate.Month;

        while (new DateOnly(currentYear, currentMonth, 1) < endDate)
        {
            months.Add(new DateOnly(currentYear, currentMonth, 1));

            currentMonth++;
            if (currentMonth > 12)
            {
                currentMonth = 1;
                currentYear++;
            }
        }

        return months;
    }



    //public List<WorkHour> GenerateWorkHours(List<Agreement> agreements)
    //{
    //    var aSorted = agreements.OrderBy(a => a.StartDate).ToList();
    //    var result = new List<WorkHour>();

    //    var count = aSorted.Count;

    //    if (count > 0)
    //    {
    //        var year = aSorted[0].StartDate.Year;
    //        var month = aSorted[0].StartDate.Month;
    //        var wh = WorkHour.Create(aSorted[0].Id, new Random().Next(8, 24), year, month);
    //        result.Add(wh);

    //        if (count > 1)
    //        {
    //            year = aSorted[1].StartDate.Year;
    //            month = aSorted[1].StartDate.Month;
    //            var wh2 = WorkHour.Create(aSorted[1].Id, new Random().Next(8, 24), year, month);

    //            result.Add(wh2);
    //        }

    //    }

    //    return result;
    //}
}
