namespace MyProfile.Shared;

 public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public bool IsFailure => !IsSuccess;

        public Result()
        {
            IsSuccess = true;
            Error = string.Empty;
        }
        public Result(bool isSuccess, string error)
        {
            if (isSuccess && error != string.Empty)
                throw new InvalidOperationException();

            if (!isSuccess && error == string.Empty)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Fail(string error)
        {
            return new Result(false, error);
        }

        public static Result<T> Fail<T>(string error)
        {
            return new Result<T>(default(T), false, error);
        }

        public static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }
      
    }

    public class Result<TValue> : Result
    {   
        private readonly TValue? _value;
        public Result(TValue? value, bool isSuccess, string error) : base(isSuccess, error) => _value = value;

        public TValue? Value => _value!;

        public static implicit operator Result<TValue>(TValue value) => new(value,true,string.Empty);
       
    }