using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Contorllers;
using WebApi.Coomon;

namespace WebApi.BookOperations.GetBooks
{    
    public class GetBooksQuery{
        private readonly BookStoreDbContext _dbContext;
        
        public GetBooksQuery(BookStoreDbContext dbContext){
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle(){
            List<Book> bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();  
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach(var book in bookList){
                vm.Add(new BooksViewModel(){
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.Date.ToString("dd//MM/yyy")
                });
            }          
            return vm;
        }
    }

    public class BooksViewModel{

        public string Title { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }   
        
        public string Genre { get; set; }   
    }
}