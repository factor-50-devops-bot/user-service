﻿using HelpMyStreet.Contracts.AddressService.Request;
using HelpMyStreet.Contracts.AddressService.Response;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserService.Core;
using UserService.Core.Domains.Entities;
using UserService.Core.Dto;
using UserService.Core.Interfaces.Repositories;
using UserService.Core.Interfaces.Services;
using UserService.Core.Services;
using UserService.Core.Utils;
using UserService.Handlers;

namespace UserService.UnitTests
{
    public class GetHelpersByPostcodeHandlerTests
    {                        
        private Mock<IHelperService> _helperService;
        private Mock<IRepository> _repository;
                
        private IEnumerable<HelperWithinRadiusDTO> _helpers;
        private IEnumerable<User> _users;

        [SetUp]
        public void SetUp()
        {
            _helpers = new List<HelperWithinRadiusDTO>
            {
                new HelperWithinRadiusDTO {
                    User =    new User
                {
                    ID = 1,
                    UserPersonalDetails = new UserPersonalDetails
                    {
                        DisplayName = "Test",
                        EmailAddress = "test@test.com"
                    },
                    SupportActivities = new List<HelpMyStreet.Utils.Enums.SupportActivities>{HelpMyStreet.Utils.Enums.SupportActivities.Shopping},
                    ChampionPostcodes= new List<string>{ "NG1 1AE" }

                },
                    Distance = 1,
                }
            };

            _repository = new Mock<IRepository>();

            _helperService = new Mock<IHelperService>();
            _helperService.Setup(x => x.GetHelpersWithinRadius(It.IsAny<string>(), It.IsAny<IsVerifiedType>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _helpers);
            _users = new List<User>()
            {
                new User()
                {
                    ID = 1
                }
            };
         
            _repository.Setup(x => x.GetVolunteersByIdsAsync(It.IsAny<IEnumerable<int>>())).ReturnsAsync(_users);
        }

        [Test]
        public async Task GetAllVolunteersWithinRadius()
        {

            GetHelpersByPostcodeRequest request = new GetHelpersByPostcodeRequest()
            {
                Postcode = "NG1 1AE"
            };

            GetHelpersByPostcodeHandler getHelpersByPostcodeHandler = new GetHelpersByPostcodeHandler(_helperService.Object, _repository.Object);

            GetHelpersByPostcodeResponse result = await getHelpersByPostcodeHandler.Handle(request, CancellationToken.None);

            Assert.AreEqual(1, result.Users.Count);
            Assert.AreEqual(1, result.Users.FirstOrDefault().ID);
            _helperService.Verify(X => X.GetHelpersWithinRadius("NG1 1AE", IsVerifiedType.IsVerified, It.IsAny<CancellationToken>()));
                                    
        }
    }
}
