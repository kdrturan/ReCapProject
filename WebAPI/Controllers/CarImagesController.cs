using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
    
        public CarImagesController(ICarImageService carImagesService)
        {
            _carImageService = carImagesService;
        }

        [HttpPost("add")]
        public IActionResult Add(CarImage carImages)
        {
            var result = _carImageService.Add(carImages);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(CarImage carImages)
        {
            var result = _carImageService.Update(carImages);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CarImage carImages)
        {
            var result = _carImageService.Delete(carImages);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getall")]
        public IActionResult Getall()
        {
            var result = _carImageService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarid")]
        public IActionResult GetByCarId(int id)
        {
            var result = _carImageService.GetByCarId(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
