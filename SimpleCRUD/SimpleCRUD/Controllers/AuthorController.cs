using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.Data;
using SimpleCRUD.Entities;
using SimpleCRUD.Models;
using System;


namespace SimpleCRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorController(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_authorRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "");
            }
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            try
            {                
                return Ok(_authorRepository.GetById(Id));
            }
            catch (Exception)
            {
                return StatusCode(500, "");
            }
        }
        [HttpPost]
        public IActionResult Post(Author author)
        {
            try
            {
                var newAuthor = _authorRepository.Add(author);
                
                return StatusCode(201, newAuthor);
            }
            catch (Exception)
            {
                return StatusCode(500, "");
            }
        }
        [HttpPut]
        public IActionResult Update(AuthorDTO Author)
        {     

            try
            {
                var author = _authorRepository.GetById(Author.Id);

                if (author.Equals(null))
                {
                    return StatusCode(404, "");
                }

                author = _authorRepository.Update(author);

                return StatusCode(200,author);

            }
            catch (Exception)
            {

                return StatusCode(500, "");
            }
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                _authorRepository.Delete(Id);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "");
            }
        }

    }



}

