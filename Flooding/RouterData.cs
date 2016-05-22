using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class RouterData : IEquatable<RouterData>
    {
        private char _id;
        private readonly int _maxCapacity = 100;

        public RouterData(char id)
        {
            _id = id;
        }

        #region Properties

        public char ID => _id;
        /// <summary>
        /// Maximum packets the router can hold
        /// </summary>
        public int MaxCapacity => _maxCapacity;

        #endregion

        public bool Equals(RouterData other) => _id == other.ID;

        public override string ToString() => _id.ToString();
    }
}
