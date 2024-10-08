﻿using EA_Store.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EA_Store.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; }

     
    }
}
