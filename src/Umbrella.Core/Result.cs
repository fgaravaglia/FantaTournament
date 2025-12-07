namespace Umbrella.Core;

///<summary>
/// simple class to map a result (Success, Failed)
public class Result<T>
{
    /// <summary>
    /// Data of the Result
    /// </summary>
    public T? Data { get; set; }
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="succeeded"></param>
    /// <param name="errors"></param> <summary>
    /// <param name="data"></param>
    internal Result(bool succeeded, IEnumerable<string> errors, T? data)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Data = data;
    }
    /// <summary>
    /// True if the operation succeeded
    /// </summary>
    public bool Succeeded { get; init; }
    /// <summary>
    /// List of errors if the operation failed      
    /// </summary>
    public string[] Errors { get; init; }
    /// <summary>
    /// Creates a Success Result    
    /// </summary>
    public static Result<T> Success(T data)
    {
        ArgumentNullException.ThrowIfNull(data);
        return new Result<T>(true, Array.Empty<string>(), data);
    }
    /// <summary>
    /// Creates a NotFound Result    
    /// </summary>
    public static Result<T> NotFound()
    {
        return new Result<T>(false, ["Not Found"], default(T));
    }
    /// <summary>
    /// Creates a Failure Result
    /// </summary>
    public static Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(false, errors, default(T));
    }
}
