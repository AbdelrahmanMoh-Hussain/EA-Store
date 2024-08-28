namespace EA_Store.Attributes
{
    public class AllowedExtensions : ValidationAttribute
    {
        private readonly string _allowedExtensions;

        public AllowedExtensions(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isVaild = _allowedExtensions
                        .Split(',')
                        .Contains(extension, StringComparer.OrdinalIgnoreCase);

                if (!isVaild)
                {
                    return new ValidationResult($"Only {_allowedExtensions} extenstions are allowed");
                }
            }

            return ValidationResult.Success;
        }
    }
}
