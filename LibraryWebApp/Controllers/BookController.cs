﻿using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IValidator<CreateBookRecord> _validator;

        public BookController(IUnitOfWork uof, IValidator<CreateBookRecord> validator)
        {
            _uof = uof;
            _validator = validator;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create([FromBody] CreateBookRecord request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            string? isCreated = await _uof.BookRepository.CreateBook(request);

            if (!string.IsNullOrEmpty(isCreated))
                return BadRequest($"{isCreated}");

            _uof.Save();

            return Ok();
        }

        [HttpGet("all")]
        public ActionResult<List<GetBookRecord>?> GetAll()
        {
            return Ok(_uof.BookRepository.GetAllBooks());
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GetBookRecord?>> GetById(Guid id)
        {
            var book = await _uof.BookRepository.GetBookById(id);

            if (book is null)
                return NotFound("Book with that ID wasn't found");
            return Ok(book);
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<UpdateBookRecord?>> GetByISBN(string isbn)
        {
            var book = await _uof.BookRepository.GetBookByISBN(isbn);

            if (book is null)
                return NotFound("Book with that ISBN wasn't found");
            return Ok(book);
        }

        [HttpPost("getbooks")]
        public async Task<ActionResult<List<GetBookResponse>?>> GetWithParams([FromBody] GetBookRequest request)
        {
            var books = await _uof.BookRepository.GetBooksWithParams(request);

            return Ok(books);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool isDeleted = await _uof.BookRepository.DeleteBook(id);

            if (!isDeleted)
                return NotFound("Book with that ID wasn't found");

            _uof.Save();

            return Ok();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<string?>> Update([FromBody] CreateBookRecord request, Guid id)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            var isUpdated = await _uof.BookRepository.UpdateBook(request, id);

            if (!string.IsNullOrEmpty(isUpdated))
                return BadRequest($"{isUpdated}");

            _uof.Save();

            return Ok();
        }

        [HttpGet("borrowed")]
        [Authorize]
        public ActionResult<List<GetBookRecord>?> GetBooksByUserId(Guid userId)
        {
            var books = _uof.BookRepository.GetBooksByUserId(userId);

            return Ok(books);
        }

        [HttpPut("return/{id:guid}")]
        [Authorize]
        public async Task<ActionResult> ReturnBook(Guid id)
        {
            var isReturned = await _uof.BookRepository.ReturnBook(id);

            if (!isReturned)
                return BadRequest();

            _uof.Save();

            return Ok();
        }
    }
}
