using System.Net;

namespace AuthenticationAuthorization.Exceptions;

public class HttpResponseException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public string ErrorMessage { get; set; }

    public HttpResponseException(HttpStatusCode statusCode, string errorMessage = "Bad Request")
    {
        (HttpStatusCode, ErrorMessage) = (statusCode, errorMessage);
    }
}