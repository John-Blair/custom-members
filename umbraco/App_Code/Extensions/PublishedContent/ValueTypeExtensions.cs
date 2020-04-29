namespace JB.UmbracoExtensions
{
    public static class ValueTypeExtensions
    {

        // Some packages store document types without a node id - return 0.
        // Generate a "unique id" for use in html id attributes.
        // Using above max int as the starting point as this will not clash with any existing umbraco node id.
        private const long _longSeed = ((long) int.MaxValue) + 1;
        private static long _nextUniqueNodeId = _longSeed;

        // Make access thread safe.
        internal static readonly object idlock = new object();


        internal static long NextUniqueNodeId
        {
            get
            {
                lock (idlock)
                {
                    if (_nextUniqueNodeId == long.MaxValue)
                    {
                        // Reset seed.
                        _nextUniqueNodeId = _longSeed;
                    }

                    return _nextUniqueNodeId++;
                }
                
            }
        }

        public static long UniqueId<T> (this T id) where T : struct  {

            return NextUniqueNodeId;

        }

        // Get a unique block of ids for use in HTML controls for use in id and href synchronisation.
        // Just return the first of those ids - knowing the following sequential ids are reserved.
        public static long UniqueIdRange<T>(this T id, int rangeSize = 1) where T : struct
        {
            // Reserve a range of node ids in thread safe manner.
            // Note: Keep ids sequential even if mid range hits the max long id by resetting the starting id to the seed.
            lock (idlock)
            {
                // Handle wrap of ids early to keep ids sequential.
                if ( long.MaxValue - rangeSize < _nextUniqueNodeId) _nextUniqueNodeId = _longSeed;

                long result = _nextUniqueNodeId;
                _nextUniqueNodeId += rangeSize;

                return result;
            }
        }

    }
}