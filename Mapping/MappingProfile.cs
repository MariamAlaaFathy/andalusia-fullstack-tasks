using AutoMapper;
using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Movie => MovieDTO
            CreateMap<Movie, MovieDTOV1>();
            CreateMap<Movie, MovieDTOV2>();

            // CreateMovieRequest => Movie
            CreateMap<CreateMovieRequest, Movie>()
                .ForMember(dest => dest.ShowTimes, opt => opt.Ignore());

            // UpdateMovieRequest => Movie
            CreateMap<UpdateMovieRequest, Movie>()
                .ForMember(dest => dest.ShowTimes, opt => opt.Ignore());

            // Auditorium => AuditoriumDTO
            CreateMap<Auditorium, AuditoriumDTO>();

            // CreateAuditoriumRequest => Auditorium
            CreateMap<CreateAuditoriumRequest, Auditorium>()
                .ForMember(dest => dest.Shows, opt => opt.Ignore());

            // UpdateAuditoriumRequest => Auditorium
            CreateMap<UpdateAuditoriumRequest, Auditorium>()
                .ForMember(dest => dest.Shows, opt => opt.Ignore());

            // ShowTime => ShowTimeDTO
            CreateMap<ShowTime, ShowTimeDTO>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie != null ? src.Movie.Name : string.Empty))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Auditorium != null ? src.Auditorium.RoomNumber : default));

            // CreateShowTimeRequest => ShowTime
            CreateMap<CreateShowTimeRequest, ShowTime>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore())
                .ForMember(dest => dest.Auditorium, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore());

            // UpdateShowTimeRequest => ShowTime
            CreateMap<UpdateShowTimeRequest, ShowTime>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore())
                .ForMember(dest => dest.Auditorium, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore());

            // Customer => CustomerDTO
            CreateMap<Customer, CustomerDTO>();
            // CreateCustomerRequest => Customer
            CreateMap<CreateCustomerRequest, Customer>();
            // UpdateCustomerRequest => Customer
            CreateMap<UpdateCustomerRequest, Customer>();

            // Booking => BookingDTO
            CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : string.Empty))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Email : string.Empty))
                .ForMember(dest => dest.Show_Time, opt => opt.MapFrom(src => src.ShowTime != null ? src.ShowTime.Show_Time : default))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.ShowTime != null && src.ShowTime.Movie != null ? src.ShowTime.Movie.Name : string.Empty))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.ShowTime != null && src.ShowTime.Auditorium != null ? src.ShowTime.Auditorium.RoomNumber : default));

            // CreateBookingRequest => Booking
            CreateMap<CreateBookingRequest, Booking>()
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.ShowTime, opt => opt.Ignore());
        }
    }
}
