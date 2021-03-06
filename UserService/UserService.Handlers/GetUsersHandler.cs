﻿using UserService.Core.Domains.Entities;
using UserService.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HelpMyStreet.Contracts.UserService.Response;

namespace UserService.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IRepository _repository;

        public GetUsersHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            List<UserDetails> userDetails = await _repository.GetUserDetailsAsync(cancellationToken);

            return new GetUsersResponse()
            {
                UserDetails = userDetails
            };
        }
    }
}
