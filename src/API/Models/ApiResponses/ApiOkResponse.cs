using System.Net;

namespace API.Models.ApiResponses
{
    public class ApiOkResponse : ApiResponse
    {
        public object Result { get; }

        public ApiOkResponse(object result)
            :base((int)HttpStatusCode.OK)
        {
            Result = result;
        }
    }
}