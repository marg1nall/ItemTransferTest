namespace Items.Api
{
    public interface IItemIdGenerator
    {
        public int Next();
        public int PeekNext();
        public void Reset(int nextValue);
    }
}
