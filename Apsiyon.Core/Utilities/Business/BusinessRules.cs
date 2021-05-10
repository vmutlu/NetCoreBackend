using Apsiyon.Core.Utilities.Results;

namespace Apsiyon.Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var result in logics)
                if (!result.Success)
                    return result;

            return null;
        }
    }
}
