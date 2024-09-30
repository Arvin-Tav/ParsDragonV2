using Learning.Domain.DTO.Forum;
using Learning.Domain.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IForumRepository
    {
        #region Question

        Task<FilterQuestionDTO> FilterQuestion(FilterQuestionDTO filter);
        Task AddQuestion(Question question);
        Task<List<Question>> GetLastQuestionsByTake(int takeCount);
        Task<Question> GetQuestionById(int questionId);
        void UpdateQuestion(Question question);
        public Task<bool> IsUserTypeQuestion(int questionId, int userId);
        #endregion


        #region Answer
        Task<FilterAnswerDTO> FilterAnswer(FilterAnswerDTO filter);

        Task AddAnswer(Answer answer);
        Task<List<Answer>> GetAnswersByQuestionId(int questionId);
        Task<Answer> GetAnswerById(int answerId);
        void UpdateAnswer(Answer answer);

        #endregion

        Task Save();
    }
}
