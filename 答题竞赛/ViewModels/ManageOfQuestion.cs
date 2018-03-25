using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 答题竞赛.ViewModels
{
    public class ManageOfQuestion: FileUploadViewModel
    {
        public List<QuestionViewModel> questions { get; set; }
    }
}