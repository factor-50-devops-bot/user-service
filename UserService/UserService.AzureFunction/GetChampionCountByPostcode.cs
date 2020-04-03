﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using UserService.Core.Domains.Entities;

namespace UserService.AzureFunction
{
    public class GetChampionCountByPostcode
    {
        private readonly IMediator _mediator;

        public GetChampionCountByPostcode(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetChampionCountByPostcode")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] GetChampionCountByPostcodeRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                GetChampionCountByPostcodeResponse response = await _mediator.Send(req);
                return new OkObjectResult(response);
            }
            catch (Exception exc)
            {
                log.LogInformation(exc.ToString());
                return new BadRequestObjectResult(exc);
            }
        }
    }
}