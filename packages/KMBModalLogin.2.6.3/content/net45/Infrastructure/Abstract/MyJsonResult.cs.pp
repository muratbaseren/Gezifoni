namespace $rootnamespace$.Infrastructure.Abstract
{
    public class MyJsonResult<T>
    {
        public bool HasError { get; set; }
        public T Result { get; set; }
		
		public MyJsonResult()
        {
        }

        public MyJsonResult(bool hasError, T resut)
        {
            HasError = hasError;
            Result = resut; 
        }
    }
}