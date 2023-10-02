using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Base
{
    public class BaseEntity
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        public int? Order { get; set; }

        [NotMapped]
        public List<string> Include { get; set; }
        [NotMapped]
        public List<string> Relations { get; set; }

        [NotMapped]
        public int tempId { get; set; }

    }
}
