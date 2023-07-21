﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserEventsChallenge.API.Entities.Exceptions;

public class AuthorizationProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}