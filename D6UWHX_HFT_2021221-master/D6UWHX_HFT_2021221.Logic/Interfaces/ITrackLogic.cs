using D6UWHX_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D6UWHX_HFT_2021221.Logic.Interfaces
{
    public interface ITrackLogic<T> where T : class
    {
        //non crud
        List<T> GetTracks();
        T HighestLength();
        T GetShortestTrack();
        //

        //crud
        void CreatTrack(int TrackId, string NamePlace, int Length);
        void UpdateTrack(Track track);
        void DeleteTrack(int TrackId);
        T GetTrack(int TrackId);
        //
    }
}
