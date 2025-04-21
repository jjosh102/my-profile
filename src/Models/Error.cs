namespace MyProfile.Models;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error EmptyValue = new(nameof(Error), "Result is empty.");
    public static Error HttpError(string code) => new($"HttpError - {code}", "Result encountered a Http error");
    public Error(string code, string messaage)
    {
        Code = code;
        Message = messaage;
    }

    public string Code { get; }

    public string Message { get; }

    public bool Equals(Error? other)
    {
        return other is not null && other.GetType() == GetType();
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && obj.GetType() == GetType() && obj is Error;
    }

    public static bool operator ==(Error? first, Error? second)
         => first is not null && second is not null && first.Equals(second);

    public static bool operator !=(Error? first, Error? second)
         => !(first == second);

    public static implicit operator string(Error error)
         => error.Message;

    public override int GetHashCode()
    {
        return string.IsNullOrEmpty(Code) ? 0 : Code.GetHashCode();
    }
}