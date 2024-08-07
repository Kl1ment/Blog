﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DataAccess.Entities
{
    public record LoginEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string passwordHash { get; set; } = string.Empty;

        public UserEntity? User { get; set; }
    }
}
