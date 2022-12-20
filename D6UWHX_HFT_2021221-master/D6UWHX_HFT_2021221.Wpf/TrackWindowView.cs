using D6UWHX_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace D6UWHX_HFT_2021221.Wpf
{
    public class TrackWindowView : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Track> Tracks { get; set; }
        private Track selectedTrack;

        public Track SelectedTrack
        {
            get { return selectedTrack; }
            set
            {
                if (value != null)
                {
                    selectedTrack = new Track()
                    {
                        NamePlace = value.NamePlace,
                        TrackId = value.TrackId,
                        Length = value.Length
                    };
                    OnPropertyChanged();
                    (DeleteTrackCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateTrackCommand { get; set; }
        public ICommand DeleteTrackCommand { get; set; }
        public ICommand UpdateTrackCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public TrackWindowView()
        {
            if (!IsInDesignMode)
            {
                Tracks = new RestCollection<Track>("http://localhost:63408/", "hub");
                CreateTrackCommand = new RelayCommand(() =>
                {
                    Tracks.Add(new Track()
                    {
                        NamePlace = SelectedTrack.NamePlace,
                        Length = SelectedTrack.Length
                    });
                });

                UpdateTrackCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Tracks.Update(SelectedTrack);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteTrackCommand = new RelayCommand(() =>
                {
                    Tracks.Delete(SelectedTrack.TrackId);
                },
                () =>
                {
                    return SelectedTrack != null;
                });
                SelectedTrack = new Track();
            }
        }
    }
}
