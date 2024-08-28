namespace EA_Store.Attributes
{
    public class MaxFileSize : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSize(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
              

                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"File can't be more than {_maxFileSize} bytes");
                }
            }

            return ValidationResult.Success;
        }
    }
}
