using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UKBET.Models;

namespace UKBET.Util
{
    public class ValidationModelsukBetsHistory : AbstractValidator<ukBetsHistory>
    {
        public ValidationModelsukBetsHistory()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name nao pode ser Nulo");
            RuleFor(s => s.BetId).NotEmpty().WithMessage("BetId nao pode ser Nulo");
            RuleFor(s => s.StrategyName).Length(1, 150);
            RuleFor(s => s.MatchedDate).Must(ValidaData).WithMessage("Data Invalida");
        }
        //Aqui criamos uma validação customizada
        private static bool ValidaData(DateTime MatchedDate)
        {
            DateTime dataValida = new DateTime(1754, 1, 1, 00, 00, 00);
            return (MatchedDate > dataValida);
        }


    }
}

