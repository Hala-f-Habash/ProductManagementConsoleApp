namespace ProductManagement.Validation.Interfaces
{
    public interface IInputValidator
    {
        public abstract string ReadRequired(string prompt);
        public abstract string ReadOptional(string prompt);
        public abstract decimal ReadDecimal(string prompt);
        public abstract int ReadInt(string prompt);
    }
}


