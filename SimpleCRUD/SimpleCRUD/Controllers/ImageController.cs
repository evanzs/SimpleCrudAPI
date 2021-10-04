using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.Data;
using SimpleCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IRepository<Image> _imageRepository;
        public ImageController( IRepository<Image> imageRepository)
        {           
            
            _imageRepository = imageRepository;         
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_imageRepository.GetAll());
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
                var book = _imageRepository.GetById(Id);

                return StatusCode(200, book);
            }
            catch (Exception)
            {
                return StatusCode(500, "");
            }
        }
    }
}
