namespace SquadHealthCheck
{
    using System;

    public partial class SquadAdmin
    {
        public long Id { get; set; }

        public int Squad { get; set; }

        public Guid Adminhash { get; set; }
    }
}
