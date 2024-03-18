namespace BankingApp.Extensions.ApiResponse
{
    public interface IResponseFactory
    {
        IResponse<object> GenericSuccessResponse { get; }
        IResponse<object> GenericErrorResponse { get; }
        IResponse<object> CreateErrorResponse(string errorCode, string errorTitle, string errorMessage);
        IResponse<object> CreateErrorResponse(List<Error> errorMessages);
        IResponse<object> CreateErrorResponse(PredefinedError error);
        IResponse<object> CreateErrorResponse(List<PredefinedError> errorMessages);
        IResponse<object> CreateErrorResponse(PredefinedError error, string detail);
    }

    public class ResponseFactory : IResponseFactory
    {

        public IResponse<object> GenericSuccessResponse => new Response<object>
        {
        };

        public IResponse<object> GenericErrorResponse => new Response<object>
        {
            Errors = new List<Error>()
        };

        public IResponse<object> CreateErrorResponse(PredefinedError error)
        {
            return CreateErrorResponse(error.Code, error.Title, error.Detail);
        }
        public IResponse<object> CreateErrorResponse(PredefinedError error, string detail)
        {
            return CreateErrorResponse(error.Code, error.Title, detail);
        }
        public IResponse<object> CreateErrorResponse(List<PredefinedError> errors)
        {
            return CreateErrorResponse(errors.Select(e => new Error
            {
                Code = e.Code,
                Title = e.Title,
                Detail = e.Detail
            }).ToList());
        }

        public IResponse<object> CreateErrorResponse(string errorCode, string errorTitle, string errorMessage)
        {
            return new Response<object>
            {
                Errors = new List<Error>()
                {
                    new()
                    {
                        Code = errorCode,
                        Detail = errorMessage,
                        Title = errorTitle
                    }
                }
            };
        }

        public IResponse<object> CreateErrorResponse(List<Error> errorMessages)
        {
            return new Response<object>
            {
                Errors = errorMessages
            };
        }
    }
}
