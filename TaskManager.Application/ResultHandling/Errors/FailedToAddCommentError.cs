using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.ResultHandling.Errors
{
    public class FailedToAddCommentError() : Error("Falha ao adicionar comentário na tarefa.")
    {
    }
}
