﻿using Learning.Domain.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Forum
{
    public class ShowQuestionByAnswersDTO
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
