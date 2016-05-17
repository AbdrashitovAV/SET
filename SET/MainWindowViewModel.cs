using System;
using System.Diagnostics;
using System.Timers;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Model;
using Set.ViewModels;
using Set.ViewModels.PropertyChanged;

namespace SET
{
    public class MainWindowViewModel : PropertyChangedImplementation, IDisposable
    {
        private readonly EventAggregator _eventAggregator;
        private readonly ImageGenerator.ImageGenerator _imageGenerator;

        public BoardViewModel BoardViewModel { get; set; }
        public MenuViewModel MenuViewModel { get; set; }
        public RulesViewModel RulesViewModel { get; set; }

        public bool IsMenuVisible { get; set; }
        public bool IsRulesVisible { get; set; }

        public DelegateCommand<object> StartGameCommand { get; set; }
        public DelegateCommand<object> EscapeKeyPressedCommand { get; set; }
        public DelegateCommand<object> ResumeGameCommand { get; set; }
        public DelegateCommand<object> ShowRulesCommand { get; set; }

        public MainWindowViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _imageGenerator = new ImageGenerator.ImageGenerator();

            RulesViewModel = new RulesViewModel(_imageGenerator);

            StartGameCommand = new DelegateCommand<object>(StartGame);
            EscapeKeyPressedCommand = new DelegateCommand<object>(EscapeKeyPressed);
            ResumeGameCommand = new DelegateCommand<object>(ResumeGame);
            ShowRulesCommand = new DelegateCommand<object>(ShowRulesView);

            IsMenuVisible = true;
            IsRulesVisible = false;
        }

        public void StartGame(object o)
        {
            var isEasyGame = false;
            if (o is bool)
                isEasyGame = (bool)o;

            if (BoardViewModel != null)
                BoardViewModel.Dispose();

            BoardViewModel = new BoardViewModel(new DeckGenerator(), isEasyGame, _eventAggregator, _imageGenerator, new Timer(), new Stopwatch());

            IsMenuVisible = false;

            RaisePropertyChanged(() => IsMenuVisible);
            RaisePropertyChanged(() => BoardViewModel);
        }


        private void EscapeKeyPressed(object obj)
        {
            if (IsRulesVisible)
            { 
                CloseRulesView();
                return;
            }

            if (BoardViewModel == null) return;

            ChangeMenuState();
        }

        private void ChangeMenuState()
        {
            IsMenuVisible = !IsMenuVisible;

            if (IsMenuVisible)
                BoardViewModel.PauseGame();
            else
                BoardViewModel.ResumeGame();

            RaisePropertyChanged(() => IsMenuVisible);
        }

        private void ResumeGame(object obj)
        {
            IsMenuVisible = false;

            BoardViewModel.ResumeGame();

            RaisePropertyChanged(() => IsMenuVisible);
        }

        private void ShowRulesView(object obj)
        {
            IsRulesVisible = true;
            RaisePropertyChanged(() => IsRulesVisible);
        }

        private void CloseRulesView()
        {
            IsRulesVisible = false;
            RaisePropertyChanged(() => IsRulesVisible);
        }

        public void Dispose()
        {
            if (BoardViewModel != null)
            {
                BoardViewModel.Dispose();
            }
        }
    }
}
