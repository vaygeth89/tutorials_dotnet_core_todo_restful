using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private AppDBContext _dbContext;

        public TodoController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            try
            {
                var todos = await _dbContext.Todos.ToListAsync();
                return Ok(todos);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo(Todo todo)
        {
            try
            {
                await _dbContext.Todos.AddAsync(todo);
                int todoId = await _dbContext.SaveChangesAsync();
                return Ok(todo);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodos([FromRoute] int? id)
        {
            try
            {
                Todo todo = await _dbContext.Todos.FirstOrDefaultAsync(e => e.Id == id);
                if (todo == null) return NotFound();
                _dbContext.Remove(todo);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> UpdateTodos([FromRoute] int? id, Todo todo)
        {
            try
            {
                Todo toBeUpdatedTodo = await _dbContext.Todos.FirstOrDefaultAsync(e => e.Id == id);
                if (toBeUpdatedTodo == null) return NotFound();
                toBeUpdatedTodo.name = todo.name;
                toBeUpdatedTodo.IsCompleted = todo.IsCompleted;
                _dbContext.Update(toBeUpdatedTodo);
                await _dbContext.SaveChangesAsync();
                return Ok(toBeUpdatedTodo);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
