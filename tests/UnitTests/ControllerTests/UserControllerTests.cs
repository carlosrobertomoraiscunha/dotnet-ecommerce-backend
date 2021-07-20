using AutoMapper;
using CommonTests.Fixtures;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.ViewModels.User;
using EcommerceAPI.Controllers;
using EcommerceAPI.Service.Security;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Service;
using System.IO;
using System.Net.Mime;
using Xunit;

namespace UnitTests.ControllerTests
{
    [Collection(nameof(UserFixtureCollection))]
    public class UserControllerTests : IClassFixture<UserFixture>,
                                       IClassFixture<ImageFixture>
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IMapper> _mapper;

        private UserController _userController;
        private readonly IUserService _userService;

        private readonly IConfiguration _configuration;

        private readonly UserFixture _userFixture;
        private readonly ImageFixture _imageFixture;

        private readonly User _validUser;
        private readonly UserSignUpViewModel _validUserSignUpViewModel;
        private readonly UserTokenViewModel _validUserTokenViewModel;

        public UserControllerTests(UserFixture userFixture,
                                   ImageFixture imageFixture)
        {
            _userFixture = userFixture;
            _imageFixture = imageFixture;
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                       .AddJsonFile("appsettings.json")
                                                       .Build();

            _configuration.GetSection("JwtTokenConfig")
                          .Get<JwtTokenConfig>();

            _userRepository = new Mock<IUserRepository>();
            _mapper = new Mock<IMapper>();

            _validUser = userFixture.ValidUser();
            _validUser.Photo = imageFixture.ValidImage();

            _validUserSignUpViewModel = userFixture.ValidUserSignUpViewModel();
            _validUserSignUpViewModel.Photo = imageFixture.ValidImageViewModel();

            _validUserTokenViewModel = userFixture.ValidUserTokenViewModel();
            _validUserTokenViewModel.Photo = imageFixture.ValidImageOutputViewModel();

            _mapper.Setup(m => m.Map<UserSignUpViewModel, User>(_validUserSignUpViewModel))
                   .Returns(_validUser);
            _mapper.Setup(m => m.Map<User, UserTokenViewModel>(_validUser))
                   .Returns(_validUserTokenViewModel);

            _userRepository.Setup(r => r.FindByEmail(_validUser.Email))
                           .Returns(_validUser);

            _userService = new UserService(_userRepository.Object, _mapper.Object);
        }

        [Fact]
        [Trait("UserController", "UserController_SignUp_Success")]
        public void UserController_SignUp_Success()
        {
            _userController = new UserController(_userService);

            var result = _userController.SignUp(_validUserSignUpViewModel);

            _mapper.Verify(m => m.Map<UserSignUpViewModel, User>(_validUserSignUpViewModel),
                           Times.Once);
            _mapper.Verify(m => m.Map<User, UserTokenViewModel>(_validUser),
                           Times.Once);

            result.Should().BeOfType<CreatedResult>();

            ((CreatedResult)result).Value.Should().BeOfType<UserTokenViewModel>();
        }
    }
}
