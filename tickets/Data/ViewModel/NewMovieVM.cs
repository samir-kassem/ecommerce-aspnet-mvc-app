using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tickets.Data;
using tickets.Data.Base;

namespace tickets.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Movie Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price in $")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Movie Poster is required")]
        [Display(Name = "Movie Poster URL")]
        public string ImageURL { get; set; }


        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }


        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }


        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Movie Category")]
        public MovieCategory MovieCategory { get; set; } // enum

        // Relations 

        [Required(ErrorMessage = "Movie Actor(s) is required")]
        [Display(Name = "Movie Actor(s)")]
        public List<int> ActorIds { get; set; }

        [Required(ErrorMessage = "Movie Cinema is required")]
        [Display(Name = "Movie Cinema")]

        public int CinemaId { get; set; }


        [Required(ErrorMessage = "Movie Producer is required")]
        [Display(Name = "Movie Producer")]
        public int ProducerId { get; set; }
    }
}
