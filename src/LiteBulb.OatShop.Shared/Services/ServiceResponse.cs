namespace LiteBulb.OatShop.Shared.Services;
/// <summary>
/// The response object from a service (for void return type service methods).
/// </summary>
public class ServiceResponse
{
    /// <summary>
    /// Whether any errors occurred during the service operation.
    /// </summary>
    public bool HasErrors { get; private set; }

    /// <summary>
    /// The message describing the error.
    /// </summary>
    public string ErrorMessage { get; private set; }

    /// <summary>
    /// The exception that was thrown for some errors.
    /// </summary>
    public Exception? Exception { get; private set; }


    /// <summary>
    /// Creates an unsuccessful (error) response.
    /// Note: Result property will be null.
    /// </summary>
    /// <param name="hasErrors">Boolean says that service resulted in an error</param>
    /// <param name="errorMessage">The message describing the error</param>
    /// <param name="exception">Exception object</param>
    public ServiceResponse(bool hasErrors, string errorMessage, Exception? exception = null)
    {
        HasErrors = hasErrors;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    /// <summary>
    /// Creates a successful response.
    /// </summary>
    public ServiceResponse()
    {
        HasErrors = false;
        ErrorMessage = string.Empty;
    }
}

/// <summary>
/// The response object from a service (generic version which returns a result object of type T).
/// </summary>
/// <typeparam name="T">Generic type for result object class</typeparam>
public class ServiceResponse<T> : ServiceResponse
{
    /// <summary>
    /// The object or POCO to be returned by the service if successful.
    /// </summary>
    public T? Result { get; private set; }

    /// <summary>
    /// Creates an unsuccessful (error) response.
    /// Note: Result property will be null.
    /// </summary>
    /// <param name="hasErrors">Boolean says that service resulted in an error</param>
    /// <param name="errorMessage">The message describing the error</param>
    /// <param name="exception">Exception objet</param>
    public ServiceResponse(bool hasErrors, string errorMessage, Exception? exception = null) : base(hasErrors, errorMessage, exception)
    {
    }

    /// <summary>
    /// Creates a successful response.
    /// </summary>
    /// <param name="result">Payload object returned by the service (Note: this response has no errors)</param>
    public ServiceResponse(T result) : base()
    {
        Result = result;
    }
}
