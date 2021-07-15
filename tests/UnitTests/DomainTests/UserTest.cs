using CommonTests.Fixtures;
using FluentAssertions;
using Xunit;

namespace UnitTests.DomainTests
{
    [Collection(nameof(UserFixtureCollection))]
    public class UserTest : IClassFixture<UserFixture>,
                            IClassFixture<ImageFixture>
    {
        private readonly UserFixture _userFixture;
        private readonly ImageFixture _imageFixture;

        public UserTest(UserFixture userFixture, ImageFixture imageFixture)
        {
            _userFixture = userFixture;
            _imageFixture = imageFixture;
        }

        [Fact]
        [Trait("User", "User_Okay_ValidUser")]
        public void User_Okay_ValidUser()
        {
            var user = _userFixture.ValidUser();
            user.Photo = _imageFixture.ValidImage();

            var isValid = user.IsValid();

            isValid.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            user.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("User", "User_Empty_InvalidUser")]
        public void User_Empty_InvalidUser()
        {
            var user = _userFixture.EmptyUser();

            var isValid = user.IsValid();

            isValid.Should().BeFalse(because: "deve haver erros na validação dos campos");
            user.ErrorMessages.Should().HaveCount(6, because: "os 6 campos obrigatórios informados estão inválidos");
        }

        [Fact]
        [Trait("User", "User_MaxLength_InvalidUser")]
        public void User_MaxLength_InvalidUser()
        {
            var user = _userFixture.MaxLengthUser();
            user.Photo = _imageFixture.ValidImage();

            var isValid = user.IsValid();

            isValid.Should().BeFalse(because: "tamanho máximo dos campos atingidos");
            user.ErrorMessages.Should().HaveCount(5, because: "o preenchimento de 5 campos ultrapassou o tamanho máximo permitido ou é inválido");
        }
    }
}
