using D6UWHX_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace D6UWHX_HFT_2021221.Wpf
{
    public class AlbumWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Album> Albums { get; set; }
        private Album selectedAlbum;

        public Album SelectedAlbum
        {
            get { return selectedAlbum; }
            set
            {
                if (value != null)
                {
                    selectedAlbum = new Album()
                    {
                        Title = value.Title,
                        AlbumID = value.AlbumID,
                        BasePrice = value.BasePrice
                    };
                    OnPropertyChanged();
                    (DeleteAlbumCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateAlbumCommand { get; set; }
        public ICommand DeleteAlbumCommand { get; set; }
        public ICommand UpdateAlbumCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public AlbumWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Albums = new RestCollection<Album>("http://localhost:63408/", "hub");
                CreateAlbumCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Albums.Add(new Album()
                        {
                            Title = SelectedAlbum.Title,
                            BasePrice = SelectedAlbum.BasePrice
                        });
                        MessageBox.Show("New Album Created");
                        GetAllAlbums();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                });

                UpdateAlbumCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Albums.Update(SelectedAlbum);
                        MessageBox.Show("Album Updated");
                        GetAllAlbums();
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                        MessageBox.Show(ex.ToString());
                    }
                });

                DeleteAlbumCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Albums.Delete(SelectedAlbum.AlbumID);
                        MessageBox.Show("Album Deleted");
                        GetAllAlbums();
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                        MessageBox.Show(ex.ToString());
                    }
                },
                () =>
                {
                    return SelectedAlbum != null;
                });
                SelectedAlbum = new Album();
            }
        }

        private async void GetAllAlbums()
        {
            await Albums.GetListAsync("Album");
        }
    }
}