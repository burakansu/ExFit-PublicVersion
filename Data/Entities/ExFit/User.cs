using Data.Entities.Base;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.ExFit
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
        public string IMG { get; set; }
        public string Phone { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Task>? Tasks { get; set; }

        [NotMapped]
        public string? Position
        {
            get
            {
                switch (this.Type)
                {
                    case 1:
                        return "Yönetici";
                    case 2:
                        return "Antrenör";
                    case 3:
                        return "Diyetisyen";
                    case 4:
                        return "Satış Temsilcisi";
                    case 5:
                        return "Sosyal Medya Sorumlusu";
                    case 6:
                        return "Danışman";
                    case 7:
                        return "Genel";
                    default: 
                        return "-";
                }
            }
        }
        [NotMapped]
        public IFormFile? file{ get; set; }
    }
}