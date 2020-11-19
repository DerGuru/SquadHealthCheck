namespace SquadHealthCheck
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Item : IEquatable<Item>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string BestText { get; set; }

        public string WorstText { get; set; }

        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Item))
                return false;
            if (base.Equals(obj))
                return true;
            return Id.Equals((obj as Item).Id);
        }

        public bool Equals(Item other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
