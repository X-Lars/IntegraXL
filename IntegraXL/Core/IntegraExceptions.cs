namespace IntegraXL.Core
{
    /// <summary>
    /// Base exception for exceptions raised from within the <see cref="IntegraXL"/> namespace.
    /// </summary>
    public class IntegraException : Exception
    {
        public IntegraException(string message) : base(message) { }
        public IntegraException(string message, Exception innerException) : base(message, innerException) { }
    }
}
