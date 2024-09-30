using Learning.Domain.DTO.Forum;
using Learning.Domain.DTO.Paging;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Question;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class ForumRepository : IForumRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public ForumRepository(LearningContext context)
        {
            _context = context;
        }

        #endregion
        public async Task<FilterQuestionDTO> FilterQuestion(FilterQuestionDTO filter)
        {
            IQueryable<Question> query = _context.Questions
                .Where(i => i.Course.IsDelete == false)
                .OrderByDescending(u => u.CreateDate)
               .Include(i => i.Course)
               .Include(a => a.Answers)
               .Include(i => i.User);

            #region state
            switch (filter.FilterQuestionOrder)
            {
                case FilterQuestionOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterQuestionOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }

            switch (filter.FilterQuestionRead)
            {
                case FilterQuestionRead.All:
                    break;
                case FilterQuestionRead.NotRead:
                    query = query.Where(i => !i.IsAdminRead);
                    break;
                case FilterQuestionRead.Read:
                    query = query.Where(i => i.IsAdminRead);
                    break;
            }
            #endregion

            #region filter
            if (filter.CourseId != null && filter.CourseId != 0)
                query = query.Where(i => i.CourseId == filter.CourseId);
            if (filter.QuestionId != null && filter.QuestionId != 0)
                query = query.Where(i => i.QuestionId == filter.QuestionId);
            if (filter.UserId != null && filter.UserId != 0)
                query = query.Where(i => i.UserId == filter.UserId);
            if (!string.IsNullOrEmpty(filter.Search))
                query = query
                    .Where(i => EF.Functions.Like(i.Title, $"%{filter.Search}%") ||
                    EF.Functions.Like(i.Body, $"%{filter.Search}%"));
            if (!string.IsNullOrEmpty(filter.UserName))
                query = query
                    .Where(i => EF.Functions.Like(i.User.UserName, $"%{filter.UserName}%") ||
                    EF.Functions.Like(i.User.Email, $"%{filter.UserName}%"));
            #endregion
            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter.SetPaging(pager).SetQuestions(allEntities);

        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        #region question
        public async Task AddQuestion(Question question)
        {
            await _context.Questions.AddAsync(question);
        }
        public async Task<List<Question>> GetLastQuestionsByTake(int takeCount)
        {
            return await _context.Questions.Include(a => a.Answers).OrderByDescending(i => i.CreateDate).Take(6).ToListAsync();

        }

        public async Task<Question> GetQuestionById(int questionId)
        {
            return await _context.Questions.AsQueryable()
                .Include(i=>i.User)
                .Include(u => u.Course)
                .FirstOrDefaultAsync(i => i.QuestionId == questionId);
        }

        public async Task<bool> IsUserTypeQuestion(int questionId, int userId)
        {
            return await _context.Questions.AnyAsync(i => i.QuestionId == questionId && i.UserId == userId);
        }
        public void UpdateQuestion(Question question)
        {
            _context.Questions.Update(question);
        }
        #endregion


        #region answer
        public async Task<FilterAnswerDTO> FilterAnswer(FilterAnswerDTO filter)
        {
            IQueryable<Answer> query = _context.Answers
               .Where(i => i.Question.IsDelete == false)
               .OrderByDescending(u => u.CreateDate)
              .Include(i => i.Question)
              .Include(i => i.User);

            #region state
            switch (filter.FilterAsnwerOrder)
            {
                case FilterAsnwerOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterAsnwerOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }

            switch (filter.FilterAsnwerRead)
            {
                case FilterAsnwerRead.All:
                    break;
                case FilterAsnwerRead.NotRead:
                    query = query.Where(i => !i.IsAdminRead);
                    break;
                case FilterAsnwerRead.Read:
                    query = query.Where(i => i.IsAdminRead);
                    break;
            }
            #endregion

            #region filter
            if (filter.QuestionId != null && filter.QuestionId != 0)
                query = query.Where(i => i.QuestionId == filter.QuestionId);
            if (filter.UserId != null && filter.UserId != 0)
                query = query.Where(i => i.UserId == filter.UserId);
            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i => EF.Functions.Like(i.BodyAnswer, $"%{filter.Search}%"));
          
            if (!string.IsNullOrEmpty(filter.UserName))
                query = query = query
                    .Where(i => EF.Functions.Like(i.User.UserName, $"%{filter.UserName}%") ||
                    EF.Functions.Like(i.User.Email, $"%{filter.UserName}%"));
            #endregion
            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter.SetPaging(pager).SetAsnwers(allEntities);
        }
        public async Task AddAnswer(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
        }

        public Task<Answer> GetAnswerById(int answerId)
        {
            return _context.Answers.AsQueryable()
                .Include(u => u.User)
                .FirstOrDefaultAsync(i => i.AnswerId == answerId);
        }
        public async Task<List<Answer>> GetAnswersByQuestionId(int questionId)
        {
            return await _context.Answers.Where(i => i.QuestionId == questionId).ToListAsync();

        }
        public void UpdateAnswer(Answer answer)
        {
            _context.Answers.Update(answer);
        }

        #endregion













    }
}
