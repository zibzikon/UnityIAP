using System.Threading.Tasks;
using Demos.Demo_1.Kernel.Data;
using Kernel.DataProviders;

namespace Demos.Demo_1.Kernel.SaveDataProcessors
{
    public class PlayerSaveDataProcessor : AtomSaveDataProcessor<PlayerData>
    {
        private readonly Player _player;

        public override string Key => "player_data";

        public PlayerSaveDataProcessor(Player player)
        {
            _player = player;
        }

        public override object GetData() => new PlayerData() { GemsCount = _player.Gems };

        public override Task ProcessDataLoadAsync(PlayerData data)
        {
            _player.Initialize(data.GemsCount);
            
            return Task.CompletedTask;
        }
    }
}