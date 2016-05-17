using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Events;
using Model;
using Model.Enums;
using Set.ViewModels;
using WpfInfrastructure;
using ImageGenerator = global::ImageGenerator.ImageGenerator;

namespace ImageGeneratorTestApp
{
    public class MainWindowViewModel : PropertyChangedImplementation
    {
        private Card _card;
        private IEventAggregator _eventAggregator;
        private global::ImageGenerator.ImageGenerator _imageGenerator;

        public List<NumberOfSymbols> NumberOfSymbolList { get; set; }
        public List<Color> ColorList { get; set; }
        public List<Filling> FillingList { get; set; }
        public List<SymbolType> SymbolTypeList { get; set; }

        public NumberOfSymbols SelectedNumberOfSymbols
        {
            get
            {
                return NumberOfSymbolList.Single(_ => _ == _card.NumberOfSymbols);
            }
            set
            {
                if (_card.NumberOfSymbols == value) return;
                ClearCardViewModel();
                _card.NumberOfSymbols = value;
                SetCardViewModel();
            }
        }

        public Color SelectedColor
        {
            get
            {
                return ColorList.Single(_ => _ == _card.Color);
            }
            set
            {
                if (_card.Color == value) return;
                ClearCardViewModel();
                _card.Color = value;
                SetCardViewModel();
            }
        }

        public Filling SelectedFilling
        {
            get
            {
                return FillingList.Single(_ => _ == _card.Filling);
            }
            set
            {
                if (_card.Filling == value) return;
                ClearCardViewModel();
                _card.Filling = value;
                SetCardViewModel();
            }
        }

        public SymbolType SelectedSymbolType
        {
            get
            {
                return SymbolTypeList.Single(_ => _ == _card.SymbolType);
            }
            set
            {
                if (_card.SymbolType == value) return;
                ClearCardViewModel();
                _card.SymbolType = value;
                SetCardViewModel();
            }
        }

        public CardViewModel CardViewModel { get; set; }

        public MainWindowViewModel()
        {
            _card = new Card();
            _eventAggregator = new EventAggregator();
            _imageGenerator = new global::ImageGenerator.ImageGenerator();

            CardViewModel = new CardViewModel(_card, _eventAggregator, _imageGenerator);

            NumberOfSymbolList = new List<NumberOfSymbols>();
            foreach (NumberOfSymbols numberOfSymbols in Enum.GetValues(typeof(NumberOfSymbols)))
                NumberOfSymbolList.Add(numberOfSymbols);

            ColorList = new List<Color>();
            foreach (Color color in Enum.GetValues(typeof(Color)))
                ColorList.Add(color);

            FillingList = new List<Filling>();
            foreach (Filling filling in Enum.GetValues(typeof(Filling)))
                FillingList.Add(filling);

            SymbolTypeList = new List<SymbolType>();
            foreach (SymbolType symbolType in Enum.GetValues(typeof(SymbolType)))
                SymbolTypeList.Add(symbolType);

        }

        private void ClearCardViewModel()
        {
            CardViewModel = null;
            RaisePropertyChanged(() => CardViewModel);
        }

        private void SetCardViewModel()
        {
            //            CardViewModel.Clear();
            //            CardViewModel.Add(new CardViewModel(_card, _eventAggregator, _imageGenerator));
            CardViewModel = new CardViewModel(_card, _eventAggregator, _imageGenerator);
            RaisePropertyChanged(() => CardViewModel);
        }
    }
}
