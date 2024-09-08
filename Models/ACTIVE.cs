using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Employeeportal.Models
{
    public abstract class EntityBase
    {
        [Display(Name = "Active")]
        [DefaultValue(true)]
        public bool Active { get; set; }

        [Display(Name = "Created By")]
        public int? CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Updated By")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; }
    }
}
