using IntegraXL.Core;

namespace IntegraXL.Interfaces
{
    /// <summary>
    /// Provides a contract for models containing an indexed parameter array.
    /// </summary>
    public interface IParameterProvider<T>
    {
        T this[int index] { get; set; }
        IntegraParameter<T>? Parameter { get; set; }
    }
}
