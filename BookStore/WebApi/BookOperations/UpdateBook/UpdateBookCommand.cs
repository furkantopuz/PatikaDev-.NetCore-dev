using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Contorllers;
using WebApi.Coomon;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand{
        public UpdateBookModel Model {get; set;}
        private readonly BookStoreDbContext _dbcontext;

        public UpdateBookCommand(BookStoreDbContext dbcontext){
            _dbcontext = dbcontext;
        }

        public void Handle(int id){
            var book = _dbcontext.Books.SingleOrDefault(x => x.Id == id);

            if(book is not null){
                throw new InvalidOperationException("Kitap bulunamad─▒.");
            }
                
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;

            _dbcontext.SaveChanges();
        }

        public class UpdateBookModel{
            public int Id {get; set;}
            public string Title { get; set; }
            public int GenreId { get; set; }  
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }                
        }
    }
}