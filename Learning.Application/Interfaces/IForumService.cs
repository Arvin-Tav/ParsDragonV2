using Learning.Domain.DTO.Forum;
using Learning.Domain.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IForumService
    {
        #region Question
        Task<int> AddQuestion(Question question);
        Task<CreateForumResult> CreateQuestion(CreateQuestionDTO create);
        Task<ShowQuestionByAnswersDTO> ShowQuestion(int questionId);
        Task<FilterQuestionDTO> FilterQuestion(FilterQuestionDTO filter);
        Task<List<Question>> GetLastQuestions(int takeCount);
        Task<Question> GetQuestionById(int questionId);
        Task<DeleteForumResult> DeleteQuestion(int questionId);
        Task<DeleteForumResult> DeleteQuestionByUser(int questionId,int userId);
        Task<DeleteForumResult> DeleteAnswerByUser(int answerId,int questionId,int userId);
        Task UpdateQuestion(Question question);
        Task IsAdminReadQuestion(int questionId);
        Task<bool> IsUserTypeQuestion(int questionId, int userId);
        #endregion


        #region Answer
        Task AddAnswer(Answer answer);
        Task<CreateForumResult> CreateAnswer(CreateAnswerDTO create);

        Task ChangeIsTrueAnswer(int questionId, int answerId);
        Task<List<Answer>> GetAnswersByQuestionId(int questionId);
        Task<FilterAnswerDTO> FilterAnswer(FilterAnswerDTO filter);
        Task<Answer> GetAnswerById(int answerId);
        Task<DeleteForumResult> DeleteAnswer(int answerId);
        Task UpdateAnswer(Answer answer);
        Task IsAdminReadAnswer(int answerId);

        #endregion
    }
}
