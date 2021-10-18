using System;

namespace NgTemplate.API.Entities
{
    public class BaseEntity
    {
        public int AddedById { get; set; }
        public DateTime AddedDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}