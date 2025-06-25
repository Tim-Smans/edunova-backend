using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Core.DTO.Tag
{
    public class CreateTagDTO
    {
        [
            Required(ErrorMessage = "A tag title is required"),
            MinLength(3, ErrorMessage = "A tag title needs to be at least 3 characters long"),
        ]
        public string Title { get; set; }
    }
}
