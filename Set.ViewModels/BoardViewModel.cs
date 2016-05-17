using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Model;
using Set.ViewModels.Events;
using Set.ViewModels.PropertyChanged;

namespace Set.ViewModels
{
    public class BoardViewModel : PropertyChangedImplementation, IDisposable
    {
        private int minimumNumberOfCards = 12;
        private int minimumDeckSize = 2;
        private bool gameEnded = false;

        private readonly DeckGenerator _deckGenerator;
        private readonly IEventAggregator _eventAggregator;
        private readonly ImageGenerator.ImageGenerator _imageGenerator;
        private readonly Timer _timer;
        private readonly Stopwatch _stopwatch;

        public List<Card> Deck { get; set; }
        public ObservableCollection<CardViewModel> CardOnBoardViewModels { get; set; }
        public List<Card> CardOnBoard { get; set; }
        public int FoundedSets { get; set; }

        public DelegateCommand SetCommand { get; set; }

        public String ElapsedTime
        {
            get
            {
                var fmt = @"mm\:ss";
                
                if (_stopwatch.Elapsed.Hours > 0)
                    fmt = @"hh\:" + fmt;

                return _stopwatch.Elapsed.ToString(fmt);
            }
        }

        public BoardViewModel(DeckGenerator deckGenerator, 
                              bool isEasyGame, 
                              IEventAggregator eventAggregator, 
                              ImageGenerator.ImageGenerator imageGenerator, 
                              Timer timer, 
                              Stopwatch stopwatch)
        {
            _deckGenerator = deckGenerator;
            _eventAggregator = eventAggregator;
            _imageGenerator = imageGenerator;
            _timer = timer;
            _stopwatch = stopwatch;

            InitialiseDeck(isEasyGame);

            SetCommand = new DelegateCommand(ExecuteSetCommand, CanExecuteSetCommand);

            _eventAggregator.GetEvent<CardSelected>().Subscribe(OnCardSelected);
            _timer.Elapsed += RaiseTimerUpdate;

            _timer.Interval = 200;
            _timer.Start();
            _stopwatch.Start();
        }

        public void PauseGame()
        {
            _stopwatch.Stop();
            _timer.Stop();
        }

        public void ResumeGame()
        {
            if (gameEnded) return;

            _stopwatch.Start();
            _timer.Start();
        }

        private void InitialiseDeck(bool isEasyGame)
        {
            CardOnBoard = new List<Card>();
            CardOnBoardViewModels = new ObservableCollection<CardViewModel>();
            Deck = _deckGenerator.GetDeck(isEasyGame);

            GetInitialCards();
            while (!SetIsOnBoard() && Deck.Count > minimumDeckSize)
                Get3NewCards();
        }

        private void GetInitialCards()
        {
            while (CardOnBoard.Count < minimumNumberOfCards)
                Get3NewCards();
        }

        private void Get3NewCards()
        {
            var newCards = Deck.GetRange(0, 3);
            Deck.RemoveRange(0, 3);

            CardOnBoard.AddRange(newCards);
            foreach (var card in newCards)
                CardOnBoardViewModels.Add(new CardViewModel(card, _eventAggregator, _imageGenerator));
        }

        private bool SetIsOnBoard()
        {

            for (var firstCardIndex = 0; firstCardIndex < CardOnBoard.Count; firstCardIndex++)
            {
                for (var secondCardIndex = firstCardIndex + 1; secondCardIndex < CardOnBoard.Count; secondCardIndex++)
                {
                    var thirdCard = CardOnBoard[firstCardIndex].FindComplementaryCard(CardOnBoard[secondCardIndex]);
                    if (CardOnBoard.Contains(thirdCard))
                        return true;
                }
            }

            return false;
        }

        private void OnCardSelected(object obj)
        {
            SetCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteSetCommand()
        {
            var selectedCards = CardOnBoard.Where(_ => _.IsSelected).ToList();
            if (selectedCards.Count != 3) return;

            if (IsSelectedCardsIsSet(selectedCards))
            {
                RemoveSelectedCardAndViewModels(selectedCards);

                ++FoundedSets;

                while (Deck.Count > minimumDeckSize && (CardOnBoard.Count < minimumNumberOfCards || !SetIsOnBoard()))
                    Get3NewCards();

                RaisePropertyChanged(() => FoundedSets);
                RaisePropertyChanged(() => Deck);

                if (Deck.Count == 0 && !SetIsOnBoard())
                    EndGame();
            }
        }

        private void EndGame()
        {
            PauseGame();
            gameEnded = true;
            MessageBox.Show("Game ended. There is no sets on table");
        }

        private void RemoveSelectedCardAndViewModels(List<Card> selectedCards)
        {
            var selectedViewModels = CardOnBoardViewModels.Where(_ => _.IsSelected).ToList();
            foreach (var selectedViewModel in selectedViewModels)
                CardOnBoardViewModels.Remove(selectedViewModel);

            foreach (var card in selectedCards)
                CardOnBoard.Remove(card);
        }

        private bool IsSelectedCardsIsSet(List<Card> selectedCards)
        {
            var selectedCardsIsSet = selectedCards[0].FindComplementaryCard(selectedCards[1]).Equals(selectedCards[2]);
            var message = selectedCardsIsSet ? "This is set" : "This is not set";

            MessageBox.Show(message);
            return selectedCardsIsSet;
        }

        private bool CanExecuteSetCommand()
        {
            return CardOnBoard.Count(_ => _.IsSelected) == 3;
        }

        private void RaiseTimerUpdate(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            RaisePropertyChanged(() => ElapsedTime);
        }

        #region Dispose

        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
            }

            _eventAggregator.GetEvent<CardSelected>().Unsubscribe(OnCardSelected);
            _timer.Elapsed -= RaiseTimerUpdate;
            _timer.Dispose();

            disposed = true;
        }

        ~BoardViewModel()
        {
            Dispose(false);
        }
        #endregion
    }
}
