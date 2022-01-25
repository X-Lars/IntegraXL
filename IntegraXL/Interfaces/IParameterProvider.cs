using IntegraXL.Core;

namespace IntegraXL.Interfaces
{
    /// <summary>
    /// Provides a contract for models containing an indexed parameter array.
    /// </summary>
    public interface IParameterProvider
    {
        int this[int index] { get; set; }
        IntegraParameter Parameter { get; set; }
    }
}
