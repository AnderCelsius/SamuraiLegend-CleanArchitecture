using System;

namespace SamuraiLegend.Domain.Entities
{
    public class BattleSamurai
    {
        public string SamuraiId { get; set; }
        public int BattleId { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.Now;
    }
}
