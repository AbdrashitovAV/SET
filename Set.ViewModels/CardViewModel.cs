using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Model;
using Set.ViewModels.Events;
using Set.ViewModels.PropertyChanged;

namespace Set.ViewModels
{
    public class CardViewModel : PropertyChangedImplementation
    {
        private readonly IEventAggregator _eventAggregator;
        private Card _card;

        public bool IsSelected { get { return _card.IsSelected; } }
        public BitmapFrame CardImage { get; private set; }
        public ICommand OnMouseButtonDownCommand { get; set; }

        public CardViewModel(Card card, IEventAggregator eventAggregator, ImageGenerator.ImageGenerator imageGenerator)
        {
            _card = card;
            _eventAggregator = eventAggregator;

            CardImage = imageGenerator.GetImageForCard(_card);

            OnMouseButtonDownCommand = new DelegateCommand<MouseButtonEventArgs>(OnMouseButtonDown);
        }

        private void OnMouseButtonDown(MouseButtonEventArgs mouseButtonEventArgs)
        {
            _card.IsSelected = !_card.IsSelected;
            RaisePropertyChanged(() => IsSelected);

            _eventAggregator.GetEvent<CardSelected>().Publish(_card);
        }
    }
}
