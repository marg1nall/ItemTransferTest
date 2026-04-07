using System;
using Items.Api;
using Items.Models;

namespace Items.Logic
{
    public class ItemGenerator : IRandomItemGenerator
    {
        private readonly Random _random = new();

        public ItemData CreateRandom(int id)
        {
            int variant = _random.Next(0, 3);

            return variant switch
            {
                0 => new ItemData(id, ItemType.A, 0),

                1 => new ItemData(id, ItemType.A, _random.Next(1, 10)),

                _ => new ItemData(id, ItemType.B, 0)
            };
        }
    }
}
