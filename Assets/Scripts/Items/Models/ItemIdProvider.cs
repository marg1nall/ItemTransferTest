using Items.Api;

namespace Items.Models
{
    public class ItemIdProvider : IItemIdGenerator
    {
        private int _current = 1;

        public int Next()
        {
            return _current++;
        }

        public int PeekNext()
        {
            return _current;
        }

        public void Reset(int nextValue)
        {
            _current = nextValue < 1 ? 1 : nextValue;
        }
    }
}
