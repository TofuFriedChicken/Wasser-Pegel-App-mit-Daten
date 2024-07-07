using System.Collections.ObjectModel;

namespace Pegel_Wetter_DFFUDC
{
    public class ObervableCollection<T>
    {
        public static implicit operator ObervableCollection<T>(ObservableCollection<WaterLevelViewModel.Root> v)
        {
            throw new NotImplementedException();
        }
    }
}