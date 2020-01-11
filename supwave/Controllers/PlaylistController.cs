using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using supwave.Models;
using supwave.Data;

namespace supwave.Controllers
{
    [Authorize]
    public class PlaylistController : Controller
    {
        private readonly ApplicationDbContext _database;
        public PlaylistController(ApplicationDbContext database)
        {
            _database = database;
        }

        [Route("playlists")]
        public IActionResult Playlists() // View playlist list
        {
            // Retrive all user's playlist from the database
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            ViewData["Playlists"] = playlists;
            return View();
        }

        [HttpGet, Route("add-playlist")]
        public IActionResult Create() // Form to add a new playlist
        {
            return View();
        }

        [HttpPost, Route("add-playlist")]
        public IActionResult Create(Playlist playlist) // Form handler to add a new playlist
        {
            playlist.User = User.Identity.Name;
            _database.Playlist.Add(playlist);
            _database.SaveChanges();

            ViewData["Playlist"] = playlist;
            return View();
        }


        [HttpGet, Route("delete-playlist")]
        public IActionResult Delete()
        {
            // Retrive all user's playlist from the database
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            ViewData["Playlists"] = playlists;
            return View();
        }

        [HttpPost, Route("delete-playlist")]
        public IActionResult Delete(Playlist playlist)
        {

            // DELETE ALL SONGS IN THRE PLAYLIST

            // DELETE PLAYLIST            
            _database.Playlist.Attach(playlist);
            _database.Playlist.Remove(playlist);
            _database.SaveChanges();

            // Retrive all user's playlist from the database
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            ViewData["Playlists"] = playlists;
            return View();
        }

        [HttpGet, Route("edit-playlist")]
        public IActionResult Edit()
        {
            // Retrive all user's playlist from the database
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            ViewData["Playlists"] = playlists;
            return View();
        }


        [HttpPost, Route("edit-playlist")]
        public IActionResult Edit(Playlist playlist)
        {
            Console.WriteLine("Id: " + playlist.Id);
            Console.WriteLine("Name: " + playlist.Name);
            Console.WriteLine("User: " + playlist.User);

            // CHANGE PLAYLIST NAME
            var playlistToRename = _database.Playlist.First(p => p.Id == playlist.Id);
            playlistToRename.Name = playlist.User;
            _database.SaveChanges();

            // Retrive all user's playlist from the database
            var playlists = _database.Playlist.Where(playlist => playlist.User.Equals(User.Identity.Name)).ToList();
            ViewData["Playlists"] = playlists;
            return View();
        }




    }
}