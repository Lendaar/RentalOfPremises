namespace RentalOfPremises.Common.Entity.InterfaceDB
{
    public interface IIdentityProvider
    {
        public Guid Id { get; }

        public string Name { get; }

        public IEnumerable<KeyValuePair<string, string>> Claims { get; }
    }
}
