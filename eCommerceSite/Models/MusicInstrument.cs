﻿using System.ComponentModel.DataAnnotations;

namespace eCommerceSite.Models
{
    /// <summary>
    /// Represents a single music instrument for available for purchase
    /// </summary>
    public class MusicInstrument
    {
        /// <summary>
        /// The unique identifier for each music instrument product
        /// </summary>
        [Key]
        public int InstrumentID { get; set; }

        /// <summary>
        /// The name of the music instrument
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The sales price of music instrument
        /// </summary>
        [Range(0, 10000)]
        public double Price { get; set; }
    }
}
