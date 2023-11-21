﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone
{
    internal class Song
    {
        string _name;
        string _duration;
        Album _album;

        public Song(string Name, Album Album, string Duration) 
        {
            _name = Name;
            _duration = Duration;
            _album = Album;
        }

        public string Name { get { return _name; } }
        public string Duration { get { return _duration; } }
        public Album Album { get {  return _album; } }




    }
}
