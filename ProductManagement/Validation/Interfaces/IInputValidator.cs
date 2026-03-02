namespace ProductManagement.Validation.Interfaces
{
    public interface IInputValidator
    {
        public abstract bool TryReadRequired(string prompt, out string input);
        public abstract bool TryReadDecimal(string prompt, out decimal value);
        public abstract bool TryReadInt(string prompt, out int value);
    }
}


