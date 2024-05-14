using Api.Dtos.Comment;
using Api.Dtos.Genre;
using Api.Dtos.MovieEpisode;
using Api.Dtos.MovieSeries;
using Api.Dtos.Region;
using Api.Dtos.Year;
using Api.Dtos.Reaction;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MovieSeries, MovieSeriesDto>();
            CreateMap<MovieSeriesDto, MovieSeries>();
            CreateMap<MovieSeries, CreateMovieSeriesDto>();
            CreateMap<CreateMovieSeriesDto, MovieSeries>();
            CreateMap<MovieSeries, UpdateMovieSeriesDto>();
            CreateMap<UpdateMovieSeriesDto, MovieSeries>();


            CreateMap<MovieEpisode, MovieEpisodeDto>();
            CreateMap<MovieEpisodeDto, MovieEpisode>();
            CreateMap<MovieEpisode, CreateMovieEpisodeDto>();
            CreateMap<CreateMovieEpisodeDto, MovieEpisode>();
            CreateMap<MovieEpisode, UpdateMovieEpisodeDto>();
            CreateMap<UpdateMovieEpisodeDto, MovieEpisode>();

            CreateMap<Genre, GenreDto>();           
            CreateMap<GenreDto, Genre>();
            CreateMap<Genre, CreateGenreDto>();
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<Genre, UpdateGenreDto>();
            CreateMap<UpdateGenreDto, Genre>();

            CreateMap<Region, RegionDto>();
            CreateMap<RegionDto, Region>();
            CreateMap<Region, CreateRegionDto>();
            CreateMap<CreateRegionDto, Region>();
            CreateMap<Region, UpdateRegionDto>();
            CreateMap<UpdateRegionDto, Region>();


            CreateMap<Year, YearDto>();
            CreateMap<YearDto, Year>();
            CreateMap<Year, CreateYearDto>();
            CreateMap<CreateYearDto, Year>();
            CreateMap<Year, UpdateYearDto>();
            CreateMap<UpdateYearDto, Year>();

            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CreateCommentDto>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment, UpdateCommentDto>();
            CreateMap<UpdateCommentDto, Comment>();

            CreateMap<Reaction, ReactionDto>();
            CreateMap<ReactionDto, Reaction>();
        }
    }
}
