using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class RoseTattooType
    {
        public RoseTattooType()
        {
            TattooStickers = new HashSet<TattooSticker>();
        }

        public string TypeId { get; set; } = null!;
        public string RoseTattooName { get; set; } = null!;
        public string? RoseTattooDescription { get; set; }
        public string? Origin { get; set; }

        public virtual ICollection<TattooSticker> TattooStickers { get; set; }
    }
}
