using AutoMapper;
using DataAccess.Entities;
using MainMicroservice.Dtos.Flashcards;
using MainMicroservice.Dtos.Images;
using MainMicroservice.Dtos.Pronunciations;
using MainMicroservice.Dtos.TestDetails;
using MainMicroservice.Dtos.Tests;
using MainMicroservice.Dtos.Topic;
using MainMicroservice.Dtos.UserFlashcards;
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

            CreateMap<Pronunciation, PronunciationForReturn>().ForMember(x => x.FlashcardName, y => { y.MapFrom(z => z.Flashcard.Word); });
            CreateMap<PronunciationForCreate, Pronunciation>();
            CreateMap<PronunciationForUpdate, Pronunciation>();

            CreateMap<Flashcard, FlashcardForReturn>().ForMember(x => x.TopicName, y => { y.MapFrom(z => z.Topic.Name); }); ;
            CreateMap<FlashcardForCreate, Flashcard>();
            CreateMap<FlashcardForCreateAI, Flashcard>();
            CreateMap<FlashcardForCreateByUserId, Flashcard>();
            CreateMap<FlashcardForUpdate, Flashcard>();

            CreateMap<UserFlashcard, UserFlashcardForReturn>();
            CreateMap<UserFlashcardForCreate, UserFlashcard>();
            CreateMap<UserFlashcardForUpdate, UserFlashcard>();

            CreateMap<Image, ImageForReturn>().ForMember(x => x.FlashcardName, y => { y.MapFrom(z => z.Flashcard.Word); });
            CreateMap<ImageForCreateDB, Image>();

            CreateMap<Test, TestForReturn>().ForMember(x => x.UserName, y => { y.MapFrom(z => z.User.UserName); });
            CreateMap<TestForCreate, Test>();
            CreateMap<TestForUpdate, Test>();
            CreateMap<Test, TestAndTestDetailForReturn>();

            CreateMap<TestDetail, TestDetailForReturn>();
            CreateMap<TestDetail, TestDetailAndTestForReturn>();
            CreateMap<TestDetailForCreate, TestDetail>();
            CreateMap<TestDetailForUpdate, TestDetail>();

        }
    }
}
