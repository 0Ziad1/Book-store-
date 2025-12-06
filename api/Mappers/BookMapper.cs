using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.Models;

namespace api.Mappers
{
    public static class BookMapper
    {
        public static Book ToBookFromCreateDto(this CreateBookDto bookDto)
        {
            return new Book
            {
                ISBN = bookDto.ISBN,
                Title = bookDto.Title,
                TotalCopies = bookDto.TotalCopies,
                Publisher = bookDto.Publisher,
                AvailableCopies = bookDto.AvailableCopies,
                PublicationYear = bookDto.PublicationYear,
                Authors = bookDto.Authors,
            };
        }
        public static Book ToBookFromUpdateDto(this updateBookDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                TotalCopies = bookDto.TotalCopies,
                Publisher = bookDto.Publisher,
                AvailableCopies = bookDto.AvailableCopies,
                PublicationYear = bookDto.PublicationYear,
                Authors = bookDto.Authors,
            };
        }
    }
}