using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Common.DTOs
{
    public class CreateMakeDTO
    {
      
        [Required(ErrorMessage = "Make name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Abrv is a required field.")]
        public string Abrv { get; set; }
    }
    public class MakeDTO : CreateMakeDTO
    {
        public int Id { get; set; }
    }


}
