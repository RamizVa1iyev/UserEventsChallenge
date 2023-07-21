using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserEventsChallenge.API.Entities.Exceptions;

public class InternalExceptionDetails: ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}
