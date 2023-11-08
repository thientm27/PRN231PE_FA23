﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class TattooSticker
    {
        public int TattooStickerId { get; set; }
        public string TattooStickerName { get; set; } = null!;
        public DateTime? ImportDate { get; set; }
        public string? TattooStickerDescription { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public string? TypeId { get; set; }

        public virtual RoseTattooType? Type { get; set; }
    }
}
