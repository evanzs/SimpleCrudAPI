using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.Data;
using SimpleCRUD.Entities;
using SimpleCRUD.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Linq;

namespace SimpleCRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Image> _imageRepository;
        private  IAmazonS3 _s3Client;

        private readonly IOptions<S3Configuration> _config;


        public BookController(IRepository<Book> bookRepository, IRepository<Author> authorRepository, IRepository<Image> imageRepository, IOptions<S3Configuration> config)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _imageRepository = imageRepository;
            _config = config;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_bookRepository.GetAll());
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
                var book = _bookRepository.GetById(Id);              

                return StatusCode(200, book);
            }
            catch (Exception)
            {
                return StatusCode(500, "");
            }
        }
        [HttpPost]
        public IActionResult Post(BookDTO Book)
        {
            var authors = new List<Author>();

            if(Book.AuthorIds.Count > 0)
            {
                var newauthors = _authorRepository.FindIn(Book.AuthorIds);
                authors.AddRange(newauthors);
            }

            var model = new Book()
            {
                Title = Book.Title,
                SubTitle = Book.SubTitle,
                Category = Book.Category,
                PublicationDate = Book.PublicationDate, 
                Author = authors
            };

            try
            {
                var newBook = _bookRepository.Add(model);
                return StatusCode(201, newBook);
            }
            catch (Exception)
            {
                return StatusCode(500, "");
            }
        }
        [HttpPut]
        public IActionResult Update(BookDTO book)
        {          
          
            try
            {
                Book updateBook = _bookRepository.GetById(book.Id);

                if(book.AuthorIds.Count > 0)
                {
                    var newauthors = _authorRepository.FindIn(book.AuthorIds);
                    updateBook.Author = newauthors;


                }

                if (updateBook == null)
                {
                    return StatusCode(404, "");
                }

                updateBook.SubTitle = book.SubTitle;
                updateBook.PublicationDate = book.PublicationDate;
                updateBook.Title = book.Title;
                updateBook.Category = book.Category;


                updateBook = _bookRepository.Update(updateBook);

                

                return StatusCode(200, updateBook);

            }
            catch (Exception)
            {

                return StatusCode(500, "");
            }
        }
        [HttpPost("{Id}/uploadImage")]
        public async Task<PutObjectResponse>UploadImage(int Id, IFormFile file)
        {
            Book book = _bookRepository.GetById(Id);
            var response = new PutObjectResponse();
           

            if (book != null )
            {

                var image = new Image();


                var config = new AmazonS3Config()
                {
                    RegionEndpoint = RegionEndpoint.USEast2,
                };

                using (var client = new AmazonS3Client(_config.Value.AcessKey,_config.Value.SecretKey, config))
                {

                    using (var stream = new System.IO.MemoryStream())
                    {

                        file.CopyTo(stream);
                       

                        PutObjectRequest request = new PutObjectRequest();
                        request.InputStream = stream;
                        request.BucketName = _config.Value.BucketName;
                        request.CannedACL = S3CannedACL.PublicRead;
                        request.Key = file.FileName;
                        response = await client.PutObjectAsync(request);

                    } ;                  
                  
                }



               





            }
            return response;
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                _bookRepository.Delete(Id);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "");
            }
        }
    }
}
