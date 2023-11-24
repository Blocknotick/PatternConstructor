﻿using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}
