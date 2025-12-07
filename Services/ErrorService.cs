namespace excel_workflow.Services
{
    public class ErrorService
    {
        public event Action<string?>? OnErrorChanged;

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            private set
            {
                _errorMessage = value;
                OnErrorChanged?.Invoke(value);
            }
        }

        public void ReportError(Exception ex, string userMessage = "An unexpected error occurred.")
        {
            //Console.WriteLine($"{ex.GetType().Name}: {ex.Message.Split('\n')[0]}");
            Console.WriteLine(ex.ToString());
            ErrorMessage = userMessage;
        }

        public void ClearError() => ErrorMessage = null;
    }

}
