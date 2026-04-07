using Items.Models;

namespace Items.Api
{
    public interface IRandomItemGenerator
    {
        public ItemData CreateRandom(int id);
    }
}
