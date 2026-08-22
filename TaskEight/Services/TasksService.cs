using AutoMapper;
using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using FullStackSession6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskEight.DTOs;
using TaskEight.Exceptions;
using TaskEight.Model;

namespace FullStackSession6.Services
{
    public class TasksService : ITasksService
    {
        private ITasksRepository _taskRepository;
        private IMapper _mapper;
        public TasksService(ITasksRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<Tasks>> GetTasks(TaskFilterParams paginationParams)
        {
            return await _taskRepository.GetTasks(paginationParams);
        }

        public async Task<TasksDTO> GetTaskById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            var task = await _taskRepository.GetTaskById(id);
            var taskDTO = _mapper.Map<TasksDTO>(task);
            return taskDTO;
        }

        public async Task<TaskSummaryDTO> GetTaskSummaryById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            var task = await _taskRepository.GetTaskById(id);
            var taskSummaryDTO = _mapper.Map<TaskSummaryDTO>(task);
            return taskSummaryDTO;
        }

        public async Task<Tasks> GetTaskByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }
            return await _taskRepository.GetTaskByTitle(title);
        }

        public async Task<TasksDTO> CreateTask(Tasks task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            else if (await _taskRepository.GetTaskByTitle(task.Title!) != null)
            {
                throw new ConflictException("A task with the same title already exists.");
            }
            else if (task.DueDate < DateTime.Now)
            {
                throw new DueDateInPastException("The due date cannot be in the past.");
            }
            task.Id = default;
            task.CreatedAt = default;
            var createdTask = await _taskRepository.CreateTask(task);
            var taskDTO = _mapper.Map<TasksDTO>(createdTask);
            return taskDTO;
        }

        public async Task<TasksDTO> UpdateTask(int id, Tasks task)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            else if(task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            var existingTask = await _taskRepository.UpdateTask(id, task);
            var taskDTO = _mapper.Map<TasksDTO>(existingTask);
            return taskDTO;
        }

        public async Task DeleteTask(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            else if (await _taskRepository.GetTaskById(id) == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            await _taskRepository.DeleteTask(id);
        }
    }
}
