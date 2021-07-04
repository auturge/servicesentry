namespace ServiceSentry.Extensibility.Extensions
{
    public abstract class ImportedExtension
    {
        public string ExtensionName { get; set; }
        public bool CanExecute { get; set; }
    }
}