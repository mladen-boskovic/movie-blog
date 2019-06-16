using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CommentCommands;
using Application.DTO;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
		private readonly IGetCommentsCommand getComments;
		private readonly IGetCommentCommand getComment;
		private readonly IAddCommentCommand addComment;
		private readonly IEditCommentCommand editComment;
		private readonly IDeleteCommentCommand deleteComment;

		public CommentsController(IGetCommentsCommand getComments, IGetCommentCommand getComment, IAddCommentCommand addComment, IEditCommentCommand editComment, IDeleteCommentCommand deleteComment)
		{
			this.getComments = getComments;
			this.getComment = getComment;
			this.addComment = addComment;
			this.editComment = editComment;
			this.deleteComment = deleteComment;
		}

        // GET: api/Comments
        /// <summary>
        /// Gets all comments.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "UserId": 1,
        ///        "MovieId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Gets all comments</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet]
        public ActionResult<PagedResponse<CommentDto>> Get([FromQuery] CommentSearch search)
        {
			try
			{
				var comments = getComments.Execute(search);
				return Ok(comments);
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // GET: api/Comments/5
        /// <summary>
        /// Gets one comment by ID.
        /// </summary>
        /// <response code="200">Gets one comment by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet("{id}")]
        public ActionResult<CommentDto> Get(int id)
        {
			try
			{
				var comment = getComment.Execute(id);
				return Ok(comment);
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // POST: api/Comments
        /// <summary>
        /// Adds new comment.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "UserId": 1,
        ///        "MovieId": 1,
        ///        "Text" : "I like this movie"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Adds new comment</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpPost]
        public ActionResult Post([FromBody] InsertUpdateCommentDto dto)
        {
			try
			{
				addComment.Execute(dto);
				return StatusCode(201);
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // PUT: api/Comments/5
        /// <summary>
        /// Edits comment.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "Text" : "I like this movie"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Edits comment</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] InsertUpdateCommentDto dto)
        {
			dto.Id = id;

			try
			{
				editComment.Execute(dto);
				return NoContent();
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes one comment by ID.
        /// </summary>
        /// <response code="204">Deletes one comment by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="409">If item is already deleted</response>
        /// <response code="500">If server error occurred</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
			try
			{
				deleteComment.Execute(id);
				return NoContent();
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (EntityAlreadyDeletedException e)
			{
				return Conflict(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}
    }
}
