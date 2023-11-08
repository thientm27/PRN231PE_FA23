using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<TattooSticker> TattooStickers { get; set; }
    }
}
