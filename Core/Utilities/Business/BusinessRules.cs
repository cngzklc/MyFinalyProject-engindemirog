using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        //params = istediğimiz kadar virgül ile IResult türünde parametre gönderebiliyoruz demek.
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    //Bütün kuralları gez, kurala uymayan varsa bize döndür.
                    return logic;
                }
                
            }
            return null;
        }
    }
}
