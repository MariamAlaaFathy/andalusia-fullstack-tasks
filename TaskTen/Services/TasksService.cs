using AutoMapper;
using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using FullStackSession6.Services.Interfaces;
using TaskTen.DTOs;
using TaskTen.Exceptions;
using TaskTen.Model;

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

        public async Task<PagedResult<TaskSummaryDTO>> GetTasks(TaskFilterParams paginationParams, int userId)
        {
            var tasks = await _taskRepository.GetTasks(paginationParams, userId);
            return new PagedResult<TaskSummaryDTO>
            {
                Data = _mapper.Map<List<TaskSummaryDTO>>(tasks.Data),
                Page = tasks.Page,
                PageSize = tasks.PageSize,
                TotalCount = tasks.TotalCount
            };
        }

        public async Task<TasksDTO> GetTaskById(int id, int userId)
        {
            var task = await _taskRepository.GetTaskById(id, userId);
            var taskDTO = _mapper.Map<TasksDTO>(task);
            return taskDTO;
        }

        public async Task<TaskSummaryDTO> GetTaskSummaryById(int id, int userId)
        {
            var task = await _taskRepository.GetTaskById(id, userId);
            var taskSummaryDTO = _mapper.Map<TaskSummaryDTO>(task);
            return taskSummaryDTO;
        }

        public async Task<Tasks> GetTaskByTitle(string title, int userId)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }
            return await _taskRepository.GetTaskByTitle(title, userId);
        }

        public async Task<TasksDTO> CreateTask(CreateTaskRequest task, int userId)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            else if (await _taskRepository.GetTaskByTitle(task.Title!, userId) != null)
            {
                throw new ConflictException("A task with the same title already exists.");
            }
            else if (task.DueDate < DateTime.Now)
            {
                throw new DueDateInPastException("The due date cannot be in the past.");
            }
            var mappedTask = _mapper.Map<Tasks>(task);
            mappedTask.UserId = userId;
            var created = await _taskRepository.CreateTask(mappedTask, userId);
            var taskDTO = _mapper.Map<TasksDTO>(created);
            return taskDTO;
        }

        public async Task<TasksDTO> UpdateTask(int id, UpdateTaskRequest task, int userId)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            var mappedTask = _mapper.Map<Tasks>(task);
            var existingTask = await _taskRepository.UpdateTask(id, mappedTask, userId);
            var taskDTO = _mapper.Map<TasksDTO>(existingTask);
            return taskDTO;
        }

        public async Task DeleteTask(int id, int userId)
        {
            if (await _taskRepository.GetTaskById(id, userId) == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            await _taskRepository.DeleteTask(id, userId);
        }
    }
}
