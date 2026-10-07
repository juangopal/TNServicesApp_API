using System.ComponentModel.DataAnnotations;

namespace TNServicesApp_API.Models
{
    public class StudentsInsertRequest
    {
        [Required]
        public int registerId { get; set; }
        [Required]
        public int total { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string medium { get; set; }
        [Required]
        public DateTime dateOfBirth { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string school { get; set; }
        [Required]
        public string area { get; set; }
        [Required]
        public string district { get; set; }
        [Required]
        public string pincode { get; set; }
        [Required]
        public int schoolType { get; set; }
        [Required]
        public List<Marks> marks { get; set; }
    }

    public class Marks
    {
        [Required]
        public int code { get; set; }
        [Required]
        public int mark { get; set; }
    }

    public class HSC_InsertRequest : StudentsInsertRequest
    {
        [Required]
        public int groupId { get; set; }
    }
}
