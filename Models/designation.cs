using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Net.Http;


namespace Employeeportal.Models
{
    public class designation : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int designationID { get; set; }
        public string designationNAME { get; set; }

    }
}
