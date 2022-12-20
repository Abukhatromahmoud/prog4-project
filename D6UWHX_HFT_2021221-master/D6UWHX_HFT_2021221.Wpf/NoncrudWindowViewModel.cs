using D6UWHX_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace D6UWHX_HFT_2021221.Wpf
{
    public class NoncrudWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public RestCollection<Track> Tracks { get; set; }
        public RestCollection<Album> Albums { get; set; }
        public RestCollection<Artist> Artists { get; set; }

        public ICommand Get_LONGEST_TRACK_Artist { get; set; }
        public Track LONGEST_TRACK_Artist;

        public Track lONGEST_TRACK_Artist
        {
            get { return LONGEST_TRACK_Artist; }
            set
            {
                if (value != null)
                {
                    LONGEST_TRACK_Artist = new Track()
                    {
                        TrackId = value.TrackId,
                        NamePlace = value.NamePlace,
                        Length = value.Length,
                    };
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GetShortestAlbum { get; set; }
        public Album shortestAlbumArtist;

        public Album ShortestAlbumArtist
        {
            get { return shortestAlbumArtist; }
            set
            {
                if (value != null)
                {
                    shortestAlbumArtist = new Album()
                    {
                        AlbumID = value.AlbumID,
                        Title = value.Title,
                        BasePrice = value.BasePrice
                    };
                }
                OnPropertyChanged();
            }
        }

        public ICommand GetShortestTrack { get; set; }
        public Album LowestAlbumArtist;

        public Album lowestAlbumArtist
        {
            get { return LowestAlbumArtist; }
            set
            {
                if (value != null)
                {
                    LowestAlbumArtist = new Album()
                    {
                        AlbumID = value.AlbumID,
                        Title = value.Title,
                        BasePrice = value.BasePrice
                    };
                }
                OnPropertyChanged();
            }
        }

        public ICommand GetHighestAlbumArtist { get; set; }

        public int _LongestTrack;
        public int LongestTrack
        {
            get { return _LongestTrack; }
            set
            {
                if (value != null)
                {
                    _LongestTrack = value;
                }
                OnPropertyChanged();
            }
        }

        public int _ShortestTrack;
        public int ShortestTrack
        {
            get { return _ShortestTrack; }
            set
            {
                if (value != null)
                {
                    _ShortestTrack = value;
                }
                OnPropertyChanged();
            }
        }

        public ICommand GetTotalArtists { get; set; }

        public int _TotalArtists;
        public int TotalArtists
        {
            get { return _TotalArtists; }
            set
            {
                if (value != null)
                {
                    _TotalArtists = value;
                }
                OnPropertyChanged();
            }
        }
        public List<Artist> getartists;

        public List<Artist> Artist1
        {
            get { return getartists; }
            set
            {
                if (value != null)
                {
                    getartists = new List<Artist>();

                    foreach (var item in value)
                    {
                        Artist veteranSoldier = new Artist()
                        {
                            ArtistId = item.ArtistId,
                            Name = item.Name,
                            Age = item.Age,
                        };
                        getartists.Add(veteranSoldier);
                    }
                }
                OnPropertyChanged();
            }
        }

        public ICommand GetArtistsBefore2005 { get; set; }
        public List<Artist> artistsBefore2005;

        public List<Artist> ArtistsBefore2005
        {
            get { return artistsBefore2005; }
            set
            {
                if (value != null)
                {
                    artistsBefore2005 = new List<Artist>();

                    foreach (var item in value)
                    {
                        Artist soldierBefore2005 = new Artist()
                        {
                            ArtistId = item.ArtistId,
                            Name = item.Name,
                            Age = item.Age,
                        };
                        artistsBefore2005.Add(soldierBefore2005);
                    }
                }
                OnPropertyChanged();
            }
        }

        public int SelectedTrackId { get; set; }
        public int SelectedAlbumonId { get; set; }

        public NoncrudWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                LoadData();

                Get_LONGEST_TRACK_Artist = new RelayCommand(async () =>
                {
                    var length = await Tracks.GetAsync("Query/LongestTrack");
                    LongestTrack = length.Length;
                });
                GetShortestTrack = new RelayCommand(async () =>
                {
                    var length = await Tracks.GetAsync("Query/GetShortestTrack");
                    ShortestTrack = length.Length;
                });
                GetTotalArtists = new RelayCommand(async () =>
                {
                    TotalArtists = Artists.GetTotalCount();
                });
            }
        }

        private async Task LoadData()
        {
            var t1 = Task.Run(() =>
                                Tracks = new RestCollection<Track>("http://localhost:63408/", "hub")
                             );
            t1.Wait();
            var t2 = Task.Run(() =>
                                Albums = new RestCollection<Album>("http://localhost:63408/", "hub")
                             );
            t2.Wait();
            var t3 = Task.Run(() =>
                                Artists = new RestCollection<Artist>("http://localhost:63408/", "hub")
                             );
            t3.Wait();
        }
    }
}