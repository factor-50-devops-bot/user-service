﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UserService.Core.Contracts
{
    [DataContract(Name = "getHelperCoordsByPostcodeAndRadiusResponse")]
    public class GetHelperCoordsByPostcodeAndRadiusResponse
    {
        [DataMember(Name = "coordinates")]
        public IReadOnlyList<VolunteerCoordinate> Coordinates { get; set; }
    }
}
