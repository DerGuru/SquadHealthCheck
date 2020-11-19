namespace SquadHealthCheck
{
    using System;

    public partial class SquadMember
    {
        public long Id { get; set; }

        public int Squad { get; set; }

        public Guid Userhash { get; set; }
    }
}
