﻿using Api.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public string CreateBy { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int MovieEpisodeId { get; set; }
    }
}
