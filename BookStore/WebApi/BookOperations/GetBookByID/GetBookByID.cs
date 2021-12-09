using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Contorllers;
using WebApi.Coomon;

namespace WebApi.BookOperations.GetBook
{
    public class GetBookByIDCommand{
        private readonly BookStoreDbContext _dbcontext;

        public GetBookByIDCommand(BookStoreDbContext dbcontext){
            _dbcontext = dbcontext;
        }

        public GetBookByIdModel Handle(int id){
            GetBookByIdModel model = new GetBookByIdModel();
            var book = _dbcontext.Books.Where(x => id == x.Id).FirstOrDefault();  
            if(book is not null){
                throw new InvalidOperationException("Kitap bulunamadı.");
            }else{  
                model.Title = book.Title;
                model.Genre = ((GenreEnum)book.GenreId).ToString();
                model.PageCount = book.PageCount;
                model.PublishDate = book.PublishDate.Date.ToString("dd//MM/yyy");
                return model;
            }
        }

        public class GetBookByIdModel{
            public string Title { get; set; }
            public string Genre { get; set; }  
            public int PageCount { get; set; }
            public string PublishDate { get; set; }                
        }
    }
}