﻿using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dto
{
    public class SongDTO
    {

     
            public string Name { get; set; }
            public TimeSpan Duration { get; set; }
            public string Album { get; set; }
            public string Artist { get; set; }
            public int ListenCount { get; set; }
            public string[] Playlists { get; set; }


        public SongDTO(Song song)
            {
                Name = song.Name;
                Duration = song.Duration;
                Album = song.Album;
                Artist = song.Artist;
                ListenCount = song.ListenCount;
                Playlists = song.Playlists;
            }

        public SongDTO()
        {
        }

        

    }
}