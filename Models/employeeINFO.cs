using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employeeportal.Models
{
    public class employeeINFO
    {



        //personal information
        public string? FirstNAME { get; set; }
        public string? LastNAME { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Nationality { get; set; }
        public string? Photo { get; set; }


        //contact information
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContact { get; set; }


        //job information
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? EmployeeID { get; set; }


        public int? departmentID { get; set; }
        [NotMapped]
        public string? departmentNAME { get; set; }
        [NotMapped]
        public List<department>? Department { get; set; }


        public string? Position { get; set; }
        public string? DateOfJoining { get; set; }
        public string? EmployeeType { get; set; }
        public string? Supervisor { get; set; }


        //compensation
        public string? SalaryRate { get; set; }
        public string? PayFrequency { get; set; }
        public string? BenefitsEligibility { get; set; }


        //bank information
        public string? BankNAME { get; set; }
        public string? AccountNumber { get; set; }
        public string? RoutingNumber { get; set; }


        //documentation
        public string? CV { get; set; }
        public string? OfferLetter { get; set; }
        public string? IdentificationDocuments { get; set; }
        public string? TaxDocuments { get; set; }


    }
}
