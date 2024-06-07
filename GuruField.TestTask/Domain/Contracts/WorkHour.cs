namespace Domain.Contracts;

public class WorkHour : Entity
{
    internal WorkHour(Guid id, Guid agreementId, int hours, int year, int month) : base(id)
    {
        AgreementId = agreementId;
        Hours = hours;
        Year = year;
        Month = month;
    }

    private WorkHour() { }
    public int Hours { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }
    public Guid AgreementId { get; private set; }
    public Agreement Agreement { get; private set; }


    public static WorkHour Create(Guid agreementId, int hours, int year, int month)
    {
        var wh = new WorkHour(Guid.NewGuid(), agreementId, hours, year, month);

        wh.Raise(new WorkHourCreatedDomainEvent(wh.Id));

        return wh;
    }
}