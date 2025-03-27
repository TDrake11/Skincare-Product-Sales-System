using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.CommentService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet("listComments")]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var comments = await _commentService.GetAllComments();
                var commentModel = _mapper.Map<IEnumerable<CommentModel>>(comments);
                return Ok(commentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCommentById/{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            try
            {
                var comment = await _commentService.GetCommentById(id);
                if (comment == null)
                {
                    return Ok("No comment found.");
                }
                return Ok(_mapper.Map<CommentModel>(comment));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCommentsByProductId/{productId}")]
        public async Task<IActionResult> GetCommentsByProductId(int productId)
        {
            try
            {
                var comments = await _commentService.GetCommentByProductId(productId);
                if (comments == null || !comments.Any())
                {
                    return Ok("No comments found for this product.");
                }
                var commentModels = _mapper.Map<IEnumerable<CommentModel>>(comments);
                return Ok(commentModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCommentsByCustomerId/{customerId}")]
        public async Task<IActionResult> GetCommentsByCustomerId(string customerId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByCustomerId(customerId);
                if (comments == null || !comments.Any())
                {
                    return Ok("No comments found for this Customer.");
                }
                var commentModels = _mapper.Map<IEnumerable<CommentModel>>(comments);
                return Ok(commentModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createComment")]
        public async Task<IActionResult> Create(CreateCommentModel commentModel)
        {
            try
            {
            var comment = _mapper.Map<Comment>(commentModel);

            await _commentService.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, _mapper.Map<CommentModel>(comment));
        }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateComment")]
        public async Task<IActionResult> Update([FromBody] UpdateCommentModel commentModel)
        {
            try
            {
            var comment = _mapper.Map<Comment>(commentModel);
            await _commentService.UpdateCommentAsync(comment);
                return Ok("Comment updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteComment/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var comment = await _commentService.GetCommentById(id);
                if (comment == null)
                {
                    return Ok("Comment not found");
                }
            await _commentService.DeleteCommentAsync(id);
                return Ok("Comment deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
