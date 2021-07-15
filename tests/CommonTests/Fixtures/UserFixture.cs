using Bogus;
using Domain.Entities;
using Domain.Enums;
using Xunit;

namespace CommonTests.Fixtures
{
    [CollectionDefinition(nameof(UserFixtureCollection))]
    public class UserFixtureCollection : ICollectionFixture<UserFixture>
    { }

    public class UserFixture
    {
        public User ValidUser()
        {
            var faker = new Faker<User>("pt_BR");

            faker.RuleFor(u => u.Name, (f, u) => f.Name.FullName());
            faker.RuleFor(u => u.Password, (f, u) => f.Internet.Password());
            faker.RuleFor(u => u.Email, (f, u) => f.Internet.ExampleEmail(u.Name.ToLower()));
            faker.RuleFor(u => u.Role, (f, u) => f.PickRandom<Role>());
            faker.RuleFor(u => u.Phone, (f, u) => f.Phone.PhoneNumber("(##) #####-####"));

            return faker.Generate();
        }

        public User EmptyUser()
        {
            return new User
            {
                Name = string.Empty,
                Email = string.Empty,
                Password = string.Empty,
                Phone = string.Empty,
                Photo = null
            };
        }

        public User MaxLengthUser()
        {
            const int LENGTH_FIELDS = UserValidation.MAX_LENGTH_FIELDS + 50;
            const int LENGTH_PHONE = UserValidation.LENGTH_PHONE + 10;
            var faker = new Faker<User>("pt_BR");

            faker.CustomInstantiator(f => new User
            {
                Name = f.Lorem.Letter(LENGTH_FIELDS),
                Email = f.Lorem.Letter(LENGTH_FIELDS),
                Password = f.Lorem.Letter(LENGTH_FIELDS),
                Phone = f.Lorem.Letter(LENGTH_PHONE)
            });

            return faker.Generate();
        }
    }
}
