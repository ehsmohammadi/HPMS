namespace MITD.PMS.Integration.Domain
{
    public class Period
    {
        public Period(long id,string name,PeriodState state)
        {
            Id = id;
            Name = name;
            State = state;
        }
        public long Id { get; set; }

        public string Name { get; set; }

        public PeriodState State { get; set; }

    }
}
