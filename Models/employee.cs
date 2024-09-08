using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Employeeportal.Models
{
    public class employee : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int employeeID { get; set; }
        public string? employeeNAME { get; set; }

        public string? dob { get; set; }



        public string? phone { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }


        public int? designationID { get; set; }

        public int? departmentID { get; set; }

        [NotMapped]
        public string? designationNAME { get; set; }
        [NotMapped]
        public string? departmentNAME { get; set; }




        [NotMapped]
        public List<designation>? Designation { get; set; }

        [NotMapped]
        public List<department>? Department { get; set; }


    }

}

