using Demos.Demo_1.Kernel.Data;

namespace Demos.Demo_1.Kernel
{
    public class PlayerSavingDataProvider : ISavingDataProvider
    {
        private readonly Player _player;
        public string Key => "player_data";

        public PlayerSavingDataProvider(Player player)
        {
            _player = player;
        }
        

        public object GetData() => new PlayerData() { GemsCount = _player.Gems };
    }
}