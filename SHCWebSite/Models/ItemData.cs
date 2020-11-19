using System;
using System.Collections.Generic;
using System.Linq;

namespace SquadHealthCheck
{
    public class ItemData
    {
        private Item _Item { get; set; }
        public int Id => _Item.Id;
        public string BestText => _Item.BestText;
        public string WorstText => _Item.WorstText;
        public string Name { get; private set; }
        public ItemValue Value { get; private set; }
        public double SquadValue { get; private set; }
        public int SquadBadCount { get; private set; } = 0;
        public int SquadMediumCount { get; private set; } = 0;
        public int SquadGoodCount { get; private set; } = 0;
        public ItemData(Item item, ItemValue value, IEnumerable<UserItem> squadData)
        {
            _Item = item;
            Name = item.Name;
            Value = value;
            SquadValue = squadData.Any() ? GetSquadValue(squadData) : 0;
            
            foreach (var sv in squadData)
            {
                switch (sv.Value)
                {
                    case ItemValue.Bad:
                        ++SquadBadCount;
                        break;
                    case ItemValue.Medium:
                        ++SquadMediumCount;
                        break;
                    case ItemValue.Good:
                        ++SquadGoodCount;
                        break;
                }
            }
        }

        private double GetSquadValue(IEnumerable<UserItem> squadData)
        {
            var avg = squadData.Average(x => (double)x.Value);
            var min = (double)squadData.Min(x => x.Value);
            return min + ( avg > min ? 0.5 : 0);
        }
    }
}