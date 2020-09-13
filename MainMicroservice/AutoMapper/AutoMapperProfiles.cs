using AutoMapper;
using DataAccess.Entities;
using MainMicroservice.Dtos.Flashcards;
using MainMicroservice.Dtos.Pronunciations;
using MainMicroservice.Dtos.Topic;
using MainMicroservice.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForReturn>();
            CreateMap<UserForCreate, User>();
            CreateMap<UserForUpdate, User>();

            CreateMap<Topic, TopicForReturn>();
            CreateMap<TopicForCreate, Topic>();
            CreateMap<TopicForUpdate, Topic>();

            CreateMap<Pronunciation, PronunciationForReturn>().ForMember(x => x.FlashcardName, y => { y.MapFrom(z => z.Flashcard.Word); }); ;
            CreateMap<PronunciationForCreate, Pronunciation>();
            CreateMap<PronunciationForUpdate, Pronunciation>();

            CreateMap<Flashcard, FlashcardForReturn>();
            CreateMap<FlashcardForCreate, Flashcard>();
            CreateMap<FlashcardForUpdate, Flashcard>();
        }
    }
}
