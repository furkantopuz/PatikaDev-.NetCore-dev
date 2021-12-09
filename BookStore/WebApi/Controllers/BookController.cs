using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.GetBook;
using static WebApi.BookOperations.GetBook.GetBookByIDCommand;

namespace WebApi.Contorllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context; //readonly ile değiştirilmesini istemiyoruz.  Uygulama içinde değiştirilemezler.

        public BookController (BookStoreDbContext context){
            _context = context;
        }
        
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

         [HttpGet("{id}")]
        public IActionResult GetBookByID(int id)
        {
            GetBookByIDCommand command = new GetBookByIDCommand(_context);
            GetBookByIdModel result = command.Handle(id);
                     
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook ){
            CreateBookCommand command = new CreateBookCommand(_context);
            try{
                command.Model = newBook;
                command.Handle();
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UptadeBook(int id, [FromBody] UpdateBookModel updateBook){
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try{
                command.Model = updateBook;
                command.Handle(id);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            var book = _context.Books.Where(x=> x.Id == id).FirstOrDefault();

            if(book == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return Ok();
            }

        }
    }
}