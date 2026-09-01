using AutoMapper;
using FullStackSession6.Model;
using TaskTen.DTOs;
using TaskTen.Enums;
using TaskTen.Model;

namespace TaskTen.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users => UserDTO
            CreateMap<Users, UserDTO>()
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => src.Role.ToString())
                );

            // RegisterUserRequestDTO => Users
            CreateMap<RegisterUserRequestDTO, Users>()
                .ForMember(
                    dest => dest.HashedPassword,
                    opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password))
                )
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => Enum.Parse<Role>(src.UserRole, true))
                );

            // UpdateUserRequestDTO => Users
            CreateMap<UpdateUserRequestDTO, Users>()
                .ForMember(
                    dest => dest.HashedPassword,
                    opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.NewPassword))
                )
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => Enum.Parse<Role>(src.Role, true))
                );

            // Tasks => TasksDTO
            CreateMap<Tasks, TasksDTO>()
            .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.User != null ? src.User.Name : null)
            );

            // Tasks => TaskSummaryDTO
            CreateMap<Tasks, TaskSummaryDTO>();

            // CreateTaskRequest => Tasks
            CreateMap<CreateTaskRequest, Tasks>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.IsCompleted,
                opt => opt.MapFrom(_ => false)
            )
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(_ => DateTime.UtcNow)
            )
            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.User,
                opt => opt.Ignore()
            );

            // UpdateTaskRequest => Tasks
            CreateMap<UpdateTaskRequest, Tasks>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.User,
                opt => opt.Ignore()
            );
        }
    }
}
