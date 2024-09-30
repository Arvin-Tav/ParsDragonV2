using Learning.Application.Interfaces;
using Learning.Domain.DTO.Forum;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Question;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class ForumService : IForumService
    {
        #region constructor
        private readonly IForumRepository _forumRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IPermissionRepository _permissionRepository;
        public ForumService(IForumRepository forumRepository, ICourseRepository courseRepository, IPermissionRepository permissionRepository)
        {
            _forumRepository = forumRepository;
            _courseRepository = courseRepository;
            _permissionRepository = permissionRepository;
        }
        #endregion

        #region question

        public async Task<int> AddQuestion(Question question)
        {
            await _forumRepository.AddQuestion(question);
            await _forumRepository.Save();
            return question.QuestionId;
        }

        public async Task<DeleteForumResult> DeleteQuestion(int questionId)
        {
            var question = await _forumRepository.GetQuestionById(questionId);
            if (question == null) return DeleteForumResult.NotFound;
            question.IsDelete = true;
            _forumRepository.UpdateQuestion(question);
            await _forumRepository.Save();
            return DeleteForumResult.Success;
        }

        public async Task<DeleteForumResult> DeleteQuestionByUser(int questionId, int userId)
        {
            bool isadmin = false;
            if (await _permissionRepository.CheckPermission("Courses", userId))
            {
                isadmin = true;
            }
            var question = await _forumRepository.GetQuestionById(questionId);
            if (question == null && (question.UserId != userId || !isadmin)) return DeleteForumResult.NotFound;
            question.IsDelete = true;
            _forumRepository.UpdateQuestion(question);
            await _forumRepository.Save();
            return DeleteForumResult.Success;
        }
        public async Task<DeleteForumResult> DeleteAnswerByUser(int answerId, int questionId, int userId)
        {
            bool isadmin = false;
            if (await _permissionRepository.CheckPermission("Courses", userId))
            {
                isadmin = true;
            }
            var answer = await _forumRepository.GetAnswerById(answerId);
            if (answer == null && (answer.UserId != userId || !isadmin) && answer.QuestionId != questionId) return DeleteForumResult.NotFound;
            answer.IsDelete = true;
            _forumRepository.UpdateAnswer(answer);
            await _forumRepository.Save();
            return DeleteForumResult.Success;
        }



        public async Task<FilterQuestionDTO> FilterQuestion(FilterQuestionDTO filter)
        {
            return await _forumRepository.FilterQuestion(filter);
        }

        public Task<List<Question>> GetLastQuestions(int takeCount)
        {
            return _forumRepository.GetLastQuestionsByTake(takeCount);
        }

        public async Task<Question> GetQuestionById(int questionId)
        {
            return await _forumRepository.GetQuestionById(questionId);
        }

        public async Task IsAdminReadQuestion(int questionId)
        {
            var question = await GetQuestionById(questionId);
            question.IsAdminRead = true;
            await UpdateQuestion(question);
        }

        public async Task<bool> IsUserTypeQuestion(int questionId, int userId)
        {
            return await _forumRepository.IsUserTypeQuestion(questionId, userId);
        }

        public async Task<ShowQuestionByAnswersDTO> ShowQuestion(int questionId)
        {
            return new ShowQuestionByAnswersDTO()
            {
                Question = await GetQuestionById(questionId),
                Answers = await GetAnswersByQuestionId(questionId),
            };
        }
        public async Task<CreateForumResult> CreateQuestion(CreateQuestionDTO create)
        {
            var course = await _courseRepository.GetCourseById(create.CourseId);
            if (course == null) return CreateForumResult.NotFound;
            Question addquestion = new Question()
            {
                UserId = create.UserId,
                Body = create.Body,
                CourseId = create.CourseId,
                Title = create.Title,
            };
            await _forumRepository.AddQuestion(addquestion);
            await _forumRepository.Save();
            return CreateForumResult.Success;
        }

        public async Task UpdateQuestion(Question question)
        {
            _forumRepository.UpdateQuestion(question);
            await _forumRepository.Save();
        }

        #endregion

        #region answer


        public async Task AddAnswer(Answer answer)
        {
            await _forumRepository.AddAnswer(answer);
            await _forumRepository.Save();
        }

        public async Task<CreateForumResult> CreateAnswer(CreateAnswerDTO create)
        {
            var course = await _courseRepository.GetCourseById(create.CourseId);
            if (course == null) return CreateForumResult.NotFound;
            var question = await _forumRepository.GetQuestionById(create.QuestionId);
            if (course == null) return CreateForumResult.NotFound;
            Answer addanswer = new Answer()
            {
                UserId = create.UserId,
                BodyAnswer = create.Body,
                QuestionId = create.QuestionId,
            };
            await _forumRepository.AddAnswer(addanswer);
            await _forumRepository.Save();
            return CreateForumResult.Success;
        }

        public async Task ChangeIsTrueAnswer(int questionId, int answerId)
        {
            var answers = await GetAnswersByQuestionId(questionId);
            foreach (var item in answers)
            {
                item.IsTrue = false;
                if (item.AnswerId == answerId)
                {
                    item.IsTrue = true;
                }
                await UpdateAnswer(item);
            }
        }

        public Task<List<Answer>> GetAnswersByQuestionId(int questionId)
        {
            return _forumRepository.GetAnswersByQuestionId(questionId);
        }

        public async Task<FilterAnswerDTO> FilterAnswer(FilterAnswerDTO filter)
        {
            return await _forumRepository.FilterAnswer(filter);
        }

        public async Task<Answer> GetAnswerById(int answerId)
        {
            return await _forumRepository.GetAnswerById(answerId);
        }

        public async Task<DeleteForumResult> DeleteAnswer(int answerId)
        {
            var answer = await _forumRepository.GetAnswerById(answerId);
            if (answer == null) return DeleteForumResult.NotFound;
            answer.IsDelete = true;
            _forumRepository.UpdateAnswer(answer);
            await _forumRepository.Save();
            return DeleteForumResult.Success;
        }


        public async Task UpdateAnswer(Answer answer)
        {
            _forumRepository.UpdateAnswer(answer);
            await _forumRepository.Save();
        }

        public async Task IsAdminReadAnswer(int answerId)
        {
            var answer = await GetAnswerById(answerId);
            answer.IsAdminRead = true;
            await UpdateAnswer(answer);
        }
        #endregion




    }
}
