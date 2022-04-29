using System.Collections.ObjectModel;

namespace BattleTechTracking.Models
{
    public class Targeting : BaseModel
    {
        private ObservableCollection<TargetedEntity> _targets;

        public ObservableCollection<TargetedEntity> Targets
        {
            get => _targets;
            set
            {
                _targets = value;
                OnPropertyChanged(nameof(Targets));
            }
        }

        private TargetedEntity _selectedTarget;

        public TargetedEntity SelectedTarget
        {
            get => _selectedTarget;
            set
            {
                _selectedTarget = value;
                OnPropertyChanged(nameof(SelectedTarget));
            }
        }
    }
}
