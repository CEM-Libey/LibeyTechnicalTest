using LibeyTechnicalTestDomain.LibeyUserAggregate.Application;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Moq;
using Xunit;

namespace LibeyTechnicalTestTest
{
    public class LibeyUserAggregateTests
    {
        private readonly Mock<ILibeyUserRepository> _mockRepository;
        private readonly LibeyUserAggregate _userAggregate;

        public LibeyUserAggregateTests()
        {
            _mockRepository = new Mock<ILibeyUserRepository>();
            _userAggregate = new LibeyUserAggregate(_mockRepository.Object);
        }

        [Fact]
        public void Create_ValidUser_ShouldCallRepositoryCreate()
        {
            // Arrange
            var command = new UserUpdateorCreateCommand
            {
                DocumentNumber = "12345678",
                DocumentTypeId = 1,
                Name = "Juan",
                FathersLastName = "Pérez",
                MothersLastName = "García",
                Address = "Av. Test 123",
                UbigeoCode = "150101",
                Phone = "987654321",
                Email = "juan@test.com",
                Password = "password123"
            };

            _mockRepository
                .Setup(r => r.ExistsByDocumentNumber(command.DocumentNumber))
                .Returns(false);

            // Act
            _userAggregate.Create(command);

            // Assert
            _mockRepository.Verify(r => r.Create(It.IsAny<LibeyUser>()), Times.Once);
        }

        [Fact]
        public void Create_ExistingUser_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var command = new UserUpdateorCreateCommand
            {
                DocumentNumber = "12345678",
                DocumentTypeId = 1,
                Name = "Juan",
                FathersLastName = "Pérez",
                MothersLastName = "García",
                Address = "Av. Test 123",
                UbigeoCode = "150101",
                Phone = "987654321",
                Email = "juan@test.com",
                Password = "password123"
            };

            _mockRepository
                .Setup(r => r.ExistsByDocumentNumber(command.DocumentNumber))
                .Returns(true);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _userAggregate.Create(command));
            Assert.Contains("already exists", exception.Message);
        }

        [Fact]
        public void Update_ExistingUser_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var command = new UserUpdateorCreateCommand
            {
                DocumentNumber = "12345678",
                DocumentTypeId = 1,
                Name = "Juan Carlos",
                FathersLastName = "Pérez",
                MothersLastName = "García",
                Address = "Av. Test 456",
                UbigeoCode = "150101",
                Phone = "987654321",
                Email = "juan.carlos@test.com",
                Password = "newpassword123"
            };

            var existingUser = new LibeyUser(
                "12345678", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            );

            _mockRepository
                .Setup(r => r.GetByDocumentNumber(command.DocumentNumber))
                .Returns(existingUser);

            // Act
            _userAggregate.Update(command);

            // Assert
            _mockRepository.Verify(r => r.Update(It.IsAny<LibeyUser>()), Times.Once);
        }

        [Fact]
        public void Update_NonExistingUser_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var command = new UserUpdateorCreateCommand
            {
                DocumentNumber = "12345678",
                DocumentTypeId = 1,
                Name = "Juan",
                FathersLastName = "Pérez",
                MothersLastName = "García",
                Address = "Av. Test 123",
                UbigeoCode = "150101",
                Phone = "987654321",
                Email = "juan@test.com",
                Password = "password123"
            };

            _mockRepository
                .Setup(r => r.GetByDocumentNumber(command.DocumentNumber))
                .Returns((LibeyUser?)null);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _userAggregate.Update(command));
            Assert.Contains("not found", exception.Message);
        }

        [Fact]
        public void Delete_ExistingUser_ShouldDeactivateUser()
        {
            // Arrange
            var documentNumber = "12345678";
            var existingUser = new LibeyUser(
                documentNumber, 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            );

            _mockRepository
                .Setup(r => r.GetByDocumentNumber(documentNumber))
                .Returns(existingUser);

            // Act
            _userAggregate.Delete(documentNumber);

            // Assert
            _mockRepository.Verify(r => r.Update(It.Is<LibeyUser>(u => !u.Active)), Times.Once);
        }

        [Fact]
        public void GetUsers_ShouldReturnRepositoryResult()
        {
            // Arrange
            var query = new UserSearchQuery { Page = 1, PageSize = 10 };
            var expectedResult = new UserListResponse
            {
                Users = new List<LibeyUserResponse>(),
                TotalCount = 0,
                Page = 1,
                PageSize = 10
            };

            _mockRepository
                .Setup(r => r.GetUsers(query))
                .Returns(expectedResult);

            // Act
            var result = _userAggregate.GetUsers(query);

            // Assert
            Assert.Equal(expectedResult, result);
            _mockRepository.Verify(r => r.GetUsers(query), Times.Once);
        }

        [Fact]
        public void GetDocumentTypes_ShouldReturnRepositoryResult()
        {
            // Arrange
            var expectedResult = new List<DocumentTypeResponse>
            {
                new DocumentTypeResponse { Id = 1, Description = "DNI" },
                new DocumentTypeResponse { Id = 2, Description = "Pasaporte" }
            };

            _mockRepository
                .Setup(r => r.GetDocumentTypes())
                .Returns(expectedResult);

            // Act
            var result = _userAggregate.GetDocumentTypes();

            // Assert
            Assert.Equal(expectedResult, result);
            _mockRepository.Verify(r => r.GetDocumentTypes(), Times.Once);
        }

        [Fact]
        public void GetUbigeos_ShouldReturnRepositoryResult()
        {
            // Arrange
            var expectedResult = new List<UbigeoResponse>
            {
                new UbigeoResponse 
                { 
                    UbigeoCode = "150101", 
                    Description = "Lima - Lima - Lima",
                    ProvinceCode = "1501",
                    RegionCode = "15",
                    ProvinceDescription = "Lima",
                    RegionDescription = "Lima"
                }
            };

            _mockRepository
                .Setup(r => r.GetUbigeos())
                .Returns(expectedResult);

            // Act
            var result = _userAggregate.GetUbigeos();

            // Assert
            Assert.Equal(expectedResult, result);
            _mockRepository.Verify(r => r.GetUbigeos(), Times.Once);
        }
    }

    public class LibeyUserDomainTests
    {
        [Fact]
        public void CreateUser_ValidData_ShouldCreateUser()
        {
            // Arrange & Act
            var user = new LibeyUser(
                "12345678", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            );

            // Assert
            Assert.Equal("12345678", user.DocumentNumber);
            Assert.Equal("Juan", user.Name);
            Assert.True(user.Active);
        }

        [Fact]
        public void CreateUser_EmptyDocumentNumber_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LibeyUser(
                "", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            ));
        }

        [Fact]
        public void CreateUser_EmptyName_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LibeyUser(
                "12345678", 1, "", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            ));
        }

        [Fact]
        public void CreateUser_InvalidEmail_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LibeyUser(
                "12345678", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "invalid-email", "password123"
            ));
        }

        [Fact]
        public void UpdateProfile_ValidData_ShouldUpdateFields()
        {
            // Arrange
            var user = new LibeyUser(
                "12345678", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            );

            // Act
            user.UpdateProfile(
                "Juan Carlos", "Pérez", "García",
                "Av. New 456", "150102", "999888777",
                "juan.carlos@test.com"
            );

            // Assert
            Assert.Equal("Juan Carlos", user.Name);
            Assert.Equal("Av. New 456", user.Address);
            Assert.Equal("999888777", user.Phone);
            Assert.Equal("juan.carlos@test.com", user.Email);
        }

        [Fact]
        public void Deactivate_ActiveUser_ShouldSetActiveToFalse()
        {
            // Arrange
            var user = new LibeyUser(
                "12345678", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            );

            // Act
            user.Deactivate();

            // Assert
            Assert.False(user.Active);
        }

        [Fact]
        public void Activate_InactiveUser_ShouldSetActiveToTrue()
        {
            // Arrange
            var user = new LibeyUser(
                "12345678", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            );
            user.Deactivate();

            // Act
            user.Activate();

            // Assert
            Assert.True(user.Active);
        }

        [Fact]
        public void ChangePassword_ValidPassword_ShouldUpdatePassword()
        {
            // Arrange
            var user = new LibeyUser(
                "12345678", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            );

            // Act
            user.ChangePassword("newpassword456");

            // Assert
            Assert.Equal("newpassword456", user.Password);
        }

        [Fact]
        public void ChangePassword_EmptyPassword_ShouldThrowArgumentException()
        {
            // Arrange
            var user = new LibeyUser(
                "12345678", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            );

            // Act & Assert
            Assert.Throws<ArgumentException>(() => user.ChangePassword(""));
        }

        [Fact]
        public void IsValidEmail_ValidEmail_ShouldReturnTrue()
        {
            // Arrange
            var user = new LibeyUser(
                "12345678", 1, "Juan", "Pérez", "García",
                "Av. Test 123", "150101", "987654321",
                "juan@test.com", "password123"
            );

            // Act & Assert
            Assert.True(user.IsValidEmail());
        }
    }
} 