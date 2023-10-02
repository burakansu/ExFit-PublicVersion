using Microsoft.AspNetCore.Http;
using Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.ExFit
{
    public class Member : BaseEntity
    {
        public int MemberId { get; set; }
        public string? IMG { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string? Adress { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime RegistrationTime { get; set; }
        public int Gift { get; set; }
        public int Price { get; set; }
        public int Block { get; set; }
        public string? HealthReport { get; set; }
        public string? IdentityCard { get; set; }
        public string Password { get; set; }

        public int ExcersizeId { get; set; }
        public Excersize? Excersize { get; set; }

        public int DietId { get; set; }
        public Diet? Diet { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public int PackageId { get; set; }
        public Package? Package { get; set; }

        public ICollection<MemberMeazurement>? Meazurement { get; set; }
        public ICollection<Task>? Tasks { get; set; }


        [NotMapped]
        public IFormFile? FileAvatarIMG { get; set; }
        [NotMapped]
        public IFormFile? FileHealthReport { get; set; }
        [NotMapped]
        public IFormFile? FileIdentityCard { get; set; }
    }
}