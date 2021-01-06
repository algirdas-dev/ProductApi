using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Api.Models.Product;
using Product.Domain.Dtos;
using Product.Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController<IProductService>
    {
        public ProductController(ILogger<ProductController> logger, IProductService service) :base(logger, service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Single([FromQuery]SingleRequest req)
        {
            Logger.LogInformation($"Get product with filter: name={req?.Name}, bestRatingProduct={req?.BestRatingProduct}, worstRatingProduct={req?.WorstRatingProduct}");

            var product = await Service.Single(req.Name, req.BestRatingProduct, req.WorstRatingProduct).ConfigureAwait(false);

            if (product == null)
                return NotFound($"name={req?.Name}, bestRatingProduct={req?.BestRatingProduct}, worstRatingProduct={req?.WorstRatingProduct}");

            var result = new SingleResponse { 
                Code = product.Code,
                Name = product.Name,
                Description = product.Description
            };

            return Ok(result);
        }

        [HttpGet("GetComments")]
        public async Task<IActionResult> GetComments([FromQuery]GetCommentsRequest req)
        {
            Logger.LogInformation($"Get product comments with filter: ProductCode={req?.ProductCode}");

            int? productId = await Service.GetId(req.ProductCode).ConfigureAwait(false);

            if (!productId.HasValue)
                return NotFound($"ProductCode={req?.ProductCode}");

            var comments = await Service.GetComments(productId).ConfigureAwait(false);

            var result = new GetCommentResponse();
            if (comments != null) {
                result.Comments = new List<GetCommentResponse.Comment>();
                foreach (var comment in comments) {
                    result.Comments.Add(new GetCommentResponse.Comment {
                        PosterName = comment.PosterName,
                        ProductName = comment.ProductName,
                        Rating = comment.Rating,
                        Description = comment.Description
                    });
                }
            }


            return Ok(result);
        }

        [HttpPost("SaveComment")]
        public async Task<IActionResult> SaveComment([FromBody]SaveCommentRequest req)
        {
            Logger.LogInformation($"Get product comments with filter: ProductCode={req?.ProductCode}");

            int? productId = await Service.GetId(req?.ProductCode).ConfigureAwait(false);

            if (!productId.HasValue)
                return NotFound($"ProductCode={req?.ProductCode}");

            var comment = new CommentDto { 
                Description = req.Description,
                PosterName = req.PosterName,
                ProductId = productId.Value,
                Rating = req.Rating
            };

            await Service.SaveComment(comment).ConfigureAwait(false);

            return Ok();
        }
    }
}
