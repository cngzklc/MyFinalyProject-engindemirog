using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç.
    public interface IResult
    {
        bool Success { get; } // BAşarılı mı? Başarısız mı?
        string Message { get; } // Başarılı olup olmadığınının mesajını tutacak.
    }
}
