using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Set.ViewModels.Interfaces;
using Set.ViewModels.PropertyChanged;

namespace Set.ViewModels
{
    public class RulesViewModel : PropertyChangedImplementation, IRulesViewModel
    {
        private int _selectedPage;
        public int SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
                UpdateButtonState();
            }
        }

        public Dictionary<int, BitmapFrame> RuleImages { get; set; }

        public BitmapFrame SelectedPageImage { get { return RuleImages[SelectedPage]; } }

        public DelegateCommand<object> MoveForwardCommand { get; private set; }
        public DelegateCommand<object> MoveBackwardCommand { get; private set; }


        public RulesViewModel(ImageGenerator.ImageGenerator imageGenerator)
        {
            RuleImages = imageGenerator.GetRuleImages();

            MoveForwardCommand = new DelegateCommand<object>(MoveForward, CanMoveForward);
            MoveBackwardCommand = new DelegateCommand<object>(MoveBackward, CanMoveBackward);
        }

        private void MoveBackward(object obj)
        {
            if ((string)obj == "one")
                SelectedPage -= 1;
            else
                SelectedPage = 0;
        }

        private bool CanMoveBackward(object arg)
        {
            return SelectedPage > 0;
        }

        private void MoveForward(object obj)
        {
            if ((string)obj == "one")
                SelectedPage += 1;
            else
                SelectedPage = 7;
        }

        private bool CanMoveForward(object arg)
        {
            return SelectedPage < 7;
        }

        private void UpdateButtonState()
        {
            RaisePropertyChanged(() => SelectedPage);
            RaisePropertyChanged(()=> SelectedPageImage);
            MoveBackwardCommand.RaiseCanExecuteChanged();
            MoveForwardCommand.RaiseCanExecuteChanged();
        }

    }
}
