using System;

namespace STT.Application.Dto.Request
{
    public class CreateWatchlistRequestDto
    {
        public string? Title { get; set; }
        public Guid UserId { get; set; }
    }
}