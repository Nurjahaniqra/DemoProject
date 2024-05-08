using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 1)]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(0.5, 5, ErrorMessage = "The field Credit must be between 0.5 and 5.")]
        public decimal Credit { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DepartmentID { get; set; }
        public int SemesterID { get; set; }

        [ValidateNever]
        public Department? Department { get; set; }

		[ValidateNever]
		public Semester? Semester { get; set; }
    }


}
