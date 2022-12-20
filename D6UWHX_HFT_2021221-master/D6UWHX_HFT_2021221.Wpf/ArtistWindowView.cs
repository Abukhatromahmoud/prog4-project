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
    public class ArtistWindowView : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Artist> Artists { get; set; }
        private Artist selectedArtist;

        public Artist SelectedArtist
        {
            get { return selectedArtist; }
            set
            {
                if (value != null)
                {
                    selectedArtist = new Artist()
                    {
                        Name = value.Name,
                        ArtistId = value.ArtistId,
                        Age = value.Age
                    };
                    OnPropertyChanged();
                    (DeleteArtistCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateArtistCommand { get; set; }
        public ICommand DeleteArtistCommand { get; set; }
        public ICommand UpdateArtistCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ArtistWindowView()
        {
            if (!IsInDesignMode)
            {
                GetAllArtists();
                CreateArtistCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Artists.Add(new Artist()
                        {
                            Name = SelectedArtist.Name,
                            Age = SelectedArtist.Age
                        });
                        MessageBox.Show("New Artist Created");
                        GetAllArtists();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });

                UpdateArtistCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Artists.Update(SelectedArtist);
                        MessageBox.Show("Artist Updated");
                        GetAllArtists();
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                        MessageBox.Show(ex.Message);
                    }

                });

                DeleteArtistCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Artists.Delete(SelectedArtist.ArtistId);
                        MessageBox.Show("Artist Deleted");
                        GetAllArtists();
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                        MessageBox.Show(ex.Message);
                    }             
                },
                () =>
                {
                    return SelectedArtist != null;
                });
                SelectedArtist = new Artist();
            }
        }

        private void GetAllArtists()
        {
            Artists = new RestCollection<Artist>("http://localhost:63408/", "hub");
        }
    }
}
