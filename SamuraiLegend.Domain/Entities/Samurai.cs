using SamuraiLegend.Domain.Common;
using System.Collections.Generic;

namespace SamuraiLegend.Domain.Entities
{
    public class Samurai : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ShortStory { get; set; }
        public List<Quote> Quotes { get; set; } = new List<Quote>();
        public List<Battle> Battles { get; set; } = new List<Battle>();
    }
}
