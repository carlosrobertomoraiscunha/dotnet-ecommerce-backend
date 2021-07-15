using CommonTests.Fixtures;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace UnitTests.DomainTests
{
    [Collection(nameof(AddressFixtureCollection))]
    public class AddressTest : IClassFixture<AddressFixture>,
                                IClassFixture<UserFixture>,
                                IClassFixture<ImageFixture>
    {
        private readonly AddressFixture _addressFixture;
        private readonly UserFixture _userFixture;
        private readonly ImageFixture _imageFixture;

        private readonly User _validUser;

        public AddressTest(AddressFixture addressFixture,
                           UserFixture userFixture,
                           ImageFixture imageFixture)
        {
            _addressFixture = addressFixture;
            _userFixture = userFixture;
            _imageFixture = imageFixture;

            _validUser = userFixture.ValidUser();
            _validUser.Photo = imageFixture.ValidImage();
        }

        [Fact]
        [Trait("Address", "Address_Okay_ValidAddress")]
        public void Address_Okay_ValidAddress()
        {
            var address = _addressFixture.ValidAddress();
            address.User = _validUser;

            var isValid = address.IsValid();

            isValid.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            address.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("Address", "Address_Empty_InvalidAddress")]
        public void Address_Empty_InvalidAddress()
        {
            var address = _addressFixture.EmptyAddress();
            address.User = _validUser;

            var isValid = address.IsValid();

            isValid.Should().BeFalse(because: "deve haver erros na validação dos campos");
            address.ErrorMessages.Should().HaveCount(6, because: "os 6 campos obrigatórios informados estão inválidos");
        }

        [Fact]
        [Trait("Address", "Address_MaxLength_InvalidAddress")]
        public void Address_MaxLength_InvalidAddress()
        {
            var address = _addressFixture.MaxLengthAddress();
            address.User = _validUser;

            var isValid = address.IsValid();

            isValid.Should().BeFalse(because: "tamanho máximo dos campos atingidos");
            address.ErrorMessages.Should().HaveCount(7, because: "o preenchimento de 7 campos ultrapassou o tamanho máximo permitido");
        }
    }
}
