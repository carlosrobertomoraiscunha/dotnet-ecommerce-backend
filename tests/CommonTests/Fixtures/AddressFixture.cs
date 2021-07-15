using Bogus;
using Domain.Entities;
using Xunit;

namespace CommonTests.Fixtures
{
    [CollectionDefinition(nameof(AddressFixtureCollection))]
    public class AddressFixtureCollection : ICollectionFixture<AddressFixture> { }

    public class AddressFixture
    {
        public Address ValidAddress()
        {
            var faker = new Faker<Address>("pt_BR");

            faker.RuleFor(a => a.Cep, (f, a) => f.Address.ZipCode());
            faker.RuleFor(a => a.City, (f, a) => f.Address.City());
            faker.RuleFor(a => a.Complement, (f, a) => f.Address.SecondaryAddress());
            faker.RuleFor(a => a.District, (f, a) => f.Address.StreetAddress());
            faker.RuleFor(a => a.Number, (f, a) => f.Address.BuildingNumber());
            faker.RuleFor(a => a.State, (f, a) => f.Address.State());
            faker.RuleFor(a => a.Street, (f, a) => f.Address.StreetName());

            return faker.Generate();
        }

        public Address EmptyAddress()
        {
            return new Address
            {
                Cep = string.Empty,
                City = string.Empty,
                Complement = string.Empty,
                District = string.Empty,
                Number = string.Empty,
                State = string.Empty,
                Street = string.Empty
            };
        }

        public Address MaxLengthAddress()
        {
            const int LENGTH_FIELDS = AddressValidation.MAX_LENGTH_FIELDS + 50;
            var faker = new Faker<Address>("pt_BR");

            faker.CustomInstantiator(f => new Address
            {
                Cep = f.Lorem.Letter(LENGTH_FIELDS),
                City = f.Lorem.Letter(LENGTH_FIELDS),
                Complement = f.Lorem.Letter(LENGTH_FIELDS),
                District = f.Lorem.Letter(LENGTH_FIELDS),
                Number = f.Lorem.Letter(LENGTH_FIELDS),
                State = f.Lorem.Letter(LENGTH_FIELDS),
                Street = f.Lorem.Letter(LENGTH_FIELDS)
            });

            return faker.Generate();
        }
    }
}
