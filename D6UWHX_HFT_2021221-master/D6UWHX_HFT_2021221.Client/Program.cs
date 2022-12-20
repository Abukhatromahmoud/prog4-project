using ConsoleTools;
using D6UWHX_HFT_2021221.Data;
using D6UWHX_HFT_2021221.Logic.Classes;
using D6UWHX_HFT_2021221.Models;
using D6UWHX_HFT_2021221.Repository.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D6UWHX_HFT_2021221.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            RestService rest = new RestService("http://localhost:5000");


            List<Track> tracks = rest.Get<Track>("track");





            MusicLibraryContext mlc = new MusicLibraryContext();
            AlbumRepository albumRepo = new AlbumRepository(mlc);
            TrackRepository trackRepo = new TrackRepository(mlc);
            ArtistRepository artistRepo = new ArtistRepository(mlc);
            TrackLogic trackLogic = new TrackLogic(trackRepo);
            AlbumLogic albumLogic = new AlbumLogic(albumRepo);
            ArtistLogic artistLogic = new ArtistLogic(artistRepo);

            var subMenuCreate = new ConsoleMenu()
                 .Add(">>ADD A NEW TRACK", () => AddNewTrack(trackLogic))
                 .Add(">> CREATE A NEW Album", () => AddNewAlbum(albumLogic))
                 .Add(">> ADD A NEW Artist TO the music", () => AddNewArtist(artistLogic))
                 .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                 .Configure(config =>
                 {
                     config.Selector = "--> ";
                     config.SelectedItemBackgroundColor = ConsoleColor.Green;
                 });
            //
            var subMenuList = new ConsoleMenu()
                .Add(">> LIST ALL Tracks", () => ListAllTracks(trackLogic))
                .Add(">> LIST ALL ALBums", () => ListAllAlbum(albumLogic))
                .Add(">> LIST ALL ARTIST", () => ListAllArtist(artistLogic))

                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var subMenuGetBy = new ConsoleMenu()
               .Add(">> GET ONE TRACK BY ID", () => GetOneTrack(trackLogic))
               .Add(">> GET ONE ALBUM BY ID", () => GetOneAlbum(albumLogic))
               .Add(">> GET ONE ARTIST BY ID", () => GetOneArtist(artistLogic))
               .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
               .Configure(config =>
               {
                   config.Selector = "--> ";
                   config.SelectedItemBackgroundColor = ConsoleColor.DarkYellow;
               });

            var subMenuListRead = new ConsoleMenu()
               .Add(">> LIST ALL", () => subMenuList.Show())
               .Add(">> GET BY ID", () => subMenuGetBy.Show())
               .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
               .Configure(config =>
               {
                   config.Selector = "--> ";
                   config.SelectedItemBackgroundColor = ConsoleColor.Green;
               });
            var subMenuNonCrud = new ConsoleMenu()
               .Add(">> Q1 - LIST THE LONGEST TRACK", () => GetTheLongestTrack(trackLogic))
               .Add(">> Q2 - LIST THE Shortest TRACK", () => GetTheShortestTrack(trackLogic)).Add(">> Q3 - LIST THE LAST TRACK", () => GivemeTheLastTrack_(trackLogic))
               .Add(">> Q4 - LIST THE LAST TRACK with Name Place", () => GiveMeTheLastTrackWithNamePlace_(trackLogic))
               .Add(">> Q5 - LIST THE LAST TRACK with Id", () => GiveMeTheLastTrackWithId_(trackLogic))



               .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
               .Configure(config =>
               {
                   config.Selector = "--> ";
                   config.SelectedItemBackgroundColor = ConsoleColor.DarkYellow;
               });

            var subMenuUpdate = new ConsoleMenu()
               .Add(">> CHANGE TRACK ID", () => ChangeTrackId(trackLogic))
               .Add(">> CHANGE ALBUM ID", () => ChangeAlbumID(albumLogic))
               .Add(">> CHANGE ARTIST ID", () => ChangeArtistID(artistLogic))
               .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
               .Configure(config =>
               {
                   config.Selector = "--> ";
                   config.SelectedItemBackgroundColor = ConsoleColor.Green;
               });

            var subMenuDelete = new ConsoleMenu()
                .Add(">> DELETE Track", () => DeleteTrack(trackLogic))
                .Add(">> DELETE Album", () => DeleteAlbum(albumLogic))
                .Add(">> DELETE Artist", () => DeleteArtist(artistLogic))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var subMenuMusic = new ConsoleMenu()
            .Add(">> C - CREATE", () => subMenuCreate.Show())
            .Add(">> R - READ", () => subMenuListRead.Show())
            .Add(">> U - UPDATE", () => subMenuUpdate.Show())
            .Add(">> D - DELETE", () => subMenuDelete.Show())
            .Add(">> NON-CRUD - QUERIES", () => subMenuNonCrud.Show())
            .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.SelectedItemBackgroundColor = ConsoleColor.DarkYellow;
            });



            var menu = new ConsoleMenu()
              .Add(">> ENTER AS A VISITOR ", () => subMenuMusic.Show())

              .Add(">> EXIT", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.SelectedItemBackgroundColor = ConsoleColor.Cyan;
              });

            menu.Show();


        }
        private static void AddNewTrack(TrackLogic tracklogic)
        {
            try
            {
                Console.WriteLine("\n:: CREATING A NEW Track ::\n");
                Console.WriteLine("TYPE THE TrackId!");
                int TrackId = int.Parse(Console.ReadLine());
                Track TG = tracklogic.GetTrack(TrackId);
                Console.WriteLine("\n :: ADDED ::\n");
                Console.WriteLine(TG.ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void AddNewAlbum(AlbumLogic albumlogic)
        {
            try
            {
                Console.WriteLine("\n:: CREATING A NEW Album ::\n");
                Console.WriteLine("TYPE THE AlbumId!");
                int albumID = int.Parse(Console.ReadLine());
                Album TG = albumlogic.GetAlbum(albumID);
                Console.WriteLine("\n :: ADDED ::\n");
                Console.WriteLine(TG.ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void AddNewArtist(ArtistLogic artistlogic)
        {
            try
            {
                Console.WriteLine("\n:: CREATING A NEW Artist ::\n");
                Console.WriteLine("TYPE THE ArtistId!");
                int ArtistId = int.Parse(Console.ReadLine());
                Artist TG = artistlogic.GetArtist(ArtistId);
                Console.WriteLine("\n :: ADDED ::\n");
                Console.WriteLine(TG.ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ListAllTracks(TrackLogic trackLogic)
        {
            Console.WriteLine("\n:: ALL Tracks ::\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.ResetColor();
            trackLogic.GetTracks().ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void ListAllArtist(ArtistLogic artistLogic)
        {
            Console.WriteLine("\n:: ALL Artist ::\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.ResetColor();
            artistLogic.GetArtists().ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void ListAllAlbum(AlbumLogic albumLogic)
        {
            Console.WriteLine("\n:: ALL Album ::\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.ResetColor();
            albumLogic.GetAlbums().ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void GetOneTrack(TrackLogic trackLogic)
        {
            Console.WriteLine("\n:: TYPE THE ID OF THE TRACK YOU WANT TO SEE ::\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.ResetColor();
                Console.WriteLine(trackLogic.GetTrack(id).ToString());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void GetOneAlbum(AlbumLogic albumLogic)
        {
            Console.WriteLine("\n:: TYPE THE ID OF THE ALBUM YOU WANT TO SEE ::\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.ResetColor();
                Console.WriteLine(albumLogic.GetAlbum(id).ToString());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void GetOneArtist(ArtistLogic artistLogic)
        {
            Console.WriteLine("\n:: TYPE THE ID OF THE ARTIST YOU WANT TO SEE ::\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.ResetColor();
                Console.WriteLine(artistLogic.GetArtist(id).ToString());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void GetTheoldestArtist(ArtistLogic artistLogic)
        {
            Console.WriteLine("\n:: I WILL GIVE YOU Oldest Artist::\n");
            var item = artistLogic.GetTheOldestArtist();

        }
        private static void DeleteTrack(TrackLogic trackLogic)
        {
            Console.WriteLine("\n:: TYPE THE ID OF THE TRACK TO DELETE THE RECORD ::\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n :: YOU ARE DELETING " + trackLogic.GetTrack(id).TrackId + " ::");
                trackLogic.DeleteTrack(id);
                Console.WriteLine("\n :: UPDATING... ::\n");
                Console.WriteLine("\n :: DELETED ::\n");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void DeleteAlbum(AlbumLogic albumLogic)
        {
            Console.WriteLine("\n:: TYPE THE ID OF THE ALBUM TO DELETE THE RECORD ::\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n :: YOU ARE DELETING " + albumLogic.GetAlbum(id).AlbumID + " ::");
                albumLogic.DeleteAlbum(id);
                Console.WriteLine("\n :: UPDATING... ::\n");
                Console.WriteLine("\n :: DELETED ::\n");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void DeleteArtist(ArtistLogic artistLogic)
        {
            Console.WriteLine("\n:: TYPE THE ID OF THE ARTIST TO DELETE THE RECORD ::\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n :: YOU ARE DELETING " + artistLogic.GetArtist(id).ArtistId + " ::");
                artistLogic.DeleteArtist(id);
                Console.WriteLine("\n :: UPDATING... ::\n");
                Console.WriteLine("\n :: DELETED ::\n");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ChangeTrackId(TrackLogic trackLogic)
        {
            Console.WriteLine("\n:: TYPE THE ID OF THE TRACK TO UPDATE ::\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("CURRENT TRACK: " + trackLogic.GetTrack(id).TrackId);
                Console.WriteLine("\n :: TYPE THE NEW TRACK ID! ::");
                string trackid = Console.ReadLine();
                Console.WriteLine("\n :: TYPE THE NEW TRACK! ::");
                Console.WriteLine("\n :: UPDATING... ::\n");
                Console.WriteLine("NEW TRACK: " + trackLogic.GetTrack(id).TrackId);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ChangeAlbumID(AlbumLogic albumLogic)
        {
            Console.WriteLine("\n:: TYPE THE ID OF THE Album TO UPDATE ::\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("CURRENT TTRACK: " + albumLogic.GetAlbum(id).AlbumID);
                Console.WriteLine("\n :: TYPE THE NEW Album ID! ::");
                string trackid = Console.ReadLine();
                Console.WriteLine("\n :: TYPE THE NEW ALBUM! ::");
                Console.WriteLine("\n :: UPDATING... ::\n");
                Console.WriteLine("NEW ALBUM: " + albumLogic.GetAlbum(id).AlbumID);

            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ChangeArtistID(ArtistLogic artistLogic)
        {
            Console.WriteLine("\n:: TYPE THE ID OF THE Artist TO UPDATE ::\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("CURRENT TTRACK: " + artistLogic.GetArtist(id).ArtistId);
                Console.WriteLine("\n :: TYPE THE NEW Artist ID! ::");
                string trackid = Console.ReadLine();
                Console.WriteLine("\n :: TYPE THE NEW Artist! ::");
                Console.WriteLine("\n :: UPDATING... ::\n");
                Console.WriteLine("NEW ARTIST: " + artistLogic.GetArtist(id).ArtistId);

            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        private static void GetTheLongestTrack(TrackLogic trackLogic)
        {
            Console.WriteLine("! THIS IS THE LONGEST TRACK !");

            var item = trackLogic.GetLongestTrack();

            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
        private static void GetTheShortestTrack(TrackLogic trackLogic)
        {
            Console.WriteLine("! THIS IS THE SHORTEST TRACK !");
            var item = trackLogic.GetShortestTrack();

            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
        private static void GivemeTheLastTrack_(TrackLogic trackLogic)
        {
            Console.WriteLine("! THIS IS THE LAST TRACK !");
            var item = trackLogic.GiveMeTheLastTrack();

            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
        private static void GiveMeTheLastTrackWithNamePlace_(TrackLogic trackLogic)
        {

            Console.WriteLine("!Give Me TheLast Track With NamePlace!");
            var item = trackLogic.GiveMeTheLastTrackWithNamePlace();

            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
        private static void GiveMeTheLastTrackWithId_(TrackLogic trackLogic)
        {

            Console.WriteLine("!Give Me TheLast Track With ID!");
            var item = trackLogic.GiveMeTheLastTrackWithID();

            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

    }

}

    


