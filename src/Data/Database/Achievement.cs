using System.Collections;
using System.Collections.Generic;

namespace Data.Database
{
    public class Achievement : BaseEntity
    {
        public string Name { get; set; }
        public int PointsToReach { get; set; }

        public ICollection<User> Users { get; } = new HashSet<User>();
    }
}